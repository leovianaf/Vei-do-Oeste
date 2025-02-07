using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider slider;
    public int maxHealth = 150;
    private int currentHealth;
    private Animator animator;
    private bool isDead = false;

    public Transform respawnPoint;  // Ponto de respawn do jogador
    private PlayerMovement playerMovement;

    // GameUI
    public GameObject[] heartObjects; // Array de objetos de cora��o no GameUI
    public GameObject gameOverScreen;

    void Start()
    {
        currentHealth = maxHealth;
        slider.value = currentHealth;
        slider.maxValue = maxHealth;
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();

        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        slider.value = currentHealth;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            LoseHeart();
        }

        UpdateHealthUI();
    }

    public void Heal(int amount)
    {
        if (isDead) return;

        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        slider.value = currentHealth;
        UpdateHealthUI();
    }

    private void LoseHeart()
    {
        int remainingHearts = currentHealth / 50;

        if (remainingHearts < heartObjects.Length)
        {
            heartObjects[remainingHearts].SetActive(false); // Desativa o cora��o perdido
        }

        if (remainingHearts <= 0)
        {
            TriggerGameOver(); // Game Over quando perder todos os cora��es
        }
        else
        {
            Die(); // Respawn ao perder um cora��o
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Dies") &&
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            Debug.Log("Estado atual: " + animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Dies"));
            Debug.Log("Tempo da anima��o: " + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            yield return null; // Espera o pr�ximo frame
        }

        yield return new WaitForSeconds(0.85f);

        transform.position = respawnPoint.position;
        currentHealth = Mathf.Max(currentHealth, 50); // Garantir ao menos 50 de vida ap�s respawn
        slider.value = currentHealth;
        isDead = false;

        animator.SetTrigger("Respawn");

        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        UpdateHealthUI();
    }

    private void TriggerGameOver()
    {
        isDead = true;
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void UpdateHealthUI()
    {
        int heartsToDisplay = currentHealth / 50;

        for (int i = 0; i < heartObjects.Length; i++)
        {
            heartObjects[i].SetActive(i < heartsToDisplay);
        }
    }
}

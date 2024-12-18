using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 150;
    private int currentHealth;
    private Animator animator;
    private bool isDead = false;

    public Transform respawnPoint;  // Ponto de respawn do jogador
    private PlayerMovement playerMovement;

    // GameUI
    public GameObject[] heartObjects; // Array de objetos de coração no GameUI
    public GameObject gameOverScreen;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();

        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

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
        UpdateHealthUI();
    }

    private void LoseHeart()
    {
        int remainingHearts = currentHealth / 50;

        if (remainingHearts < heartObjects.Length)
        {
            heartObjects[remainingHearts].SetActive(false); // Desativa o coração perdido
        }

        if (remainingHearts <= 0)
        {
            TriggerGameOver(); // Game Over quando perder todos os corações
        }
        else
        {
            Die(); // Respawn ao perder um coração
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
            Debug.Log("Tempo da animação: " + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            yield return null; // Espera o próximo frame
        }

        yield return new WaitForSeconds(0.85f);

        transform.position = respawnPoint.position;
        currentHealth = Mathf.Max(currentHealth, 50); // Garantir ao menos 50 de vida após respawn
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

    private void UpdateHealthUI()
    {
        int heartsToDisplay = currentHealth / 50;

        for (int i = 0; i < heartObjects.Length; i++)
        {
            heartObjects[i].SetActive(i < heartsToDisplay);
        }
    }
}

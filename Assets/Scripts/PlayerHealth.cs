using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    public Slider slider;
    public int maxHealth = 150;
    private int currentHealth;
    private Animator animator;
    private bool isDead = false;
    public float damageReduction = 0f;

    public Transform respawnPoint;  // Ponto de respawn do jogador
    private PlayerMovement playerMovement;

    // GameUI
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

    private void UpdateHealthUI()
    {
        if (slider != null)
        {
            slider.value = currentHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        int reducedDamage = Mathf.RoundToInt(damage * (1 - damageReduction));
        currentHealth -= reducedDamage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        UpdateHealthUI();
    }

    public void Heal(int amount)
    {
        if (isDead) return;

        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        UpdateHealthUI();
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

        /* while (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Dies") &&
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            Debug.Log("Estado atual: " + animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Dies"));
            Debug.Log("Tempo da anima��o: " + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            yield return null; // Espera o pr�ximo frame
        }

        yield return new WaitForSeconds(0.85f);

        Time.timeScale = 1;
        transform.position = respawnPoint.position;
        currentHealth = Mathf.Max(currentHealth, 50); // Garantir ao menos 50 de vida ap�s respawn
        slider.value = currentHealth;
        isDead = false;

        animator.SetTrigger("Respawn");

        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        } */
        yield return new WaitForSeconds(0.85f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void TriggerGameOver()
    {
        isDead = true;
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }
}

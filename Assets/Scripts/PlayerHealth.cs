using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;
    private bool isDead = false;

    public Transform respawnPoint;  // Ponto de respawn do jogador
    private PlayerMovement playerMovement;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (isDead) return;
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
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
        currentHealth = maxHealth;
        isDead = false;

        animator.SetTrigger("Respawn");

        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }
    }
}

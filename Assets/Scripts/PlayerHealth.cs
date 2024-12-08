using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;
    private bool isDead = false;

    public Transform respawnPoint;  // Ponto de respawn do jogador

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
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
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        float deathAnimTime = animator.GetCurrentAnimatorStateInfo(0).length;
        float respawnWaitTime = deathAnimTime + 0.2f;

        yield return new WaitForSeconds(respawnWaitTime);
        transform.position = respawnPoint.position;
        currentHealth = maxHealth;
        isDead = false;

        animator.SetTrigger("Respawn");
    }
}

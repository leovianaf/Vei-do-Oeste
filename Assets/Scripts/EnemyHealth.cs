using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 20;
    private int currentHealth;
    private Animator animator;
    private bool isDead = false;

    private EnemyMovement enemyMovement;
    private Collider2D enemyCollider;
    private EnemyCurrency enemyCurrency;
    private Rigidbody2D rb;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyCollider = GetComponent<Collider2D>();
        enemyCurrency = GetComponent<EnemyCurrency>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            EnemyManager.Instance.RemoveEnemy(gameObject); 
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        if (enemyCollider != null)
        {
            enemyCollider.enabled = false;
        }

        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.AddScore(1);
        }

        if (enemyCurrency != null)
        {
            enemyCurrency.DropCoins();
        }

        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }

    public bool IsDead()
    {
        return isDead;
    }
}

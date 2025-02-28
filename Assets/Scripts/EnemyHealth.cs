using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 20;
    protected int currentHealth;
    protected Animator animator;
    protected bool isDead = false;

    protected EnemyMovement enemyMovement;
    protected Collider2D enemyCollider;
    protected EnemyCurrency enemyCurrency;
    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyCollider = GetComponent<Collider2D>();
        enemyCurrency = GetComponent<EnemyCurrency>();
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            EnemyManager.Instance.RemoveEnemy(gameObject); 
            Die();
        }
    }

    protected virtual void Die()
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

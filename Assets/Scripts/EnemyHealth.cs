using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 20;
    private int currentHealth;
    private Animator animator;
    private bool isDead = false;

    private EnemyMovement enemyMovement;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
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

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");

        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.AddScore(1);
        }

        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }
}

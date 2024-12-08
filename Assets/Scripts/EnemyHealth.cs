using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 20;
    private int currentHealth;
    private Animator animator;
    private bool isDead = false;

    public Transform respawnPoint;  // Ponto de respawn do inimigo
    public float speedIncreaseAmount = 1f;  // A quantidade a ser aumentada na velocidade a cada morte
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

        IncreaseSpeed();

        animator.SetTrigger("Respawn");
    }

    private void IncreaseSpeed()
    {
        enemyMovement.moveSpeed += speedIncreaseAmount;
    }
}

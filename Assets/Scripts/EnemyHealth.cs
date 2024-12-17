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

        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.AddScore(1);
        }

        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        // Aguarda até que a animação de morte termine
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Die") &&
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null; // Espera o próximo frame
        }

        yield return new WaitForSeconds(0.3f);

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

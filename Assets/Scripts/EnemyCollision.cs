using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public int damageToPlayer = 10;

    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null && collision.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage(damageToPlayer);
        }
    }
}

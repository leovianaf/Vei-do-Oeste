using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public int damageToPlayer = 100;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Health playerHealth = collision.gameObject.GetComponent<Health>();

        if (playerHealth != null && collision.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage(damageToPlayer);
            Destroy(gameObject);
        }
    }
}

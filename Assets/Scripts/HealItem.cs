using UnityEngine;

public class HealItem : MonoBehaviour
{
    public int healAmount = 50;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Chama a função de cura do Player
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount);
            }

            Destroy(gameObject);
        }
    }
}

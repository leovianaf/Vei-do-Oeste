using UnityEngine;

public class DamageItem : MonoBehaviour
{
    public float damageBoost = 25f;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            // Chama a função de aumento de dano no Player
            Shooter playerShooter = other.GetComponent<Shooter>();
            if (playerShooter != null)
            {
                playerShooter.IncreaseDamage(damageBoost);;
            }

            Destroy(gameObject); // Destrói o item após ser pego
        }
    }
}

using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public int damageToPlayer = 50;
    public float damageCooldown = 1.0f;
    private float lastDamageTime;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            AudioSource playerAudioSource = collision.gameObject.GetComponent<AudioSource>();


            if (playerHealth != null)
            {
                if (Time.time >= lastDamageTime + damageCooldown)
                {
                    playerHealth.TakeDamage(damageToPlayer);
                    lastDamageTime = Time.time;
      
                    PlayDamageSound(playerAudioSource);
                }
            }
        }
    }

    private void PlayDamageSound(AudioSource audioSource)
    {
        if (audioSource != null && !audioSource.isPlaying) // Evita sobreposição de sons
        {
            audioSource.Play();
        }
    }
}

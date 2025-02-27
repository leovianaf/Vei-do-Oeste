using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f;
    public float lifetime = 3f;
    private int bulletDamage = 0;
    private Vector2 direction;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    public void SetDamage(int damage)
    {
        bulletDamage = damage;
    }

    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            AudioSource playerAudioSource = collision.GetComponent<AudioSource>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(bulletDamage);

                if (playerAudioSource != null)
                {
                    StartCoroutine(PlayDamageSoundWithDelay(0.1f, playerAudioSource));
                }
            }
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator PlayDamageSoundWithDelay(float delay, AudioSource audioSource)
    {
        yield return new WaitForSeconds(delay);
        if (audioSource != null && !audioSource.isPlaying) // Evita sobreposição de sons
        {
            audioSource.Play();
        }
    }
}

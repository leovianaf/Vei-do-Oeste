using UnityEngine;
using System.Collections;

public class EnemyCollision : MonoBehaviour
{
    public int damageToPlayer = 50;
    public float damageCooldown = 1.0f;
    private float lastDamageTime;

    public bool canStunPlayer = false;
    public float stunChance = 0.5f;

    private EnemyHealth enemyHealth;
    private Animator animator;

    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (enemyHealth != null && enemyHealth.IsDead()) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            AudioSource playerAudioSource = collision.gameObject.GetComponent<AudioSource>();

            animator.SetTrigger("Attack");

            if (playerHealth != null)
            {
                if (Time.time >= lastDamageTime + damageCooldown)
                {
                    playerHealth.TakeDamage(damageToPlayer);
                    lastDamageTime = Time.time;

                    if (playerAudioSource != null)
                    {
                        StartCoroutine(PlayDamageSoundWithDelay(0.1f, playerAudioSource));
                    }

                    // 50% de chance de atordoar o player, caso o inimigo tenha essa habilidade
                    if (canStunPlayer && Random.value <= stunChance)
                    {
                        playerMovement.Stun(1f);
                    }
                }
            }
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

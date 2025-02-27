using UnityEngine;
using System.Collections;

public class EnemyRangedAttack : MonoBehaviour
{
    public int damageToPlayer = 50;
    public float attackCooldown = 1.5f;
    public float attackRange = 5f;
    public Transform firePoint;
    public GameObject enemyBulletPrefab;
    public bool useProjectiles = true;

    private float lastAttackTime = 0f;
    private Animator animator;
    private EnemyHealth enemyHealth;
    private Transform playerTransform;
    private bool isAttacking = false;
    private AudioSource audioSource;

    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (enemyHealth.IsDead() || playerTransform == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // Só ataca se estiver na distância correta e respeitando o cooldown
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown && !isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        lastAttackTime = Time.time;

        animator.SetTrigger("Attack");
        animator.SetBool("isAttack", true);

        yield return new WaitForSeconds(0.3f); // Pequeno atraso antes do disparo

        if (useProjectiles)
        {
            ShootProjectile();
        }
        else
        {
            if (audioSource != null)
            {
                audioSource.Play();
            }

            PlayerHealth playerHealth = playerTransform.GetComponent<PlayerHealth>();
            AudioSource playerAudioSource = playerTransform.GetComponent<AudioSource>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageToPlayer);
                if (playerAudioSource != null)
                {
                    StartCoroutine(PlayDamageSoundWithDelay(0.1f, playerAudioSource));
                }
            }
        }

        yield return new WaitForSeconds(attackCooldown);
        animator.SetBool("isAttack", false);
        isAttacking = false;
    }

    void ShootProjectile()
    {
        if (enemyBulletPrefab == null || firePoint == null) return;

        GameObject bullet = Instantiate(enemyBulletPrefab, firePoint.position, Quaternion.identity);
        EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();

        if (bulletScript != null)
        {
            // Calcula a direção do player a partir do FirePoint
            Vector2 shootDirection = (playerTransform.position - firePoint.position).normalized;
            bulletScript.SetDirection(shootDirection);
            bulletScript.SetDamage(damageToPlayer);
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

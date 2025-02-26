using UnityEngine;
using System.Collections;

public class EnemyRangedAttack : MonoBehaviour
{
    public int damageToPlayer = 50;
    public float attackCooldown = 1.5f;  // Tempo entre ataques
    public float attackRange = 5f;       // Distância de ataque
    public Transform firePoint;          // Local de spawn do projétil
    public GameObject enemyBulletPrefab; // Prefab do projétil
    public bool useProjectiles = true;   // Se true, atira projéteis; se false, aplica dano direto

    private float lastAttackTime = 0f;
    private Animator animator;
    private EnemyHealth enemyHealth;
    private Transform playerTransform;

    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (enemyHealth.IsDead() || playerTransform == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // Só ataca se estiver na distância correta e respeitando o cooldown
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
        }
    }

    void Attack()
    {
        lastAttackTime = Time.time;  // Atualiza o tempo do último ataque
        animator.SetTrigger("Attack");
        animator.SetBool("isAttack", true);

        // Espera 0.3 segundos antes de disparar o projétil (para sincronizar com a animação)
        StartCoroutine(PerformAttack());
    }

    IEnumerator PerformAttack()
    {
        yield return new WaitForSeconds(0.3f);

        if (useProjectiles)
        {
            ShootProjectile();
        }
        else
        {
            // Se o ataque for direto, aplica dano no jogador
            PlayerHealth playerHealth = playerTransform.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageToPlayer);
            }
        }

        // Aguarda cooldown antes de permitir outro ataque
        yield return new WaitForSeconds(attackCooldown);
        animator.SetBool("isAttack", false);
    }

    void ShootProjectile()
    {
        if (enemyBulletPrefab == null || firePoint == null) return;

        GameObject bullet = Instantiate(enemyBulletPrefab, firePoint.position, Quaternion.identity);
        EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();

        if (bulletScript != null)
        {
            Vector2 shootDirection = firePoint.right;
            if (transform.localScale.x < 0) shootDirection *= -1; // Ajusta direção conforme flip
            bulletScript.SetDirection(shootDirection);
            bulletScript.SetDamage(damageToPlayer);
        }
    }
}

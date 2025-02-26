using UnityEngine;

public class EnemyAlienMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float stopDistance = 5f; // Distância para parar e atirar
    public float retreatDistance = 6.5f; // Distância para voltar a correr atrás do jogador

    private Transform playerPosition;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private EnemyHealth enemyHealth;
    private EnemyRangedAttack enemyRangedAttack;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyRangedAttack = GetComponent<EnemyRangedAttack>();

        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (playerPosition == null || enemyHealth.IsDead()) return;

        float distanceToPlayer = Vector2.Distance(transform.position, playerPosition.position);
        Vector2 direction = (playerPosition.position - transform.position).normalized;

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", rb.linearVelocity.magnitude);

        spriteRenderer.flipX = direction.x < 0;

        // Verifica se deve atirar ou se mover
        if (distanceToPlayer <= stopDistance)
        {
            rb.linearVelocity = Vector2.zero; // Para de andar
            enemyRangedAttack.enabled = true; // Ativa ataque à distância
        }
        else if (distanceToPlayer > retreatDistance)
        {
            enemyRangedAttack.enabled = false; // Desativa ataque à distância
        }
    }

    void FixedUpdate()
    {
        if (playerPosition == null || enemyHealth.IsDead())
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, playerPosition.position);

        if (distanceToPlayer > stopDistance)
        {
            Vector2 direction = (playerPosition.position - transform.position).normalized;
            rb.linearVelocity = direction * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}

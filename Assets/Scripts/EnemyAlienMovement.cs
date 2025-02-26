using UnityEngine;

public class EnemyAlienMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float stopDistance = 5f;
    public float retreatDistance = 6.5f;

    private Transform playerPosition;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private EnemyHealth enemyHealth;
    private EnemyRangedAttack enemyRangedAttack;
    private bool isFlipped = false;
    private bool isMoving = true;

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

        // Flip do inimigo
        if (direction.x < 0 && !isFlipped)
        {
            isFlipped = true;
            spriteRenderer.flipX = true;
            FlipFirePoint();
        }
        else if (direction.x > 0 && isFlipped)
        {
            isFlipped = false;
            spriteRenderer.flipX = false;
            FlipFirePoint();
        }

        // Se o inimigo estiver perto o suficiente, ele para antes de atirar
        if (distanceToPlayer <= stopDistance)
        {
            StopMoving();
        }
        else if (distanceToPlayer > retreatDistance)
        {
            isMoving = true;
        }
    }

    void FixedUpdate()
    {
        if (playerPosition == null || enemyHealth.IsDead())
        {
            StopMoving();
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, playerPosition.position);

        if (distanceToPlayer > stopDistance && isMoving)
        {
            Vector2 direction = (playerPosition.position - transform.position).normalized;
            rb.linearVelocity = direction * moveSpeed;
        }
        else
        {
            StopMoving();
        }
    }

    void StopMoving()
    {
        isMoving = false;
        rb.linearVelocity = Vector2.zero;
        animator.SetFloat("Speed", 0);
    }

    void FlipFirePoint()
    {
        if (enemyRangedAttack.firePoint != null)
        {
            Vector3 firePointScale = enemyRangedAttack.firePoint.localScale;
            firePointScale.x = Mathf.Abs(firePointScale.x) * (spriteRenderer.flipX ? -1 : 1);
            enemyRangedAttack.firePoint.localScale = firePointScale;
        }
    }
}

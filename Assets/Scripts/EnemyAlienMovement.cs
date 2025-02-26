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

        if (distanceToPlayer <= stopDistance)
        {
            StopMoving();
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

        if (distanceToPlayer > stopDistance)
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
        rb.linearVelocity = Vector2.zero;
        animator.SetFloat("Speed", 0);
    }

    void FlipFirePoint()
    {
        if (enemyRangedAttack.firePoint != null)
        {
            Vector3 localScale = enemyRangedAttack.firePoint.localScale;
            localScale.x = Mathf.Abs(localScale.x) * (spriteRenderer.flipX ? -1 : 1);
            enemyRangedAttack.firePoint.localScale = localScale;
        }
    }
}

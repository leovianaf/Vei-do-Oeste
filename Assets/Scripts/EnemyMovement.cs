using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Transform playerPosition;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private EnemyHealth enemyHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        if (playerPosition == null || enemyHealth.IsDead()) return;

        Vector2 direction = (playerPosition.position - transform.position).normalized;

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", rb.linearVelocity.magnitude);

        spriteRenderer.flipX = direction.x < 0;
    }

    void FixedUpdate()
    {
        if (playerPosition == null || enemyHealth.IsDead())
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction = (playerPosition.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }
}

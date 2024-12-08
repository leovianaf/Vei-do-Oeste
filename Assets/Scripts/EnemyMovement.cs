using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    private Vector2 currentDirection;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        ChangeDirection();
    }

    void Update()
    {
        animator.SetFloat("Speed", rb.linearVelocity.magnitude);

        // Controla o FlipX para a direcao horizontal
        if (currentDirection.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (currentDirection.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = currentDirection * moveSpeed;
    }

    private void ChangeDirection()
    {
        // Escolhe uma direcao aleatoria
        currentDirection = directions[Random.Range(0, directions.Length)];

        animator.SetFloat("Horizontal", currentDirection.x);
        animator.SetFloat("Vertical", currentDirection.y);

        // Muda a direcaoo apos um tempo aleatorio
        Invoke(nameof(ChangeDirection), Random.Range(2f, 5f));
    }
}

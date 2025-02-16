using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Transform playerPosition;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (playerPosition == null) return;

        // Calcula a dire��o em rela��o ao jogador
        Vector2 direction = (playerPosition.position - transform.position).normalized;

        // Atualiza a anima��o para indicar a dire��o
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", rb.linearVelocity.magnitude);

        // Controla o FlipX para inverter o sprite se necess�rio
        spriteRenderer.flipX = direction.x < 0;
    }

    void FixedUpdate()
    {
        if (playerPosition == null) return;

        // Move o inimigo na dire��o do jogador
        Vector2 direction = (playerPosition.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }
}

using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    public float moveSpeed = 4f;
    //private float baseMoveSpeed;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isStunned = false;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color; // Salva a cor original do jogador
        }
    }

    void Update()
    {

        if (isStunned) return;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        movement = new Vector2(horizontal, vertical).normalized;

        if (movement != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);

            // Reseta Horizontal e Vertical para garantir que o Blend Tree volte para Idle
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
        }
    }

    void FixedUpdate()
    {
        if (!isStunned)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    public void UpdateSpeed(float multiplier)
    {
        moveSpeed = moveSpeed * (1 + multiplier);
        Debug.Log($"Velocidade atualizada: {moveSpeed}");
    }

    public void Stun(float duration)
    {
        if (!isStunned)
        {
            StartCoroutine(StunCoroutine(duration));
        }
    }

    private IEnumerator StunCoroutine(float duration)
    {
        isStunned = true;
        animator.SetBool("isMoving", false);

        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(1f, 0.5f, 0f, 1f); // Laranja
        }

        yield return new WaitForSeconds(duration);

        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }

        isStunned = false;
    }

}

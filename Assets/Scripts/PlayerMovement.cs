using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 movement;
    private Vector2 currentDirection = Vector2.down;
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
        {
            movement.x = horizontal;
            movement.y = 0;
        }
        else
        {
            movement.x = 0;
            movement.y = vertical;
        }

        movement = movement.normalized;

        // Atualiza a direção se houver movimento
        if (movement != Vector2.zero)
        {
            currentDirection = movement;
        }

        // Atualiza os parâmetros do Animator
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetBool("isMoving", movement != Vector2.zero);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public Vector2 GetCurrentDirection()
    {
        return currentDirection;
    }
}

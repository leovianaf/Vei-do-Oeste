using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;
    private Animator animator;
    private bool isExploding = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Se colidir com uma parede, exploda
        if (collision.gameObject.CompareTag("Wall"))
        {
            Explode();
            return;
        }

        // Se colidir com um objeto que tem o script de vida, aplique dano
        EnemyHealth targetHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
            Explode();
        }
    }

    private void Explode()
    {
        if (isExploding) return;

        isExploding = true;

        if (animator != null)
        {
            animator.SetTrigger("Explode");
        }

        // Desativa o movimento e colis�o da bala
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // Destroi o objeto ap�s a anima��o (tempo ajustado ao clipe de explos�o)
        Destroy(gameObject, 0.5f);
    }
}

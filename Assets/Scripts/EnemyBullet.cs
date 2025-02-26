using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f; // Velocidade do projétil
    public int damage = 50; // Dano ao jogador
    public float lifetime = 3f; // Tempo antes do projétil desaparecer

    private Vector2 direction;

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroi a bala após um tempo
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject); // Destroi a bala ao atingir o jogador
        }
        else if (collision.CompareTag("Obstacle")) // Caso bata em obstáculos, some
        {
            Destroy(gameObject);
        }
    }
}

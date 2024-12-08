using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Se colidir com uma parede, destrua a bala
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
            return;
        }

        // Se colidir com um objeto que tem o script de vida, aplique dano
        Health targetHealth = collision.gameObject.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}

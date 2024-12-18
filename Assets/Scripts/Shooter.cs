using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;

    private PlayerMovement playerMovement;
    private Animator animator;

    public float shootCooldown = 0.5f;  // Tempo de cooldown entre os tiros
    private float lastShootTime = 0f;    // Armazena o tempo do último tiro
    public float shootDelay = 0.2f;     // Delay para a bala sair após pressionar o botão

    private float damageBase = 25f; // Dano base da bala
    private float damageBoost = 0f; // Aumento de dano temporário, começa em 0

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>(); // Obtem referência ao PlayerMovement
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.time - lastShootTime >= shootCooldown && Input.GetKeyDown(KeyCode.J))
        {
            Shoot();
            lastShootTime = Time.time;
        }
    }

    void Shoot()
    {
        animator.SetTrigger("Shoot");

        // Inicia uma corrotina para aplicar o delay no disparo
        StartCoroutine(ShootWithDelay());
    }

    IEnumerator ShootWithDelay()
    {
        // Aguarda o tempo do delay antes de instanciar a bala
        yield return new WaitForSeconds(shootDelay);

        // Obtém a direção atual do movimento do jogador
        Vector2 shootDirection = playerMovement.GetCurrentDirection();

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = shootDirection * bulletSpeed;
        }

        // Modifica o dano da bala com o multiplicador de dano
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDamage(damageBase + damageBoost);
        }

        // Calcula o ângulo da direção do disparo
        if (shootDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

            // Aplica a rotação à bala
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        // Após o disparo, resetar o aumento de dano
        ResetDamageBoost();
    }

    public void IncreaseDamage(float boostAmount)
    {
        damageBoost = boostAmount;

        // Definir um limite para o aumento de dano
        if (damageBoost > 25f)
        {
            damageBoost = 25f;
        }
    }

    // Resetar o aumento de dano após o disparo
    private void ResetDamageBoost()
    {
        damageBoost = 0f; // Reseta o aumento de dano de volta para 0
    }
}

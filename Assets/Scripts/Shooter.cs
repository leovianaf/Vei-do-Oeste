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
    }
}

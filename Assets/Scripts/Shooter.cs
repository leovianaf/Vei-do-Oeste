using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    private Vector2 shootDirection = Vector2.right;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) shootDirection = Vector2.up;
        if (Input.GetKeyDown(KeyCode.S)) shootDirection = Vector2.down;
        if (Input.GetKeyDown(KeyCode.A)) shootDirection = Vector2.left;
        if (Input.GetKeyDown(KeyCode.D)) shootDirection = Vector2.right;

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = shootDirection * bulletSpeed;
        }
    }
}

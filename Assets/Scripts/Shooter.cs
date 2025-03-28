using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Shooter : MonoBehaviour
{
    public static Shooter instance;
    //public GameObject bulletPrefab;
    public Transform firePoint;
    public GameObject mouseAim;
    //public float bulletSpeed = 10f;
    //public float shootCooldown = 0.5f;  // Tempo de cooldown entre os tiros
    [HideInInspector] public float bulletSpeed;
    [HideInInspector] public float shootCooldown;
    [SerializeField] private AudioSource reloadSound;
    public GameObject bulletPrefab;
    private float lastShootTime = 0f;    // Armazena o tempo do último tiro
    public float shootDelay = 0.2f;     // Delay para a bala sair após pressionar o botão

    private float damageBoost = 0f; // Aumento de dano temporário, começa em 0

    private Animator animator;
    private Camera mainCamera;
    private PlayerMovement playerMovement;

    private int lastShootDirection = 2; // 2 = Baixo por padrão
    private bool isShooting = false; // Controla o estado da camada Shooting
    private int currentAmmo;
    private int maxAmmo = 30;  // Munição inicial máxima
    private bool isReloading = false;

    public TMP_Text ammoText; // UI de munição
    [SerializeField] private GameManager gameManager;
    public GameObject ammoLoading;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;
        playerMovement = GetComponent<PlayerMovement>(); // Referência ao movimento do player

        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    void Update()
    {
        // Bloqueia o tiro se qualquer painel estiver aberto
        if(SceneManager.GetActiveScene().name == "GameScene"){
            if (ShopManager.IsShopOpen || InventoryManager.IsInventoryOpen || UpgradeShopManager.IsUpgradeShopOpen || gameManager.isInShop)
            {
                animator.SetBool("Shoot", false); // Reseta a animação
                mouseAim.gameObject.SetActive(false);
                currentAmmo = PlayerWeapon.instance.currentWeapon.maxBullets;
                maxAmmo = PlayerWeapon.instance.currentWeapon.maxBullets;
                UpdateAmmoUI();
                return;
            }

            if(!gameManager.isInShop){
                mouseAim.gameObject.SetActive(true);
            }
        }
        
        
        // Verifica se o jogador pode atirar
        if (Time.time - lastShootTime >= shootCooldown && Input.GetMouseButtonDown(0)) // Botão esquerdo do mouse
        {
            if (currentAmmo >= 0 && !isReloading)
            {
                SetShootDirection();
                Shoot();
                lastShootTime = Time.time;
            }
            else
            {
                Debug.Log("Sem munição! Pressione 'R' para recarregar.");
            }
        }

        if (currentAmmo == 0 && !isReloading)
        {
            StartCoroutine(Reload(PlayerWeapon.instance.currentWeapon.reloadTime));
        }

        // Atualiza o peso da camada Shooting corretamente
        bool isShootingNow = animator.GetBool("Shoot");
        if (isShootingNow != isShooting) // Evita setar repetidamente o mesmo valor
        {
            animator.SetLayerWeight(1, isShootingNow ? 1 : 0);
            isShooting = isShootingNow;
        }
    }

    void SetShootDirection()
    {
        float horizontal = animator.GetFloat("Horizontal");
        float vertical = animator.GetFloat("Vertical");

        if (horizontal != 0 || vertical != 0) // Atualiza apenas se houver movimento
        {
            if (vertical > 0) lastShootDirection = 1; // Cima
            else if (vertical < 0) lastShootDirection = 2; // Baixo
            else if (horizontal < 0) lastShootDirection = 3; // Esquerda
            else if (horizontal > 0) lastShootDirection = 4; // Direita
        }

        // Inicia uma corrotina para aplicar o delay no disparo
        animator.SetInteger("ShootDirection", lastShootDirection);
    }

    void Shoot()
    {
        animator.SetBool("Shoot", true);
        StartCoroutine(ShootWithDelay());
    }

    IEnumerator ShootWithDelay()
    {
        // Aguarda o tempo do delay antes de instanciar a bala
        yield return new WaitForSeconds(shootDelay);

        // Calcula a direção do tiro baseado na posição do mouse
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDirection = (mousePosition - (Vector2)firePoint.position).normalized;

        // Instancia a bala
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Modifica o dano da bala com o multiplicador de dano
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            float totalDamage = PlayerWeapon.instance.currentWeapon.damage + damageBoost;
            bulletScript.SetDamage(totalDamage);
        }

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = shootDirection * bulletSpeed;
        }

        // Rotaciona a bala para a direção correta
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Consome munição
        currentAmmo -= 1;
        UpdateAmmoUI();

        // Após o disparo, resetar o aumento de dano
        ResetDamageBoost();

        yield return new WaitForSeconds(0.1f);
        animator.SetBool("Shoot", false);
    }

    public void IncreaseDamage(float boostAmount)
    {
        // Definir um limite para o aumento de dano
        damageBoost = Mathf.Min(damageBoost + boostAmount, 25f);
    }

    // Resetar o aumento de dano após o disparo
    private void ResetDamageBoost()
    {
        damageBoost = 0f; // Reseta o aumento de dano de volta para 0
    }

    IEnumerator Reload(int reloadTime)
    {
        ammoLoading.SetActive(true);
        ammoText.gameObject.SetActive(false);
        isReloading = true;
        Debug.Log("Recarregando...");
        
        if (reloadSound != null)
            reloadSound.Play();
        
        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        UpdateAmmoUI();
        ammoLoading.SetActive(false);
        ammoText.gameObject.SetActive(true);
        Debug.Log("Recarga concluída!");
    }

    public void ReloadAmmo()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
        Debug.Log("Munição recarregada no respawn!");
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = currentAmmo + "/" + maxAmmo;
        }
    }

    public void UpdateWeaponStats(Weapon weapon)
    {
        shootCooldown = weapon.fireRate;
        bulletPrefab = weapon.bulletPrefab;
        bulletSpeed = weapon.bulletSpeed;        
    }
}

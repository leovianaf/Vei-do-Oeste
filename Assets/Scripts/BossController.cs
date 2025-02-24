using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    public float moveSpeed = 4f;
    public int damageCloseRange = 40;  // Dano do ataque de curta distância
    public int damageMediumRange = 60;
    public float attackInterval = 2f;  // Tempo entre ataques

    private Transform playerPosition;
    private Rigidbody2D rb;
    private Animator animator;
    public int currentPhase = 1; // 1 = Fase 1, 2 = Fase 2
    private bool isAttacking = false;
    private SpriteRenderer spriteRenderer;
    private int nextAttackType = 1; // Alterna entre 1 e 2
    private float lastAttackTime = 0f; // Marca o tempo do último ataque

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

        animator.SetFloat("Horizontal", playerPosition.position.x - transform.position.x);
        animator.SetFloat("Vertical", playerPosition.position.y - transform.position.y);
        animator.SetFloat("Speed", rb.linearVelocity.magnitude);

        if (!isAttacking)
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (playerPosition.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isAttacking && Time.time >= lastAttackTime + attackInterval)
        {
            int attackType = (currentPhase == 2) ? nextAttackType : 1; // Alterna entre 1 e 2 na fase 2
            Attack(attackType, collision);
            if (currentPhase == 2)
            {
                nextAttackType = (nextAttackType == 1) ? 2 : 1;
            }
            lastAttackTime = Time.time;
        }
    }

    private void Attack(int attackType, Collision2D collision)
    {
        if (isAttacking) return; // Evita múltiplos ataques simultâneos

        isAttacking = true;
        animator.SetLayerWeight(1, 1);
        animator.SetInteger("AttackType", attackType);
        animator.SetTrigger("Attack");

        DealDamageToPlayer(attackType, collision);
    }

    private void DealDamageToPlayer(int attackType, Collision2D collision)
    {
        PlayerHealth playerHealth = playerPosition.GetComponent<PlayerHealth>();
        AudioSource playerAudioSource = collision.gameObject.GetComponent<AudioSource>();

        if (playerHealth != null)
        {
            int damage = attackType == 1 ? damageCloseRange : damageMediumRange;
            playerHealth.TakeDamage(damage);

            if (playerAudioSource != null)
            {
                StartCoroutine(PlayDamageSoundWithDelay(0.1f, playerAudioSource));
            }

            Debug.Log($"Ataque {attackType} infligiu {damage} de dano.");
        }
    }

    public void EndAttack()
    {
        isAttacking = false;
        animator.SetLayerWeight(1, 0);
        animator.ResetTrigger("Attack");
        rb.linearVelocity = Vector2.zero;
    }

    public void EnterPhase2()
    {
        currentPhase = 2;
        spriteRenderer.color = new Color(1f, 0.5f, 0f, 1f);
        Debug.Log("Entrou na fase 2!");
    }

    public void TakeDamageEffect()
    {
        StartCoroutine(FlashRed());
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = currentPhase == 2 ? new Color(1f, 0.5f, 0f, 1f) : Color.white;
    }

    private IEnumerator PlayDamageSoundWithDelay(float delay, AudioSource audioSource)
    {
        yield return new WaitForSeconds(delay);
        if (audioSource != null && !audioSource.isPlaying) // Evita sobreposição de sons
        {
            audioSource.Play();
        }
    }
}

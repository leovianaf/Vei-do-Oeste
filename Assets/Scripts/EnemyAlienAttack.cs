using UnityEngine;
using System.Collections;

public class EnemyRangedAttack : MonoBehaviour
{
    public int damageToPlayer = 50;
    public float attackCooldown = 1.5f;
    public float attackRange = 5f; // Distância de ataque
    public Transform firePoint; // Ponto de origem do ataque (pode ser a ponta da arma)
    
    private float lastAttackTime;
    private Animator animator;
    private EnemyHealth enemyHealth;
    
    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (enemyHealth != null && enemyHealth.IsDead()) return;
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            
            if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
            {
                Attack(player);
            }
        }
    }
    
    void Attack(GameObject player)
    {
        animator.SetTrigger("Attack");
        
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageToPlayer);
            lastAttackTime = Time.time;
        }
    }
}
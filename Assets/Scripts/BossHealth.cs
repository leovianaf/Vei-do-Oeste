using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BossHealth : EnemyHealth
{
    private BossController bossController;


    protected override void Start()
    {
        base.Start();  // Reutiliza a l�gica base do EnemyHealth
        bossController = GetComponent<BossController>();
    }

    public override void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log("Vida atual do Boss: " + currentHealth);

        bossController.TakeDamageEffect();

        if (currentHealth <= 250 && bossController != null && bossController.currentPhase == 1)
        {
            bossController.EnterPhase2();
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    protected override void Die()
    {
        base.Die(); // Reutiliza a l�gica de EnemyHealth
        GameWin.Instance.LoadWinScene();
        Debug.Log("Boss derrotado!");
    }


}

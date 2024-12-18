using UnityEngine;
using System.Collections;

public class RandomEnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemiesPrefab;

    public float initialSpawnInterval = 7f;
    public int maxEnemiesPerSpawn = 3;
    public float minSpawnInterval = 4f;
    public float spawnIntervalReductionRate = 0.5f;
    private float currentSpawnInterval;

    private int enemiesPerSpawn = 1;
    private float elapsedTime = 0f;

    public float speedIncreaseInterval = 15f;
    public float speedIncreaseAmount = 1f;
    public float maxSpeed = 10f;

    private float timeSinceLastSpeedIncrease = 0f;

    void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            UpdateSpawnLogic();

            // Spawna inimigos de acordo com a lógica atual
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
                int randomEnemy = Random.Range(0, enemiesPrefab.Length);

                GameObject enemy = Instantiate(enemiesPrefab[randomEnemy], spawnPoints[randomSpawnPoint].position, Quaternion.identity);

                // Aumenta a velocidade do inimigo
                EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
                if (enemyMovement != null)
                {
                    enemyMovement.moveSpeed = Mathf.Min(enemyMovement.moveSpeed + speedIncreaseAmount, maxSpeed);
                }
            }

            // Espera o intervalo atual antes de continuar
            yield return new WaitForSeconds(currentSpawnInterval);
        }
    }


    private void UpdateSpawnLogic()
    {
        elapsedTime += Time.deltaTime;

        // Atualiza a quantidade de inimigos por spawn com base no tempo decorrido
        if (elapsedTime >= 90f) // Após 1:30
        {
            enemiesPerSpawn = Mathf.Min(maxEnemiesPerSpawn, 3);
        }
        else if (elapsedTime >= 30f) // Após 30 segundos
        {
            enemiesPerSpawn = Mathf.Min(maxEnemiesPerSpawn, 2);
        }

        // Reduz o intervalo de spawn após 2:30
        if (elapsedTime >= 150f) // 2:30
        {
            currentSpawnInterval = Mathf.Max(minSpawnInterval, currentSpawnInterval - spawnIntervalReductionRate * Time.deltaTime);
        }

        // Aumenta a velocidade dos inimigos a cada 15 segundos, até um limite máximo
        timeSinceLastSpeedIncrease += Time.deltaTime;
        if (timeSinceLastSpeedIncrease >= speedIncreaseInterval)
        {
            // Aumenta a velocidade de todos os inimigos ativos
            timeSinceLastSpeedIncrease = 0f;  // Reseta o contador
            speedIncreaseAmount = Mathf.Min(speedIncreaseAmount + 1f, maxSpeed); // Aumenta a velocidade até o limite
        }
    }
}


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawn; 
    [SerializeField] public int enemies; 
    [SerializeField] private GameObject[] enemy;
    [SerializeField] private float spawnDelay = 0.5f;

    private void Start()
    {
        StartCoroutine(DelayedSpawns());
    }

    private void SpawnEnemies()
    {
        int randomIndex = Random.Range(0, spawn.Length);

        int randomIndexEnemy = Random.Range(0, enemy.Length);
        GameObject enemyPrefab = enemy[randomIndexEnemy];
        
        GameObject enemySpawned = Instantiate(enemyPrefab, spawn[randomIndex].transform.position, spawn[randomIndex].transform.rotation);
        EnemyManager.Instance.AddEnemy(enemySpawned);


    }

    IEnumerator DelayedSpawns()
    {
        for (int i = 0; i < enemies; i++)
        {
            SpawnEnemies();
            yield return new WaitForSeconds(spawnDelay);
        }
    }


}

using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance; 
    [SerializeField] private string enemyTag = "Enemy"; 

    private List<GameObject> activeEnemies = new List<GameObject>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        GameObject[] initialEnemies = GameObject.FindGameObjectsWithTag(enemyTag);
        activeEnemies.AddRange(initialEnemies);
    }


    public void AddEnemy(GameObject enemy)
    {
        activeEnemies.Add(enemy);
    }


    public void RemoveEnemy(GameObject enemy)
    {
        activeEnemies.Remove(enemy);

        if (activeEnemies.Count == 0)
        {
            GameManager.Instance.LoadNextMap();
        }
    }
}
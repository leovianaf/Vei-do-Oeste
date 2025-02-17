using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance; 
    [SerializeField] private string enemyTag = "Enemy";
    [SerializeField] private Slider progressSlider;

    private List<GameObject> activeEnemies = new List<GameObject>();
    private int totalEnemiesInRoom;
    private int enemiesKilled;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void InitializeProgress(int totalEnemies)
    {
        totalEnemiesInRoom = totalEnemies;
        enemiesKilled = 0;
        progressSlider.maxValue = totalEnemies;
        progressSlider.value = 0;
    }

    public void AddEnemy(GameObject enemy)
    {
        activeEnemies.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
        enemiesKilled++;
        UpdateProgressBar();

        if (enemiesKilled == totalEnemiesInRoom)
        {
            GameManager.Instance.LoadNextMap();
        }
    }

    void UpdateProgressBar()
    {
        progressSlider.value = enemiesKilled;
    }
}
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance; 
    [SerializeField] private string enemyTag = "Enemy";
    [SerializeField] private TextMeshProUGUI enemyText;

    [SerializeField] private bool isDesert = false;

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
    }

    public void AddEnemy(GameObject enemy)
    {
        activeEnemies.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
        enemiesKilled++;
        UpdateProgress();

        if (enemiesKilled == totalEnemiesInRoom)
        {
            if(isDesert){
                StartCoroutine(LoadCutScene());
            }
            GameManager.Instance.LoadNextMap();
        }
    }

    void UpdateProgress()
    {
        enemyText.text = enemiesKilled+"/10";
    }


    private IEnumerator LoadCutScene()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("CutScene");

    }
}
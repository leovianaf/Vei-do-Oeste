using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] mapPrefabs;
    [SerializeField] private GameObject mapBossPrefab;
    [SerializeField] private Transform player; 
    [SerializeField] private string playerSpawnTag = "PlayerSpawner";
    [SerializeField] private CameraController cameraController;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject shopSpawn;
    [SerializeField] private GameObject diaryItem;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private GameObject currentMap;

    [SerializeField] private int mapsPlayed = 0;

    private int enemiesToSpawn = 10;

    public static GameManager Instance;
    [SerializeField] private float delayBeforeNewMap = 2f;

    public bool isInShop = false;

    [Header("UI para desativar")]
    [SerializeField] private GameObject[] shopUI;

     void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {      
        LoadRandomMap();

        if (CameraController.instance == null)
            CameraController.instance = cameraController;

        if (PlayerHealth.instance == null)
            PlayerHealth.instance = playerHealth;

        if (PlayerMovement.instance == null)
            PlayerMovement.instance = playerMovement;
    }

 
    public void LoadNextMap()
    {
        mapsPlayed++;
        if(mapsPlayed >= 2){

            if (currentMap != null)
            {
                Destroy(currentMap);
                currentMap = null;
            }
            currentMap = Instantiate(mapBossPrefab, Vector3.zero, Quaternion.identity);

            Transform[] children = currentMap.GetComponentsInChildren<Transform>(includeInactive: true);

            Transform t = FindWithTag(children, "PlayerSpawner");
            
            Transform playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawner").transform;

            if (playerSpawn != null && player != null)
            {
                player.position = t.position;
                player.rotation = t.rotation;
            }

            return;
        }
        StartCoroutine(LoadNewMapAfterDelay());
    }

    private IEnumerator LoadNewMapAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeNewMap);
        LoadRandomMap();

    }


    public void LoadRandomMap()
    {
        if (currentMap != null)
        {
            Destroy(currentMap);
            currentMap = null;
        }

        int randomIndex = Random.Range(0, mapPrefabs.Length);
        currentMap = Instantiate(mapPrefabs[randomIndex], Vector3.zero, Quaternion.identity);

        currentMap.GetComponent<EnemySpawner>().enemies = enemiesToSpawn;

        Transform[] children = currentMap.GetComponentsInChildren<Transform>(includeInactive: true);

        Transform t = FindWithTag(children, "PlayerSpawner");
        
        Transform playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawner").transform;
        Debug.Log("spawn position: " + t.position);
        if (playerSpawn != null && player != null)
        {
            player.position = t.position;
            player.rotation = t.rotation;
        }
      
    }

    Transform FindWithTag(Transform[] children, string tag){
        foreach (Transform t in children)
        {
            if (t.CompareTag(tag)){
                return t;
            }
        }
        return null;
    }


    public void OnPlayerDeath() {
        player.transform.position = shopSpawn.transform.position;
        Destroy(currentMap);

        EnemyManager enemyManager = GetComponent<EnemyManager>();
        spawnedEnemies = enemyManager.activeEnemies;

        foreach (var enemy in spawnedEnemies) {
            Destroy(enemy);
        }
        spawnedEnemies.Clear();

        foreach (var gameObjectUI in shopUI){
            gameObjectUI.SetActive(false);
        }
        isInShop = true;
        mapsPlayed = 0;

        GameState.hasOpenedDiary = false;

        diaryItem.SetActive(true);
    }

    public void LoadUI(){
        foreach (var gameObjectUI in shopUI){
            gameObjectUI.SetActive(true);
        }
    }

}
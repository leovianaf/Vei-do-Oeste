using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] mapPrefabs;
    [SerializeField] private Transform player; 
    [SerializeField] private string playerSpawnTag = "PlayerSpawner";

    private GameObject currentMap;

    private int mapsPlayed = 0;

    private int enemiesToSpawn = 10;

    public static GameManager Instance;
    [SerializeField] private float delayBeforeNewMap = 2f;

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
    }

 
    public void LoadNextMap()
    {
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

}
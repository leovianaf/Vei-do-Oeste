using UnityEngine;
using UnityEngine.SceneManagement;

public class DiaryItem : MonoBehaviour
{
    private bool isPlayerNearby = false;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            StartCutscene();
        }
    }

    private void StartCutscene()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            GameState.lastPlayerPosition = player.transform.position;
            GameState.shouldRestorePosition = true;
        }
        SceneManager.LoadScene("DiaryCutscene");
    }

    void Start()
    {
        if (GameState.shouldRestorePosition)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = GameState.lastPlayerPosition;
                GameState.shouldRestorePosition = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}

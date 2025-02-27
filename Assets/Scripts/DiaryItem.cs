using UnityEngine;
using UnityEngine.SceneManagement;

public class DiaryItem : MonoBehaviour
{
    private bool isPlayerNearby = false;
    public GameObject diaryText;
    public GameObject diaryDialogueBox;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            StartCutscene();
        }
    }

    private void StartCutscene()
    {
        GameState.hasOpenedDiary = true;

        gameObject.SetActive(false);
        diaryDialogueBox.SetActive(true);
    }

    void Start()
    {
        if (GameState.hasOpenedDiary && GameState.diaryDialogueIndex >= 6)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }

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

    public void ReturnToPreviousScene()
    {
        SceneManager.LoadScene(GameState.previousScene);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            diaryText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            diaryText.SetActive(false);
        }
    }
}

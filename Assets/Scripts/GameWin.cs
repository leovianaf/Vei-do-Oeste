using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameWin : MonoBehaviour
{

    public GameObject gameWinUI;
    public GameObject generalCanvasUI;

    public static GameWin Instance; 
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    public void Win()
    {
        gameWinUI.SetActive(true);
        generalCanvasUI.SetActive(false);
        Time.timeScale = 0f;
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void LoadWinScene()
    {
        StartCoroutine(WinScene());
    }

    private IEnumerator WinScene()
    {

        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("WinScene");

    }
}

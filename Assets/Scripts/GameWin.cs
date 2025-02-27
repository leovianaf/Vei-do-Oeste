using UnityEngine;
using UnityEngine.EventSystems;

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
}

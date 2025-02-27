using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public void LoadNextScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}

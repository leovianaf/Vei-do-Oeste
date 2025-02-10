using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public string nextSceneName; // Nome da próxima cena

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}

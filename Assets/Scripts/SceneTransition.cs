using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance;
    private CanvasGroup canvasGroup;
    public float fadeDuration = 1.5f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Mantém a transição entre cenas
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Tenta encontrar o CanvasGroup automaticamente
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogWarning("CanvasGroup não encontrado no mesmo GameObject! Tentando encontrar na cena...");

            GameObject canvasObj = GameObject.Find("CanvasTransicao"); // Nome exato do Canvas na Hierarquia
            if (canvasObj != null)
            {
                canvasGroup = canvasObj.GetComponent<CanvasGroup>();
            }
        }

        if (canvasGroup == null)
        {
            Debug.LogError("Erro: CanvasGroup ainda não foi encontrado! Certifique-se de que há um Canvas de transição na cena.");
        }
    }

    public void FadeOutToScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeOut(string sceneName)
    {
        float time = 0;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, time / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1;
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeIn()
    {
        float time = 0;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, time / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0;
    }
}

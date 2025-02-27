using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject advanceDialogueText;
    public TextMeshProUGUI textComponent;
    public TMP_FontAsset customFont;
    public string[] lines;
    public float textSpeed;

    private int currentLineIndex;
    private DiaryItem diaryItem;

    [SerializeField] public string[] diaryDialoguesSet1;
    [SerializeField] public string[] diaryDialoguesSet2;
    [SerializeField] public string[] diaryDialoguesSet3;
    [SerializeField] public string[] diaryDialoguesSet4;
    [SerializeField] public string[] diaryDialoguesSet5;
    [SerializeField] public string[] diaryDialoguesSet6;

    void Start()
    {
        diaryItem = FindObjectOfType<DiaryItem>();
        Time.timeScale = 0f;

        dialoguePanel.SetActive(true);
        advanceDialogueText.SetActive(true);

        textComponent.text = string.Empty;
        textComponent.font = customFont;

        if (SceneManager.GetActiveScene().name == "DiaryCutscene")
        {
            if (GameState.diaryDialogueIndex == 0)
            {
                lines = diaryDialoguesSet1;
            }
            else if (GameState.diaryDialogueIndex == 1)
            {
                lines = diaryDialoguesSet2;
            }
            else if (GameState.diaryDialogueIndex == 2)
            {
                lines = diaryDialoguesSet3;
            }
            else if (GameState.diaryDialogueIndex == 3)
            {
                lines = diaryDialoguesSet4;
            }
            else if (GameState.diaryDialogueIndex == 4)
            {
                lines = diaryDialoguesSet5;
            }
            else if (GameState.diaryDialogueIndex == 5)
            {
                lines = diaryDialoguesSet6;
            }
            else
            {
                SceneManager.LoadScene(GameState.previousScene);
            }

            GameState.diaryDialogueIndex++;
        }

        StartDialogue();
    }

    void Update()
    {
        if (lines == null || lines.Length == 0) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (textComponent.text == lines[currentLineIndex])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[currentLineIndex];
            }
        }
    }

    void StartDialogue()
    {
        currentLineIndex = 0;
        StartCoroutine(WriteText());
    }

    IEnumerator WriteText()
    {
        foreach (char c in lines[currentLineIndex].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }

    void NextLine()
    {
        if (currentLineIndex < lines.Length - 1)
        {
            currentLineIndex++;
            textComponent.text = string.Empty;
            StartCoroutine(WriteText());
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue() {
        gameObject.SetActive(false);
        dialoguePanel.SetActive(false);
        advanceDialogueText.SetActive(false);
        Time.timeScale = 1f;

        if (SceneManager.GetActiveScene().name == "DiaryCutscene")
        {
            SceneManager.LoadScene(GameState.previousScene);
        }
    }
}
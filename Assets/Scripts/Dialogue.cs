using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI textComponent;
    public TMP_FontAsset customFont;
    public string[] lines;
    public float textSpeed;

    private int currentLineIndex;

    void Start()
    {
        Time.timeScale = 0f;

        dialoguePanel.SetActive(true);

        textComponent.text = string.Empty;
        textComponent.font = customFont;

        StartDialogue();
    }

    void Update()
    {
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
            Debug.Log(c);
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
        Time.timeScale = 1f;
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem {
    public class DialogueLine: DialogueBase
    {
        private Text textHolder;

        [Header ("Text Options")]
        [SerializeField] private string input;
        [SerializeField] private Color textColor;
        [SerializeField] private Font textFont;

        [Header ("Time Paramaeters")]
        [SerializeField] private float delay;

        public void Awake()
        {
            textHolder = GetComponent<Text>();

            StartCoroutine(WriteText(input, textHolder, textColor, textFont, delay));
        }
    }
}
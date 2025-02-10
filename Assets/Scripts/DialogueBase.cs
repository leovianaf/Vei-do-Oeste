using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueBase : MonoBehaviour
    {
        protected IEnumerator WriteText(string input, Text textholder, Color textColor, Font textFont, float delay)
        {
            textholder.color = textColor;
            textholder.font = textFont;

            for (int i = 0; i < input.Length; i++)
            {
                textholder.text += input[i];
                yield return new WaitForSeconds(delay);
            }
        }
    }
}
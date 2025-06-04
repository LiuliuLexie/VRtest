using System.Collections;
using UnityEngine;
using TMPro;

public class ChildDialogueUI1 : MonoBehaviour
{
    public static ChildDialogueUI1 Instance;

    public TextMeshProUGUI dialogueText;
    public float sentenceDelay = 1f; 

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowDialogueSequence(string[] lines)
    {
        StopAllCoroutines();
        StartCoroutine(TypeLines(lines));
    }

    public void ShowSingleLine(string line)
    {
        StopAllCoroutines();
        dialogueText.text = line;
    }

    public void ClearDialogue()
    {
        dialogueText.text = "";
    }

    private IEnumerator TypeLines(string[] lines)
    {
        foreach (string line in lines)
        {
            dialogueText.text = line;
            yield return new WaitForSeconds(sentenceDelay);
        }
    }
}

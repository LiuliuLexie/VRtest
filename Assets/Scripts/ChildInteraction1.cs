using UnityEngine;
using TMPro;

public class ChildInteraction1 : MonoBehaviour
{
    [Header("Matching Logic")]
    public int requiredMelodyID;

    [Header("Gaze Text UI")]
    public TextMeshProUGUI gazeText;

    [Header("Dialogue Content")]
    public string gazeLine = "Can you... hear me?";
    public string correctResponse = "Thank you. I remembered... just a little. As thanks, best wishes for you.";
    public string wrongResponse = "This is... my sound?";

    private bool hasTriggered = false;

    public void TriggerDialogue()
    {
        if (hasTriggered) return;
        hasTriggered = true;

        if (gazeText != null)
        {
            gazeText.text = gazeLine;
            gazeText.gameObject.SetActive(true);
            Invoke(nameof(HideText), 2f); 
        }

        Debug.Log("Triggered child dialogue: " + gazeLine);

        MatchManager1.Instance.ReadyToMatch(this);
    }

    public void RespondToChime(int givenMelodyID)
    {
        if (givenMelodyID == requiredMelodyID)
        {
            if (gazeText != null)
            {
                gazeText.text = correctResponse;
                gazeText.gameObject.SetActive(true);
            }
            MatchManager1.Instance.RegisterMatch(this, true);
        }
        else
        {
            if (gazeText != null)
            {
                gazeText.text = wrongResponse;
                gazeText.gameObject.SetActive(true);
            }
            MatchManager1.Instance.RegisterMatch(this, false);
        }
    }

    private void HideText()
    {
        if (gazeText != null)
        {
            gazeText.gameObject.SetActive(false);
        }
    }
}

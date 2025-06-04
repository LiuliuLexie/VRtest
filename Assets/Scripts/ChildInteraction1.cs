using UnityEngine;
using TMPro;

public class ChildInteraction1 : MonoBehaviour
{
    [Header("Matching Logic")]
    public int requiredMelodyID;
    public WindChimeBackpack1 backpack;

    [Header("Gaze UI")]
    public TextMeshProUGUI gazeText;

    [Header("Texts")]
    public string successText = "Thank you. I remembered... just a little.";
    public string failureText = "This is... my sound? I don't think so.";

    private bool hasReceivedChime = false;

    public void TriggerDialogue()
    {
        if (hasReceivedChime) return;

        GameObject matchingChime = backpack.FindAttachedChime(requiredMelodyID);
        Debug.Log($"Looking for chime with melody ID: {requiredMelodyID}");

        if (matchingChime != null)
        {
            Debug.Log("Found matching chime: " + matchingChime.name);
            hasReceivedChime = true;

            StartCoroutine(MoveChimeToChild(matchingChime));

            ShowText(successText);
        }
        else
        {
            Debug.Log("No matching chime found.");
            ShowText(failureText);
        }
    }

    private void ShowText(string message)
    {
        if (gazeText != null)
        {
            gazeText.text = message;
            gazeText.gameObject.SetActive(true);
            Invoke(nameof(HideText), 2f);
        }
    }

    private void HideText()
    {
        if (gazeText != null)
        {
            gazeText.gameObject.SetActive(false);
        }
    }

    private System.Collections.IEnumerator MoveChimeToChild(GameObject chime)
    {
        float duration = 1.5f;
        float time = 0f;
        Vector3 start = chime.transform.position;
        Vector3 end = transform.position + Vector3.up * 0.3f; // 偏上位置，避免穿模

        while (time < duration)
        {
            chime.transform.position = Vector3.Lerp(start, end, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        chime.transform.position = end;
        chime.transform.SetParent(this.transform);
    }

    void OnGazeEnter()
    {
        GazeRaycaster1.instance.gazeText.text = "Can you ...hear me?";
    }
}

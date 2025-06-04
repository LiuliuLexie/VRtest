using UnityEngine;

public class MatchManager1 : MonoBehaviour
{
    public static MatchManager1 Instance;

    private ChildInteraction1 currentChild = null;
    private int matchCount = 0;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Called when player gazes at a child
    public void ReadyToMatch(ChildInteraction1 child)
    {
        currentChild = child;
        Debug.Log("Child ready to receive wind chime.");
    }

    // Called when player selects a wind chime from backpack and gives it to child
    public void TryMatch(WindChimeData1 windChimeData)
    {
        if (currentChild == null)
        {
            Debug.LogWarning("No child is currently selected for matching.");
            return;
        }

        if (windChimeData == null)
        {
            Debug.LogWarning("No wind chime selected.");
            return;
        }

        // Check match
        if (windChimeData.melodyID == currentChild.requiredMelodyID)
        {
            matchCount++;
            Debug.Log($"match{matchCount}");

            // Dialogue for correct match
            ChildDialogueUI1.Instance.ShowSingleLine("Thank you. I remembered... just a little.\nAs thanks, best wishes for you");

            // TODO: Trigger disappear animation for wind chime and child
            Destroy(currentChild.gameObject);
            Destroy(windChimeData.gameObject); // windChimeData is a MonoBehaviour on the prefab

            // Trigger game progression
            if (matchCount >= 3)
            {
                Debug.Log("thread visible, able to go back to body now");
                // TODO: Trigger thread visibility / next stage here
            }
        }
        else
        {
            Debug.Log("mismatch");

            // Dialogue for wrong match
            ChildDialogueUI1.Instance.ShowSingleLine("This is... my sound?");
        }

        currentChild = null; // Reset after attempt
    }

public void RegisterMatch(ChildInteraction1 child, bool isMatch)
{
    if (isMatch)
    {
        matchCount++;
        Debug.Log($"match{matchCount}");

        if (matchCount >= 3)
        {
            Debug.Log("thread visible, able to go back to body now");
        }
    }
    else
    {
        Debug.Log("mismatch");
    }

    Destroy(child.gameObject);
    currentChild = null;
}

}

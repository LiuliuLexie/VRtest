using UnityEngine;


public class WindChimeSelector1 : MonoBehaviour
{

    private GameObject selectedChime;

    public void Select(GameObject chime)
    {
        selectedChime = chime;
        Debug.Log($"Selected chime: {chime.name}");

        WindChimeData1 data = selectedChime.GetComponent<WindChimeData1>();
        if (data != null)
        {
            MatchManager1.Instance.TryMatch(data);
        }
        else
        {
            Debug.LogError("Selected chime does not contain WindChimeData1 component!");
        }
    }
}
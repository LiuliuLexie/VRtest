using UnityEngine;

public class GazeTarget : MonoBehaviour
{
    public float gazeDuration = 2f;
    private float gazeTimer = 0f;
    private bool isGazedAt = false;

    public void OnGazeEnter()
    {
        isGazedAt = true;
        gazeTimer = 0f;
    }

    public void OnGazeExit()
    {
        isGazedAt = false;
        gazeTimer = 0f;
    }

    void Update()
    {
        if (isGazedAt)
        {
            gazeTimer += Time.deltaTime;
            if (gazeTimer >= gazeDuration)
            {
                TriggerAction();
                isGazedAt = false;
            }
        }
    }

    private void TriggerAction()
    {
        Debug.Log("Gaze interaction triggered!");
    }
}

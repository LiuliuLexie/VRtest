using UnityEngine;

public class GazeTarget1 : MonoBehaviour
{
    public void OnGazeEnter() => Debug.Log($"{gameObject.name} Gaze Enter");
    public void OnGazeExit() => Debug.Log($"{gameObject.name} Gaze Exit");

    public void OnGazeTrigger()
    {
        if (TryGetComponent<CollectableChime1>(out var chime))
            chime.Collect();

        if (TryGetComponent<ChildInteraction1>(out var child))
            child.TriggerDialogue();
    }
}

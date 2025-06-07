using UnityEngine;

public class GazeDot : MonoBehaviour
{
    public float gazeDistance = 5.0f;
    public GameObject gazeDot;
    private Transform eyeTransform;

    void Start()
    {
        // Находим камеру как источник взгляда
        eyeTransform = Camera.main.transform;
    }

    void Update()
    {
        if (gazeDot == null || eyeTransform == null) return;

        Vector3 targetPosition = eyeTransform.position + eyeTransform.forward * gazeDistance;
        gazeDot.transform.position = targetPosition;

        // (не обязательно) разверни точку по направлению взгляда
        gazeDot.transform.rotation = Quaternion.LookRotation(eyeTransform.forward);
    }
}

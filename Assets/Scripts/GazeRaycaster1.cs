using UnityEngine;
using TMPro;

public class GazeRaycaster1 : MonoBehaviour
{
    public float gazeTime = 2f;
    private float gazeTimer = 0f;
    private GameObject gazedAtObject;

    public TextMeshProUGUI gazeText;
    public Transform cameraTransform;

    public static GazeRaycaster1 instance;

    void Start()
    {
        instance = this;
        
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObj = hit.collider.gameObject;

            if (hitObj.CompareTag("GazeInteractable"))
            {
                if (gazedAtObject != hitObj)
                {
                    gazedAtObject?.SendMessage("OnGazeExit", SendMessageOptions.DontRequireReceiver);
                    gazedAtObject = hitObj;
                    gazedAtObject.SendMessage("OnGazeEnter", SendMessageOptions.DontRequireReceiver);
                    gazeTimer = 0f;
                }

                if (gazeTimer >= 0)
                {
                    gazeTimer += Time.deltaTime;
                }

                if (gazeTimer >= gazeTime)
                {
                    gazedAtObject.SendMessage("OnGazeTrigger", SendMessageOptions.DontRequireReceiver);
                    gazeTimer = -1f;
                }
            }
            else
            {
                ResetGaze();
            }
        }

        else
        {
            ResetGaze();
        }
    }

    void ResetGaze()
    {
        if (gazedAtObject != null)
            gazedAtObject.SendMessage("OnGazeExit", SendMessageOptions.DontRequireReceiver);

        gazeTimer = 0f;
        gazeText.text = "";
        gazedAtObject = null;
    }
}

using UnityEngine;
using TMPro;

public class GazeRaycaster1 : MonoBehaviour
{
    public float gazeTime = 2f;
    private float gazeTimer = 0f;
    private GameObject gazedAtObject;

    public TextMeshProUGUI gazeText;
    public Transform cameraTransform;

    void Start()
    {
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;

        if (gazeText != null)
            gazeText.gameObject.SetActive(false);
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

                gazeTimer += Time.deltaTime;

                // Check if it's a child or wind chime
                if (gazeText != null)
                {
                    if (hitObj.GetComponent<ChildInteraction1>() != null)
                    {
                        gazeText.text = "Can you ...hear me?";
                        Debug.Log("triggered gaze child");
                    }
                    else
                    {
                        gazeText.text = "Collecting...";
                        Debug.Log("triggered gaze wind chime");
                    }

                    gazeText.gameObject.SetActive(true);
                }

                if (gazeTimer >= gazeTime)
                {
                    gazedAtObject.SendMessage("OnGazeTrigger", SendMessageOptions.DontRequireReceiver);
                    gazeTimer = 0f;
                    gazeText.gameObject.SetActive(false);
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
        gazeText?.gameObject.SetActive(false);
        gazedAtObject = null;
    }
}

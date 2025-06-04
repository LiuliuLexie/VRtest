using System.Collections;
using UnityEngine;
using TMPro;

public class GazeRaycaster : MonoBehaviour
{
    public float gazeTime = 2f;
    private float gazeTimer = 0f;
    private GameObject gazedAtObject;

    public TextMeshProUGUI GazeText; 
    public Transform cameraTransform; 
    public VRPlayer movementScript; //stop the camera movement when gazing

    void Start()
    {
        if (GazeText != null)
        {
            GazeText.gameObject.SetActive(false);
        }

        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }

    if (movementScript == null)
    movementScript = FindFirstObjectByType<VRPlayer>();

        // Debug.LogWarning("Movement script not assigned!");
    }

    void Update()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag("GazeInteractable"))
            {
                if (gazedAtObject != hitObject)
                {
                    gazedAtObject = hitObject;
                    gazeTimer = 0f;
                }

                gazeTimer += Time.deltaTime;

                if (GazeText != null)
                {
                    GazeText.text = "collecting...";
                    GazeText.gameObject.SetActive(true);
                }

                if (gazeTimer >= gazeTime)
                {
                    TriggerAction(gazedAtObject);
                    gazeTimer = 0f;
                    gazedAtObject = null;
                    if (GazeText != null)
                    {
                        GazeText.gameObject.SetActive(false);
                    }
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
        gazeTimer = 0f;
        gazedAtObject = null;
        if (GazeText != null)
        {
            GazeText.gameObject.SetActive(false);
        }
    }

void TriggerAction(GameObject target)
{
    Debug.Log("Gaze interaction triggered with: " + target.name);

    //collect
    target.GetComponent<CollectableChime>()?.Collect();

    StartCoroutine(DelayedDisable(target));
}


    IEnumerator DelayedDisable(GameObject target)
    {
        if (movementScript != null)
            movementScript.enabled = false;

        yield return new WaitForSeconds(gazeTime);

        if (target != null)
            target.SetActive(false);


        if (movementScript != null)
            movementScript.enabled = true;
    }
}

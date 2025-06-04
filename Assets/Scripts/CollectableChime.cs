using System.Collections;
using UnityEngine;

public class CollectableChime : MonoBehaviour
{
    [Header("Attachment Settings")]
    public Transform attachPoint; 
    public float moveDuration = 2f;

    [Header("Visibility & Sound Control")]
    public MonoBehaviour triggerScript; 
    public MonoBehaviour soundScript;   

    private static int collectedCount = 0;
    private static float offsetStep = 0.2f;

    private bool isCollected = false;

    public void Collect()
    {
        if (isCollected) return;
        isCollected = true;

        if (attachPoint == null)
        {
            Debug.LogWarning("AttachPoint not assigned on " + gameObject.name);
            return;
        }

        Vector3 offset = new Vector3(0, -collectedCount * offsetStep, 0);
        Vector3 targetPosition = attachPoint.position + offset;

        StartCoroutine(MoveToThread(targetPosition));

        collectedCount++;

        MakeAlwaysVisibleAfterCollected();

        if (triggerScript != null)
            triggerScript.enabled = false;

        if (soundScript != null)
            soundScript.enabled = false;
    }

    IEnumerator MoveToThread(Vector3 targetPosition)
    {
        float time = 0f;
        Vector3 start = transform.position;

        while (time < moveDuration)
        {
            transform.position = Vector3.Lerp(start, targetPosition, time / moveDuration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        transform.SetParent(attachPoint); 
    }

    void MakeAlwaysVisibleAfterCollected()
    {

        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
            rend.enabled = true;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }

        // AudioSource audio = GetComponent<AudioSource>();
        // if (audio != null)
        // {
        //     audio.enabled = true;
        //     if (!audio.isPlaying)
        //         audio.Play();
        // }

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;
    }
}

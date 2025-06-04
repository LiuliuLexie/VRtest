using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindChimeBackpack : MonoBehaviour
{
    public List<Transform> attachPoints = new List<Transform>(); //attach list
    private int currentIndex = 0;
    public float collectDuration = 2f;

    public bool TryAttach(GameObject chime)
    {
        if (currentIndex >= attachPoints.Count)
        {
            Debug.Log("Backpack full!");
            return false;
        }

        StartCoroutine(MoveChimeToAttachPoint(chime, attachPoints[currentIndex]));
        currentIndex++;
        return true;
    }

    private IEnumerator MoveChimeToAttachPoint(GameObject chime, Transform attachPoint)
    {
        Vector3 startPos = chime.transform.position;
        Quaternion startRot = chime.transform.rotation;
        float duration = collectDuration;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float lerp = t / duration;
            chime.transform.position = Vector3.Lerp(startPos, attachPoint.position, lerp);
            chime.transform.rotation = Quaternion.Slerp(startRot, attachPoint.rotation, lerp);
            yield return null;
        }

        // attach the chime to the attach point
        chime.transform.SetParent(attachPoint);
        chime.transform.localPosition = Vector3.zero;
        chime.transform.localRotation = Quaternion.identity;
    }
}

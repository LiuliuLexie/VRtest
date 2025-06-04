using System.Collections;
using UnityEngine;

public class CollectableChime1 : MonoBehaviour
{
    [Header("Attachment Settings")]
    public Transform attachPoint; 
    public float moveDuration = 2f;

    [Header("Visibility & Sound Control")]
    public MonoBehaviour triggerScript; // GazeTarget1 等
    public MonoBehaviour soundScript;   // 声音控制脚本

    // private static int collectedCount = 0;
    // private static float offsetStep = 0.2f;
    private bool isCollected = false;

    [Header("Backpack Reference")]
    public WindChimeBackpack1 backpack; 

    // [Header("Wind Chime Prefab (For Backpack Display)")]
    // public GameObject windChimePrefab; 

    public void Collect()
    {
        if (isCollected) return;
        isCollected = true;

        // 禁用其他逻辑
        if (triggerScript != null)
            triggerScript.enabled = false;

        if (soundScript != null)
            soundScript.enabled = false;

        // // 将 wind chime 添加到 backpack 上的挂点
        // if (windChimePrefab != null && backpack != null)
        // {
        //     GameObject newChime = Instantiate(windChimePrefab);
        //     backpack.TryAttach(newChime); // 自动分配挂点 + 插入动画
        // }
        // else
        // {
        //     Debug.LogWarning("Missing backpack or windChimePrefab reference!");
        // }

        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        float time = 0f;
        Vector3 start = transform.parent.transform.position;
        // Debug.Log("Start position: " + start);
        // Debug.Log("Parent name: " + transform.parent.name); 
        Vector3 target = attachPoint != null ? attachPoint.position : (start + Vector3.up * 0.5f);
        // Debug.Log("Target position: " + target);


        while (time < moveDuration)
        {
            transform.parent.transform.position = Vector3.Lerp(start, target, time / moveDuration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.parent.transform.position = target;
        transform.parent.SetParent(attachPoint); // 确保挂点正确

        yield return new WaitForSeconds(0.2f); 

    }
}

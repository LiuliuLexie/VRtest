using UnityEngine;
using System.Collections;

public class TriggerWIndChime : MonoBehaviour
{
    public GameObject windChimeObject;
    // private Material windChimeMat;
    private Coroutine fadeCoroutine;

    void Start()
    {
        // 获取材质（确保 windchimeObject 使用的是透明材质）
        // windChimeMat = windChimeObject.GetComponent<Renderer>().material;
        // Color c = windChimeMat.color;
        // c.a = 0;
        // windChimeMat.color = c;
        windChimeObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
            windChimeObject.SetActive(true);
            // fadeCoroutine = StartCoroutine(FadeToAlpha(1f)); // 渐显
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
            // fadeCoroutine = StartCoroutine(FadeToAlpha(0f)); // 渐隐
        }
    }

//     IEnumerator FadeToAlpha(float targetAlpha)
//     {
//         float duration = 1.5f;
//         float time = 0f;
//         Color startColor = windChimeMat.color;
//         Color endColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
// 
//         while (time < duration)
//         {
//             time += Time.deltaTime;
//             windChimeMat.color = Color.Lerp(startColor, endColor, time / duration);
//             yield return null;
//         }
// 
//         windChimeMat.color = endColor;
// 
//         if (targetAlpha == 0f)
//         {
//             windChimeObject.SetActive(false);
//             Debug.Log("Wind chime faded out and deactivated.");
//         }
//     }
}

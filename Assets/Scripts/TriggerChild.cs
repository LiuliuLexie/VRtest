using UnityEngine;
using System.Collections;

public class TriggerChild : MonoBehaviour
{
    public GameObject childObject;
    private Material childMat;
    private Coroutine fadeCoroutine;

    void Start()
    {
        // 获取材质（确保 childObject 使用的是透明材质）
        childMat = childObject.GetComponent<Renderer>().material;
        Color c = childMat.color;
        c.a = 0;
        childMat.color = c;
        childObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
            childObject.SetActive(true);
            fadeCoroutine = StartCoroutine(FadeToAlpha(1f)); // 渐显
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeToAlpha(0f)); // 渐隐
        }
    }

    IEnumerator FadeToAlpha(float targetAlpha)
    {
        float duration = 1.5f;
        float time = 0f;
        Color startColor = childMat.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);

        while (time < duration)
        {
            time += Time.deltaTime;
            childMat.color = Color.Lerp(startColor, endColor, time / duration);
            yield return null;
        }

        childMat.color = endColor;

        if (targetAlpha == 0f)
        {
            childObject.SetActive(false);
        }
    }
}

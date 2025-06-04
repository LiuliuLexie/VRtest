using System.Collections;
using TMPro;
using UnityEngine;

public class StorySequenceWithAmbient : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    [TextArea(3, 10)] public string[] sentences;
    public float defaultSentenceDuration = 3f; 
    public float[] sentenceDurations;
    public AudioSource ambientAudioSource;
    public float maxAmbientVolume = 1.0f;

    private int index = 0;

    void Start()
    {
        if (ambientAudioSource != null)
        {
            ambientAudioSource.volume = 0f;
            ambientAudioSource.Play();
        }

        StartCoroutine(PlayStory());
    }

    IEnumerator PlayStory()
    {
        while (index < sentences.Length)
        {
            textDisplay.text = sentences[index];

            float duration = defaultSentenceDuration;
            if (sentenceDurations != null && index < sentenceDurations.Length)
                duration = sentenceDurations[index];

            // 环境音渐强
            if (ambientAudioSource != null)
            {
                float targetVolume = Mathf.Lerp(0f, maxAmbientVolume, (float)(index + 1) / sentences.Length);
                StartCoroutine(FadeToVolume(targetVolume, duration));
            }

            index++;
            yield return new WaitForSeconds(duration);
        }

        // SceneManager.LoadScene("NextScene");
    }

    IEnumerator FadeToVolume(float targetVolume, float duration)
    {
        float startVolume = ambientAudioSource.volume;
        float time = 0f;

        while (time < duration)
        {
            ambientAudioSource.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        ambientAudioSource.volume = targetVolume;
    }
}

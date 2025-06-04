using UnityEngine;

public class PlaySoundWindC : MonoBehaviour
{
    AudioSource source;
    Collider soundTrigger;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        soundTrigger = GetComponent<Collider>();
    }


    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            source.Play();
        }

    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            source.Stop();
        }

    }
}

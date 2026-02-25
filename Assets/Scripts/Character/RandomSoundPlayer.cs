using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        if (audioSource != null)
            audioSource.Play();
    }

    public void StopAudio()
    {
        if (audioSource != null && audioSource.isPlaying)
            audioSource.Stop();
        //Debug.Break();
    }
}

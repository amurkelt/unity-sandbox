using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private void Reset()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //void Start()
    //{
    //    audioSource = GetComponent<AudioSource>();
    //}

    [ContextMenu("PlayAudio")]
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

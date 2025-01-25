using UnityEngine;

public class GameTypeAudio : MonoBehaviour
{
    public AudioSource freeGameAudioSource;
    public AudioSource normalGameAudioSource;

    void Start()
    {
        if (FreeGameData.IsFreeGameEnabled)
        {
            freeGameAudioSource.Play();
        }
        else
        {
            normalGameAudioSource.Play();
        }
    }
}
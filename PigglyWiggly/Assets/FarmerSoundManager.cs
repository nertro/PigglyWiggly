using UnityEngine;
using System.Collections;

public class FarmerSoundManager : MonoBehaviour {

    public AudioClip[] haySounds;
    public AudioClip[] forkSounds;
    public AudioClip[] feedSounds;
    public AudioClip[] slaughterSounds;

    void PlayRandSound(AudioClip[] soundArray)
    {
        this.GetComponent<AudioSource>().clip = soundArray[Random.Range(0, soundArray.Length - 1)];
        this.GetComponent<AudioSource>().Play();
    }

    public void PlayHaySound()
    {
        PlayRandSound(haySounds);
    }

    public void PlayFeedSound()
    {
        PlayRandSound(feedSounds);
    }

    public void PlaySlaughterSound()
    {
        PlayRandSound(slaughterSounds);
    }

    public void PlayForkSound()
    {
        PlayRandSound(forkSounds);
    }
}

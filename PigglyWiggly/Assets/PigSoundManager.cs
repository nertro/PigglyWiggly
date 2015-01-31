using UnityEngine;
using System.Collections;

public class PigSoundManager : MonoBehaviour {

    public AudioClip[] sick;
    public AudioClip[] dirty;
    public AudioClip[] eat;
    public AudioClip[] fart;
    public AudioClip[] hungry;
    public AudioClip[] poop;
    public AudioClip[] grow;

    public AudioSource dirtySource;
    public AudioSource audioSource;

    void PlayRandSound(AudioClip[] soundArray)
    {
        audioSource.clip = soundArray[Random.Range(0, soundArray.Length - 1)];
        audioSource.Play();
    }

    public void PlaySickSound()
    {
        PlayRandSound(sick);
        this.GetComponent<AudioSource>().loop = false;
    }

    public void PlayDirtySound()
    {
        dirtySource.Play();
        this.GetComponent<AudioSource>().loop = true;
    }

    public void PlayEatSound()
    {
        PlayRandSound(eat);
        this.GetComponent<AudioSource>().loop = true;
    }

    public void PlayFartSound()
    {
        PlayRandSound(fart);
        this.GetComponent<AudioSource>().loop = false;
    }

    public void PlayHungrySound()
    {
        PlayRandSound(hungry);
        this.GetComponent<AudioSource>().loop = false;
    }

    public void PlayPoopSound()
    {
        PlayRandSound(poop);
        this.GetComponent<AudioSource>().loop = false;
    }

    public void PlayGrowSound()
    {
        PlayRandSound(grow);
        this.GetComponent<AudioSource>().loop = false;
    }
}

using UnityEngine;
using System.Collections;

public class pigDieSound : MonoBehaviour {

    public AudioClip[] slaughter;
    public AudioClip[] sickDead;

    void PlayRandSound(AudioClip[] soundArray)
    {
        this.GetComponent<AudioSource>().clip = soundArray[Random.Range(0, soundArray.Length - 1)];
        this.GetComponent<AudioSource>().Play();
    }

    public void PlaySlaughterSound()
    {
        PlayRandSound(slaughter);
    }

    public void PlaySickDeadSound()
    {
        PlayRandSound(sickDead);
    }
}

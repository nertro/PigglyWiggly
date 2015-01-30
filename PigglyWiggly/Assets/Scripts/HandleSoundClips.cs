using UnityEngine;
using System.Collections;

public class HandleSoundClips : MonoBehaviour {

    AudioSource[] audios;
    public bool playnext;

    int currentAudio;
    bool isPlaying;


    public AudioClip otherClip;
    IEnumerator PlayAudio()
    {
        audios[currentAudio].Play();
        yield return new WaitForSeconds(audio.clip.length);
        Debug.Log(currentAudio);
        StartCoroutine(PlayAudio());
    }

    void Awake() {
        audios = this.GetComponents<AudioSource>();
        playnext = false;
        currentAudio = 0;
        StartCoroutine(PlayAudio());
    }


    void Update () {
        if (playnext)
        {
            currentAudio++;
            if (currentAudio > audios.Length -1)
            {
                currentAudio = audios.Length - 1;
            }
            playnext = false;
        }
    }
}

using UnityEngine;
using System.Collections;

public class ShowGameOverLabel : MonoBehaviour {

    public GameObject poor;
    public GameObject good;
    public GameObject amazing;

    public AudioClip poorS;
    public AudioClip goodS;
    public AudioClip amazingS;

    public GameObject scoreLabel;

	void Start () {

        int score = PlayerPrefs.GetInt("Score");

        scoreLabel.GetComponent<UILabel>().text = score.ToString();

        if (score < 10)
        {
            poor.SetActive(true);
            audio.clip = poorS;
        }
        else if (score > 10 && score < 20)
        {
            good.SetActive(true);
            audio.clip = goodS;
        }
        else if (score > 20)
        {
            amazing.SetActive(true);
            audio.clip = amazingS;
        }

        audio.Play();
	}

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) || Input.GetAxis("FeedPig") >0)
        {
            Application.LoadLevel("Start");
        }
    }
}

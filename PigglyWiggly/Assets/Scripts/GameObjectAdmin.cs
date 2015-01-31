using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectAdmin : MonoBehaviour {

    public GameObject[] pigs;
    public GameObject food;
    public GameObject pitchfork;
    public GameObject scoreLabel;
    public GameObject timerLabel;
    public GameObject SoundManager;
    public GameObject Farmer;
    int score;

    void Awake()
    {
        
    }

	void Start () {
        score = 0;
        pitchfork = GameObject.FindGameObjectWithTag("Pitchfork");
        food = GameObject.FindGameObjectWithTag("Food");
	}

    public void ChangeScore(int scoreToAdd)
    {
        score += scoreToAdd;

        if (score < 0)
        {
            score = 0;
        }
        scoreLabel.GetComponent<UILabel>().text = score.ToString();

        PlayerPrefs.SetInt("Score", score);
    }

    public void DrawTime(int minutes, int seconds)
    {
        if (seconds == 60)
        {
            seconds = 0;
        }

        if (seconds < 10)
        {
            timerLabel.GetComponent<UILabel>().text = minutes.ToString() + " : 0" + seconds.ToString();
        }
        else
        {
            timerLabel.GetComponent<UILabel>().text = minutes.ToString() + " : " + seconds.ToString();
        }
    }

}

using UnityEngine;
using System.Collections;

public class ShowGameOverLabel : MonoBehaviour {

    public GameObject poor;
    public GameObject good;
    public GameObject amazing;

    public GameObject scoreLabel;

	void Start () {

        int score = PlayerPrefs.GetInt("Score");

        scoreLabel.GetComponent<UILabel>().text = score.ToString();

        if (score < 10)
        {
            poor.SetActive(true);
        }
        else if (score > 10 && score < 20)
        {
            good.SetActive(true);
        }
        else if (score > 20)
        {
            amazing.SetActive(true);
        }
	}

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) || Input.GetAxis("FeedPig") >0)
        {
            Application.LoadLevel("Start");
        }
    }
}

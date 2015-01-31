using UnityEngine;
using System.Collections;

public class HandleGameOver : MonoBehaviour {

    int minutesPassed, secondsPassed;
    int minutesLeft, secondsLeft;
    bool decreasedMinute;

    void Start()
    {
        minutesLeft = 5;
        secondsLeft = 60;
        decreasedMinute = false;
    }

	void Update () {

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.LoadLevel("Start");
        }

        secondsPassed = (int)Time.timeSinceLevelLoad % 60;

        secondsLeft = 60 - secondsPassed;

        if (secondsLeft == 59 &! decreasedMinute)
        {
            minutesLeft--;
            decreasedMinute = true;
        }
        else if(secondsLeft < 59)
        {
            decreasedMinute = false;
        }

        if (minutesLeft <= 0 && secondsLeft == 60)
        {
            Application.LoadLevel("GameOver");
        }

        this.GetComponent<GameObjectAdmin>().DrawTime(minutesLeft, secondsLeft);
	}
}

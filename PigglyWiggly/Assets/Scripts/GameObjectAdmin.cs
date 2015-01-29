using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectAdmin : MonoBehaviour {

    public List<GameObject> pigs;
    public GameObject food;
    public GameObject pitchfork;
    public int score;


	void Start () {
        score = 0;
        pitchfork = GameObject.FindGameObjectWithTag("Pitchfork");
        food = GameObject.FindGameObjectWithTag("Food");

	}
	

	void Update () {
	
	}
}

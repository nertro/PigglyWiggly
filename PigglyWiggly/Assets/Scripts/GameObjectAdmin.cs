using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectAdmin : MonoBehaviour {

    public List<GameObject> pigs;
    public GameObject food;
    public GameObject pitchfork;

	// Use this for initialization
	void Start () {
        GameObject[] startPigs = GameObject.FindGameObjectsWithTag("Pig");
        for (int i = 0; i < startPigs.Length; i++)
        {
            pigs.Add(startPigs[i]);
        }

        pitchfork = GameObject.FindGameObjectWithTag("Pitchfork");
        food = GameObject.FindGameObjectWithTag("Food");

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

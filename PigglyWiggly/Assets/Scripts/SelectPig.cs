using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectPig : MonoBehaviour {

    List<GameObject> pigs;
    public int currentPig;

	// Use this for initialization
	void Start () {
        pigs = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameObjectAdmin>().pigs;
        currentPig = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentPig -= 1;
            if (currentPig < 0)
            {
                currentPig = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            currentPig += 1;
            if (currentPig == pigs.Count)
            {
                currentPig = pigs.Count -1;
            }
        }
	}
}

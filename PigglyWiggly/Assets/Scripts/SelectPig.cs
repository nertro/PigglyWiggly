using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectPig : MonoBehaviour {

    List<GameObject> pigs;
    public int currentPig;

	// Use this for initialization
	void Start () {
        
        currentPig = 0;
	}
	
	// Update is called once per frame
	void Update () {
        pigs = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameObjectAdmin>().pigs;
        if (Input.GetAxis("SelectPigUp"+this.gameObject.name) > 0  || 
            Input.GetAxis("ControllerSelectPig"+this.gameObject.name) > 0)
        {
            currentPig -= 1;
            if (currentPig < 0)
            {
                currentPig = 0;
            }
        }

        if (Input.GetAxis("SelectPigDown" + this.gameObject.name) > 0 ||
            Input.GetAxis("ControllerSelectPig" + this.gameObject.name) < 0)
        {
            currentPig += 1;
            if (currentPig == pigs.Count)
            {
                currentPig = pigs.Count -1;
            }
        }
	}
}

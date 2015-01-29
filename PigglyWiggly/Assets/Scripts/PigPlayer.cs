using UnityEngine;
using System.Collections;

public class PigPlayer : MonoBehaviour {

    GameObjectAdmin gameObjectAdmin;

	void Start () {
        gameObjectAdmin = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameObjectAdmin>();
	}

	void Update () {
        if (Input.GetAxis("Eat") > 0)
        {
            Pig pig = gameObjectAdmin.pigs[this.GetComponent<SelectPig>().currentPig].GetComponent<Pig>();
            if (pig.hasFood)
            {
                pig.eating = true;
            }
        }
        else if (Input.GetAxis("Poop") > 0)
        {
            Pig pig = gameObjectAdmin.pigs[this.GetComponent<SelectPig>().currentPig].GetComponent<Pig>();
            if (pig.hasToPoo)
            {
                pig.pooping = true;
            }
        }
	}
}

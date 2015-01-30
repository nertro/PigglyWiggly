using UnityEngine;
using System.Collections;

public class Pig : MonoBehaviour {

    GameObjectAdmin gameObjectAdmin;

    int hunger, weight;
    float sickness;
    int maxHunger, maxWeight, maxSickness;
    float timer;
    float delay;

    public bool pooping;
    public bool eating;
    public bool hasFood;
    public bool isDirty;
    public bool hasToPoo;
    public int ID;

	void Start () {
        gameObjectAdmin = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameObjectAdmin>();
        hunger = 5;
        weight = 0;
        sickness = 0;
        maxHunger = 10;
        maxSickness = 10;
        maxWeight = 50;
        timer = 0;
        delay = 2;
        pooping = hasFood = eating = isDirty = false;
	}

	void Update () {
        timer += Time.deltaTime;
        weight++;
        ChangeStates();
    }

    void Eat()
    {
        if (hunger != 0)
        {
            hunger--;
        }
        else
        {
            eating = false;
        }
    }

    void Poop()
    {
        if (hunger != maxHunger/2)
        {
            hunger++;
        }
        else
        {
            pooping = false;
            isDirty = true;
        }
    }

    void ChangeStates()
    {
        if (timer >= delay)
        {
            timer = 0;

            if (eating)
            {
                Eat();
            }
            else if (pooping)
            {
                Poop();
            }
            else
            {
                if (isDirty)
                {
                    sickness++;
                }
                HandleHunger();
                HandlePigLife();
            }

            Debug.Log("hun" + hunger);
            Debug.Log("sick" + sickness);
            Debug.Log("wight" + weight);
        }
    }

    void HandleHunger()
    {
        if (hunger != 0)
        {
            if (hunger == maxHunger)
            {
                sickness++;
            }
            else
            {
                if (sickness > 0)
                {
                    sickness -= 0.5f;   
                }
                hunger++;
            }
        }
        else
        {
            sickness++;
            weight++;
            hasToPoo = true;
        }
    }

    void HandlePigLife()
    {
        if (sickness >= maxSickness)
        {
            Debug.Log("dead");
            gameObjectAdmin.pigs.Remove(this.gameObject);
            gameObjectAdmin.ChangeScore(-1);
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<PigManager>().SpawnPig(ID, false);
            Destroy(this.gameObject);
        }
        else if (weight >= maxWeight)
        {
            Debug.Log("slaughterhouse");
            gameObjectAdmin.pigs.Remove(this.gameObject);
            gameObjectAdmin.ChangeScore(1);
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<PigManager>().SpawnPig(ID, true);
            Destroy(this.gameObject);
        }
    }

}

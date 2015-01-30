using UnityEngine;
using System.Collections;

public class Pig : MonoBehaviour {

    GameObjectAdmin gameObjectAdmin;

    int hunger, weight;
    float sickness;
    int maxHunger, maxWeight, maxSickness;
    float timer;
    float delay;
    float scaleBuffer;

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
        maxWeight = 500;
        timer = 0;
        delay = 2;
        pooping = hasFood = eating = isDirty = false;
        scaleBuffer = 0;

        HandleWeight();
	}

	void Update () {
        timer += Time.deltaTime;
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
            HandleWeight();
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

    void HandleWeight()
    {
        if (weight <= (maxWeight * 20)/100 && scaleBuffer != 5)
        {
            Debug.Log(5);
            scaleBuffer = 5;
            this.gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x / scaleBuffer, gameObject.transform.localScale.y / scaleBuffer, gameObject.transform.localScale.z / scaleBuffer);
        }
        else if (weight <= (maxWeight * 40) / 100 && weight > (maxWeight * 20) / 100 && scaleBuffer != 4)
        {
            Debug.Log(4);
            GetBigger();
        }
        else if (weight <= (maxWeight * 60) / 100 && weight > (maxWeight * 40) / 100 && scaleBuffer != 3)
	    {
            Debug.Log(3);
            GetBigger();
        }
        else if (weight <= (maxWeight * 80) / 100 && weight > (maxWeight * 60) / 100 && scaleBuffer != 2)
        {
            Debug.Log(2);
            GetBigger();    
        }
        else if (weight <= (maxWeight * 90) / 100 && weight > (maxWeight * 80) / 100 && scaleBuffer != 1.5f)
        {
            Debug.Log(1);
            this.gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * scaleBuffer, gameObject.transform.localScale.y * scaleBuffer, gameObject.transform.localScale.z * scaleBuffer);
            scaleBuffer = 1.5f;
            this.gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x / scaleBuffer, gameObject.transform.localScale.y / scaleBuffer, gameObject.transform.localScale.z / scaleBuffer);
        }
        else if (weight >= maxWeight)
        {
            Debug.Log(0);
            this.gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * scaleBuffer, gameObject.transform.localScale.y * scaleBuffer, gameObject.transform.localScale.z * scaleBuffer);
            scaleBuffer = 0;
        }
    }


    void GetBigger()
    {
        this.gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * scaleBuffer, gameObject.transform.localScale.y * scaleBuffer, gameObject.transform.localScale.z * scaleBuffer);
        scaleBuffer --;
        this.gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x / scaleBuffer, gameObject.transform.localScale.y / scaleBuffer, gameObject.transform.localScale.z / scaleBuffer);
    }
}

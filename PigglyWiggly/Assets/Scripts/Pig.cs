using UnityEngine;
using System.Collections;

public class Pig : MonoBehaviour {

    GameObjectAdmin gameObjectAdmin;
    PigLook pigLook;
    Animator anim;

    int hunger, weight;
    float sickness;
    int maxHunger, maxWeight, maxSickness;
    float timer;
    float delay;
    float scaleBuffer;
    bool dead;

    public bool pooping;
    public bool eating;
    public bool hasFood;
    public bool isDirty;
    public bool hasToPoo;
    public int ID;
    public bool bottomPig;

	void Start () {
        anim = this.GetComponent<Animator>();
        gameObjectAdmin = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameObjectAdmin>();
        pigLook = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PigLook>();
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
        weight += 100;
    }

    public void Eat()
    {
        if (hunger > 0)
        {
            anim.SetBool("eating", true);
            hunger-=2;
        }
        else
        {
            anim.SetBool("eating", false);
            eating = false;
        }
    }

    public void Poop()
    {
        if (hunger > maxHunger/2)
        {
            anim.SetBool("pooping", true);
            hunger+=2;
        }
        else
        {
            anim.SetBool("pooping", false);
            pooping = false;
            isDirty = true;
        }
    }

    void ChangeStates()
    {
        if (timer >= delay)
        {
            timer = 0;

            if (eating &! dead)
            {
                Eat();
            }
            else if (pooping &! dead)
            {
                Poop();
            }
            else
            {
                if (isDirty)
                {
                    sickness += 5f;
                }
                HandleHunger();
                HandlePigLife();
            }

            GetComponentInChildren<SkinnedMeshRenderer>().material.SetTexture(0, pigLook.ChangePigLook(isDirty, hunger, sickness));
            //mat.SetTexture(0, pigLook.ChangePigLook(isDirty, hunger, sickness));
        }
    }

    void HandleHunger()
    {
        if (hunger != 0)
        {
            if (hunger == maxHunger)
            {
                sickness += 0.5f;
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
            weight+=20;
            HandleWeight();
            hasToPoo = true;
        }
    }

    void HandlePigLife()
    {
        if (sickness >= maxSickness)
        {
            if (dead)
            {
                Debug.Log("dead");
                gameObjectAdmin.pigs[ID] = null;
                gameObjectAdmin.ChangeScore(-1);
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<PigManager>().SpawnPig(ID, false, bottomPig);
                Destroy(this.gameObject);
            }

            GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            dead = true;
        }
        else if (weight >= maxWeight)
        {
            if (dead)
            {
                Debug.Log("slaughterhouse");
                gameObjectAdmin.ChangeScore(2);
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<PigManager>().SpawnPig(ID, true, bottomPig);
                gameObjectAdmin.Farmer.GetComponent<FarmerSoundManager>().PlaySlaughterSound();
                Destroy(this.gameObject);
            }

            GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            dead = true;
        }
    }

    void HandleWeight()
    {
        if (weight <= (maxWeight * 20)/100 && scaleBuffer != 2)
        {
            scaleBuffer = 2;
            this.gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x / scaleBuffer, gameObject.transform.localScale.y / scaleBuffer, gameObject.transform.localScale.z / scaleBuffer);
        }
        else if (weight <= (maxWeight * 40) / 100 && weight > (maxWeight * 20) / 100 && scaleBuffer != 1.75f)
        {
            GetBigger();
        }
        else if (weight <= (maxWeight * 60) / 100 && weight > (maxWeight * 40) / 100 && scaleBuffer != 1.5)
	    {
            GetBigger();
        }
        else if (weight <= (maxWeight * 80) / 100 && weight > (maxWeight * 60) / 100 && scaleBuffer != 1.25f)
        {
            GetBigger();    
        }
        else if (weight <= (maxWeight * 90) / 100 && weight > (maxWeight * 80) / 100 && scaleBuffer != 1.1f)
        {
            this.gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * scaleBuffer, gameObject.transform.localScale.y * scaleBuffer, gameObject.transform.localScale.z * scaleBuffer);
            scaleBuffer = 1.1f;
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
        scaleBuffer -= 0.25f;
        this.gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x / scaleBuffer, gameObject.transform.localScale.y / scaleBuffer, gameObject.transform.localScale.z / scaleBuffer);
    }
}

using UnityEngine;
using System.Collections;

public class Pig : MonoBehaviour {

    GameObjectAdmin gameObjectAdmin;
    PigLook pigLook;
    Animator anim;
    PigSoundManager soundMngr;
    GameObject gameManager;

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
    public GameObject dieSoundPrefab;

    public int hungerIncrease;
    public float sicknessIncrease;
    public int weightIncrease;

	void Start () {
        anim = this.GetComponent<Animator>();
        soundMngr = this.GetComponent<PigSoundManager>();
        gameObjectAdmin = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameObjectAdmin>();
        pigLook = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PigLook>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        hunger = 5;
        weight = 0;
        sickness = 0;
        maxHunger = 10;
        maxSickness = 10;
        maxWeight = 100;
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

    public void Eat()
    {
        if (hunger > 0)
        {
            anim.SetBool("eating", true);
            soundMngr.PlayEatSound();
            hunger-=hungerIncrease;
            gameManager.GetComponent<ShowHideHay>().ChangeHayTexture(hunger, ID);
        }
        else
        {
            anim.SetBool("eating", false);
            this.audio.Stop();
            hasFood = false;
            eating = false;
            hasToPoo = true;
            weight+= 35;
            gameManager.GetComponent<ShowHideHay>().ShowHay(false, ID);
        }
    }

    public void Poop()
    {
        if (hunger < maxHunger/2)
        {
            anim.SetBool("pooping", true);
            soundMngr.PlayPoopSound();
            hasToPoo = false;
            hunger+=hungerIncrease;
        }
        else
        {
            anim.SetBool("pooping", false);
            this.audio.Stop();
            pooping = false;
            isDirty = true;
            this.soundMngr.PlayDirtySound();
            gameManager.GetComponent<ShowHidePoo>().ShowPoo(true, ID);
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
                    sickness += sicknessIncrease;
                }
                HandleHunger();
                HandlePigLife();
            }

            //if (sickness > maxSickness/2)
            //{
            //    soundMngr.PlaySickSound();
            //}

            if (!isDirty && soundMngr.dirtySource.isPlaying)
            {
                soundMngr.dirtySource.Stop();
            }

            GetComponentInChildren<SkinnedMeshRenderer>().material.SetTexture(0, pigLook.ChangePigLook(isDirty, hunger, sickness));
        }
    }

    void HandleHunger()
    {
        if (hunger > 0)
        {
            //hasToPoo = false;

            if (hunger == maxHunger)
            {
                sickness += sicknessIncrease;
                soundMngr.PlayHungrySound();
            }
            else
            {
                if (sickness > 0)
                {
                    sickness -= sicknessIncrease;   
                }
                hunger+= hungerIncrease;
            }
        }
        else
        {
            soundMngr.PlayFartSound();
            sickness += sicknessIncrease;
            HandleWeight();
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
                gameObjectAdmin.noteLabel.GetComponent<UILabel>().text = "";
                Destroy(this.gameObject);
            }

            GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            GameObject dieSound = Instantiate(dieSoundPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            dieSound.GetComponent<pigDieSound>().PlaySickDeadSound();
            dead = true;
            gameObjectAdmin.noteLabel.GetComponent<UILabel>().text = "Damn! It died from sickness!!";
        }
        else if (weight >= maxWeight)
        {
            if (dead)
            {
                Debug.Log("slaughterhouse");
                gameObjectAdmin.ChangeScore(2);
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<PigManager>().SpawnPig(ID, true, bottomPig);
                gameObjectAdmin.Farmer.GetComponent<FarmerSoundManager>().PlaySlaughterSound();
                gameObjectAdmin.noteLabel.GetComponent<UILabel>().text = "";
                Destroy(this.gameObject);
            }

            GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            GameObject dieSound = Instantiate(dieSoundPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            dieSound.GetComponent<pigDieSound>().PlaySlaughterSound();
            dead = true;
            gameObjectAdmin.noteLabel.GetComponent<UILabel>().text = "Yeah! Off to the Slaughterhouse!!";
        }
    }

    void HandleWeight()
    {
        if (weight < (maxWeight * 20)/100 && scaleBuffer != 2)
        {
            scaleBuffer = 2;
            this.gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x / scaleBuffer, gameObject.transform.localScale.y / scaleBuffer, gameObject.transform.localScale.z / scaleBuffer);
        }
        else if (weight < (maxWeight * 40) / 100 && weight >= (maxWeight * 20) / 100 && scaleBuffer != 1.75f)
        {
            GetBigger();
        }
        else if (weight < (maxWeight * 60) / 100 && weight >= (maxWeight * 40) / 100 && scaleBuffer != 1.5)
	    {
            GetBigger();
        }
        else if (weight < (maxWeight * 80) / 100 && weight >= (maxWeight * 60) / 100 && scaleBuffer != 1.25f)
        {
            GetBigger();    
        }
        else if (weight < (maxWeight * 90) / 100 && weight >= (maxWeight * 80) / 100 && scaleBuffer != 1.1f)
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
        soundMngr.PlayGrowSound();
        this.gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * scaleBuffer, gameObject.transform.localScale.y * scaleBuffer, gameObject.transform.localScale.z * scaleBuffer);
        scaleBuffer -= 0.25f;
        this.gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x / scaleBuffer, gameObject.transform.localScale.y / scaleBuffer, gameObject.transform.localScale.z / scaleBuffer);
    }
}

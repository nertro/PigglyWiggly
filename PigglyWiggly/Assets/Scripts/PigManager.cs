using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PigManager : MonoBehaviour {

    public GameObject pigPrefab;
    public Vector3 BottomPosition;

    public int currentPigCount;
    public int maxPigsRight;
    public int maxPigsBottom;
    public float gap;
    public float bottomGap;

    Vector3[] pigSpawnPositions;


	void Start () {
        pigSpawnPositions = new Vector3[maxPigsRight + maxPigsBottom];
        for (int i = 0; i < pigSpawnPositions.Length; i++)
        {
            if (i < maxPigsRight)
            {
                pigSpawnPositions[i] = new Vector3(pigPrefab.transform.position.x, pigPrefab.transform.position.y, pigPrefab.transform.position.z - pigPrefab.transform.lossyScale.z * i - gap * i);
                Debug.Log(pigSpawnPositions[i]);
            }
            else
            {
                pigSpawnPositions[i] = new Vector3(BottomPosition.x - bottomGap * i, BottomPosition.y, BottomPosition.z);
            }
        }


        this.GetComponent<GameObjectAdmin>().pigs = new GameObject[maxPigsRight + maxPigsBottom];

        currentPigCount = 1;
        SpawnPig(0, false, false);
	}

    public void SpawnPig(int spawnPointID, bool spawnTwo, bool bottomPig)
    {
        currentPigCount--;

        Quaternion rotation = Quaternion.identity;
        if (!bottomPig)
        {
            rotation = pigPrefab.transform.rotation;
        }

        GameObject pig = Instantiate(pigPrefab, pigSpawnPositions[spawnPointID], rotation) as GameObject;
        this.GetComponent<GameObjectAdmin>().pigs[spawnPointID] = pig;
        pig.GetComponent<Pig>().ID = spawnPointID;
        if (bottomPig)
        {
            pig.GetComponent<Pig>().bottomPig = true;
        }
        else
        {
            pig.GetComponent<Pig>().bottomPig = false;
        }

        currentPigCount++;

        if (spawnTwo && currentPigCount < pigSpawnPositions.Length)
        {
            Debug.Log("spawnSecond");
            if (currentPigCount >= maxPigsRight)
            {
                rotation = Quaternion.identity;
            }
            this.GetComponent<GameObjectAdmin>().SoundManager.GetComponent<HandleSoundClips>().playnext = true;
            pig = Instantiate(pigPrefab, pigSpawnPositions[currentPigCount], rotation) as GameObject;
            this.GetComponent<GameObjectAdmin>().pigs[currentPigCount] = pig;
            pig.GetComponent<Pig>().ID = currentPigCount;
            if (currentPigCount >= maxPigsRight)
            {
                pig.GetComponent<Pig>().bottomPig = true;
            }

            currentPigCount++;
        }
    }
}

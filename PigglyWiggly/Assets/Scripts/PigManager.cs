using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PigManager : MonoBehaviour {

    public GameObject pigPrefab;
    public int currentPigCount;
    public int maxPigsRight;
    public int gap;

    Vector3[] pigSpawnPositions;

	void Start () {
        pigSpawnPositions = new Vector3[maxPigsRight];
        for (int i = 0; i < maxPigsRight; i++)
        {
            pigSpawnPositions[i] = new Vector3(pigPrefab.transform.position.x, pigPrefab.transform.position.y, pigPrefab.transform.position.z + pigPrefab.transform.localScale.z * i + gap * i);
        }
        this.GetComponent<GameObjectAdmin>().pigs = new List<GameObject>();

        SpawnPig(0, false);
        currentPigCount++;
	}

    public void SpawnPig(int spawnPointID, bool spawnTwo)
    {
        GameObject pig = Instantiate(pigPrefab, pigSpawnPositions[spawnPointID], pigPrefab.transform.rotation) as GameObject;
        this.GetComponent<GameObjectAdmin>().pigs.Add(pig);
        pig.GetComponent<Pig>().ID = spawnPointID;

        if (spawnTwo && currentPigCount < maxPigsRight)
        {
            pig = Instantiate(pigPrefab, pigSpawnPositions[currentPigCount - 1], pigPrefab.transform.rotation) as GameObject;
            this.GetComponent<GameObjectAdmin>().pigs.Add(pig);
            pig.GetComponent<Pig>().ID = currentPigCount;
            currentPigCount++;
        }
    }
}

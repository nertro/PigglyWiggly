using UnityEngine;
using System.Collections;

public class PigManager : MonoBehaviour {

    public GameObject pigPrefab;
    public GameObject pigModelPrefab;
    public int currentPigCount;
    public int maxPigsLeft;
    public int gap;

    Vector3[] pigSpawnPositions;

	void Start () {
        pigSpawnPositions = new Vector3[maxPigsLeft];
        for (int i = 0; i < maxPigsLeft; i++)
        {
            pigSpawnPositions[i] = new Vector3(pigPrefab.transform.position.x, pigModelPrefab.transform.position.y, pigPrefab.transform.position.z + pigModelPrefab.transform.localScale.z * i + gap * i);
        }

        SpawnPig(0, false);
        currentPigCount++;
	}

    public void SpawnPig(int spawnPointID, bool spawnTwo)
    {
        GameObject pig = Instantiate(pigPrefab, pigSpawnPositions[spawnPointID], Quaternion.identity) as GameObject;
        this.GetComponent<GameObjectAdmin>().pigs.Add(pig);
        pig.GetComponent<Pig>().ID = spawnPointID;

        if (spawnTwo && currentPigCount < maxPigsLeft)
        {
            pig = Instantiate(pigPrefab, pigSpawnPositions[currentPigCount-1], Quaternion.identity) as GameObject;
            this.GetComponent<GameObjectAdmin>().pigs.Add(pig);
            pig.GetComponent<Pig>().ID = currentPigCount;
            currentPigCount++;
        }
    }
}

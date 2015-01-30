using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PigManager : MonoBehaviour {

    public GameObject pigPrefab;
    public int currentPigCount;
    public int maxPigsRight;
    public int maxPigsBottom;
    public int gap;

    Vector3[] pigRightSpawnPositions;
    Vector3[] pigBottomSpawnPositions;

	void Start () {
        pigRightSpawnPositions = new Vector3[maxPigsRight];
        for (int i = 0; i < maxPigsRight; i++)
        {
            pigRightSpawnPositions[i] = new Vector3(pigPrefab.transform.position.x, pigPrefab.transform.position.y, pigPrefab.transform.position.z + pigPrefab.transform.localScale.z * i + gap * i);
        }
        this.GetComponent<GameObjectAdmin>().pigs = new List<GameObject>();

        SpawnPig(0, false);
        currentPigCount++;
	}

    public void SpawnPig(int spawnPointID, bool spawnTwo)
    {
        GameObject pig = Instantiate(pigPrefab, pigRightSpawnPositions[spawnPointID], pigPrefab.transform.rotation) as GameObject;
        this.GetComponent<GameObjectAdmin>().pigs.Add(pig);
        pig.GetComponent<Pig>().ID = spawnPointID;

        if (spawnTwo && currentPigCount < maxPigsRight)
        {
            this.GetComponent<GameObjectAdmin>().SoundManager.GetComponent<HandleSoundClips>().playnext = true;
            pig = Instantiate(pigPrefab, pigRightSpawnPositions[currentPigCount - 1], pigPrefab.transform.rotation) as GameObject;
            this.GetComponent<GameObjectAdmin>().pigs.Add(pig);
            pig.GetComponent<Pig>().ID = currentPigCount;
            currentPigCount++;
        }
    }
}

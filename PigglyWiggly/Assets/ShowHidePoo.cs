using UnityEngine;
using System.Collections;

public class ShowHidePoo : MonoBehaviour {

    public GameObject[] pooSprites;

    public void ShowPoo(bool show, int id)
    {
        pooSprites[id].SetActive(show);
    }
}

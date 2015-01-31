using UnityEngine;
using System.Collections;

public class ShowHideHay : MonoBehaviour {

    public GameObject[] haySprites;

	public void ShowHay(bool show, int id)
    {
        haySprites[id].SetActive(show);
    }

    public void ChangeHayTexture(int hunger, int id)
    {
        haySprites[id].GetComponent<UISprite>().spriteName = "heu_" + (hunger / 2);
    }
}

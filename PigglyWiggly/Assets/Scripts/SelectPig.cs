using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectPig : MonoBehaviour {

    int pigCount;
    int pigCountRight;
    public int currentPig;
    public UISprite icon;
    public int gapDown;
    public int gapLeft;

    Vector3 iconTopRight;
    Vector3 iconBottom;
    bool canPress;

	// Use this for initialization
	void Start () {
        
        currentPig = 0;
        iconTopRight = icon.transform.position;
        canPress = false;
	}
	
	// Update is called once per frame
	void Update () {
        pigCount = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PigManager>().currentPigCount;
        pigCountRight = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PigManager>().maxPigsRight;

        if ((Input.GetAxis("SelectPigUp"+this.gameObject.name) > 0  || 
            Input.GetAxis("ControllerSelectPig"+this.gameObject.name) > 0)
            && canPress)
        {
            currentPig -= 1;
            if (currentPig < 0)
            {
                currentPig = 0;
            }

            if (currentPig < pigCountRight)
            {
                icon.transform.position = new Vector3(iconTopRight.x, iconTopRight.y - (gapDown * currentPig), iconTopRight.z);
            }
            else
            {
                icon.transform.position = new Vector3(iconBottom.x - (gapLeft * currentPig), iconBottom.y, iconBottom.z);
            }
            canPress = false;
        }
        else if ((Input.GetAxis("SelectPigDown" + this.gameObject.name) > 0 ||
            Input.GetAxis("ControllerSelectPig" + this.gameObject.name) < 0)
            && canPress)
        {
            currentPig += 1;
            if (currentPig == pigCount)
            {
                currentPig = pigCount - 1;
            }

            if (currentPig < pigCountRight)
            {
                icon.transform.position = new Vector3(iconTopRight.x, iconTopRight.y - (gapDown * currentPig), iconTopRight.z);
            }
            else
            {
                icon.transform.position = new Vector3(iconBottom.x - (gapLeft * currentPig), iconBottom.y, iconBottom.z);
            }

            canPress = false;
        }
        else if (Input.GetAxis("SelectPigDown" + this.gameObject.name) == 0 &&
            Input.GetAxis("ControllerSelectPig" + this.gameObject.name) == 0 &&
            Input.GetAxis("SelectPigUp" + this.gameObject.name) == 0 &&
            Input.GetAxis("ControllerSelectPig" + this.gameObject.name) == 0)
        {
            canPress = true;
        }
	}
}

using UnityEngine;
using System.Collections;

public class ShowHideMenuOverlay : MonoBehaviour {

    public GameObject panelToShow;
    public GameObject panelToHide;

    void OnClick()
    {
        panelToShow.SetActive(true);
        panelToHide.SetActive(false);
    }
}

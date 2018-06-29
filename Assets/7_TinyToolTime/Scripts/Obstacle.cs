using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    [Tooltip("Welches Level?")]
    public int level;
    public GameObject HelpWindow;
    public GameObject PositionSecondPlayer;


    //HelpFenster ploppt auf
    public void ShowHelpWindow(bool show, GameObject player) { //Callend bei PlayerMovement
        if (!HelpWindow.activeSelf)
        {
            HelpWindow.SetActive(show);
            HelpWindow.GetComponent<HelpWindow>().SetPlayer(player, level);
        }
    }


}

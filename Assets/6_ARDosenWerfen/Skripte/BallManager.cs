﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class BallManager : MonoBehaviour {

    public static int score;
    Text text;

    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update () {

      
        text.text = "Balls: " + Shoot.ballAmount + "\n" + "Level: " + GameplayManager.showCurrentLevel + "\n" + "Score: " + ScoreManager.score + "/ 250";
    }
}

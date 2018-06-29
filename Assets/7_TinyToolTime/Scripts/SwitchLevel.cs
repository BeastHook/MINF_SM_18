using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLevel : MonoBehaviour {

    private Animator anim;
    private ChangeLevel changeLevel;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    internal void LevelEnd(ChangeLevel changeLevel) //called from ChangeLevel
    {
        this.changeLevel = changeLevel;
        anim.SetBool("nextLevel", true);
    }

    
    public void NextLevel()     //Called from Animation Event
    {
        changeLevel.NextLevelStart();
    }


}

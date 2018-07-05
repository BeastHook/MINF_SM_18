using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevel : MonoBehaviour {

    public GameObject levelOne;
    public GameObject levelTwo;
    public GameObject levelThree;
    public bool nextLevel = true;   
    public int actualLevel = 1;

    private float testTimer = 0;
    private GameObject levelOld;
    private GameObject levelNext;

    //Testing
    private bool klickOne = false;

    private void Start()
    {
        SwitchLevel(levelOne, levelTwo);
    }
/*
    void Update()
    {

        if (Input.GetButtonDown("TestBtn")) {    //Testing    
            print("a key was pressed"); //Testing
            StartLevelChange();
        }
    }
*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            StartLevelChange();           
        }
    }

    private void StartLevelChange()
    {
        levelOld.GetComponent<SwitchLevel>().LevelEnd(this);
    }

    public void NextLevelStart() { //called from SwitchLevel
        levelOld.SetActive(false);
        levelNext.SetActive(true);
        SetLevel();
    }
/*
    private void MoveLevelBack()
    {
        levelOld.transform.Translate(Vector3.back * Time.deltaTime * levelSpeed);            
    }

    private void MoveLevelForward()
    {
        levelNext.transform.Translate(Vector3.forward * Time.deltaTime * levelSpeed);
    }
*/
    public void SetLevel()
    {
        if (nextLevel)
        {
            SwitchLevel(levelTwo, levelThree);
            actualLevel += 1;
        }
    }

    void SwitchLevel(GameObject lvlOld, GameObject lvlNext) {
        levelOld = lvlOld;
        levelNext = lvlNext;

    }


    
    
}

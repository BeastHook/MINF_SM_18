using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTheGame : MonoBehaviour {


    public GameObject scripts;

    void Update()
    {

        //if (Input.GetKeyDown("Fire1"))
        if (Input.GetMouseButtonDown(0))
        {
            buttonPressed();
        }
    }

    void buttonPressed()
    {
        scripts.GetComponent<LevelController>().toggleGUI(false);
    }
}

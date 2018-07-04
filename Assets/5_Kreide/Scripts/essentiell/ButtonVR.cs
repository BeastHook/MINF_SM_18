using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonVR : MonoBehaviour {

    public GameObject scripts;

    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        buttonPressed();
    }

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



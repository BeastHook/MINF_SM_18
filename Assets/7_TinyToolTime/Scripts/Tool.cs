using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ruft im Hindernis eine Funktion auf - sendet den Namen mit
public class Tool : MonoBehaviour {

    public int intTool;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Help"))
        {
            other.GetComponent<HelpWindow>().ContactWithTool(intTool);
        }
    }

}

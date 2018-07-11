using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public class GameCounter : MonoBehaviour {

    public int count = 0;
    public bool done = false;
    public TextMesh score; 
	// Use this for initialization
	void Start () {
        score.text = "Score: " + count.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        score.text = "Score: " + count.ToString();
	}
}

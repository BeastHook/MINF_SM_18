using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformArray : MonoBehaviour {
    [SerializeField] private GameObject platform1, platform2, platform3, platform4, platform5, platform6, platform7;
    public List<GameObject> platforms;
    private int readyPlatforms = 0;
    // Use this for initialization
    void Start () {
        platforms.Add(platform1);
        platforms.Add(platform2);
        platforms.Add(platform3);
        platforms.Add(platform4);
        platforms.Add(platform5);
        platforms.Add(platform6);
        platforms.Add(platform7);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        for (int i = 0; i < platforms.Count; i++)
        {
            if (platforms[i].GetComponent<PlatformMovement>().maxreach)
            {
                platforms[i].GetComponent<PlatformMovement>().moveAllowed = false;
                readyPlatforms++;
            }
        }
        if (readyPlatforms == 7) {
            for (int i = 0; i < platforms.Count; i++)
            {
                platforms[i].GetComponent<PlatformMovement>().maxreach = false;
                platforms[i].GetComponent<PlatformMovement>().moveAllowed = true; ;
            }
        }
        else
            readyPlatforms = 0;
    }
}

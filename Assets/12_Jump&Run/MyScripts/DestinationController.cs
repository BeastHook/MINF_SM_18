using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationController : MonoBehaviour {



    public GameObject chestTop;
    Rigidbody rigid;
    public GameObject letter;
    bool animateLetter=false;
    float height;
    

    // Use this for initialization
    void Start() {
        rigid = chestTop.GetComponent<Rigidbody>();
        height = letter.transform.localPosition.y;
        print(height);
    }

    // Update is called once per frame
    void Update() {

        if (animateLetter)
        {

            if (height < 0.5f)
            {
                
                height += 0.1f * Time.deltaTime;
                letter.transform.localPosition = new Vector3(letter.transform.localPosition.x, height, letter.transform.localPosition.z);
            }
        

           
          


        }

    }
       public void OpenChest()
    {

        Vector3 direction = transform.right*100;
        rigid.AddForce(direction);
        animateLetter = true;
    }
}

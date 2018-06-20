using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    Vector3 direction;
    public float travelspeed = 2;
    public float force = 750f;
    // Use this for initialization
    void Start()
    {
       
        StartCoroutine("Wait");
    }


    void FixedUpdate()
    {
       

        transform.Translate(Vector3.right * Time.deltaTime * travelspeed);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Destroy(this.gameObject);
    }
    IEnumerator Wait()
    {

       
  
        yield return new WaitForSeconds(3f);
      
        Destroy(this.gameObject);
    }
}

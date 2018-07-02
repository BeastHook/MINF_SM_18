using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour {

	public GameObject bluepaint;
	public GameObject parentObject;

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			// transform.position in Array

			//Instantiate (bluepaint, transform.position, transform.rotation);

			//ArrayList bla = new ArrayList ();
			//bla.Add (transform.position);

			// Initiate Object

			GameObject newLine = Instantiate (bluepaint, transform.position, transform.rotation) as GameObject;
			newLine.transform.parent = parentObject.transform;

		}
	}
}

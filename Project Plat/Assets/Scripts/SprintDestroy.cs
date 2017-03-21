using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		//if the sprinting player collides with the object
		if (other.tag == "Player" && other.gameObject.GetComponent<PlayerController>().sprinting) {

			//deactivate the object
			gameObject.SetActive (false);
		}
	}
}

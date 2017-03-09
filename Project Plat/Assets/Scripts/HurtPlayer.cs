using UnityEngine;
using System.Collections;

public class HurtPlayer : MonoBehaviour {


	private LevelManager theLevelManager;


	void Start () {
		//find the level manager in the game
		theLevelManager = FindObjectOfType <LevelManager> ();
	}
	

	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			//if the Player enters a killbox, respawn the player
			theLevelManager.Respawn ();
		}
	}

}

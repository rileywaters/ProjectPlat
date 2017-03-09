using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public float waitToRespawn;
	public PlayerController thePlayer;

	public GameObject deathSplosion;

	public int coinCount;

	void Start () {
		//assign the player object
		thePlayer = FindObjectOfType<PlayerController> ();

	}
	

	void Update () {
	
	}

	public void Respawn(){
		//start a respawn coroutine, so the game doesnt pause when the player dies
		StartCoroutine ("RespawnCo");
	}

	public IEnumerator RespawnCo(){
		//upon dieing, turn off the player object
		thePlayer.gameObject.SetActive (false);

		//create a death particle effect
		Instantiate (deathSplosion, thePlayer.transform.position, thePlayer.transform.rotation);

		//wait an amount of seconds
		yield return new WaitForSeconds (waitToRespawn);

		//move the player to the respawn location
		thePlayer.transform.position = thePlayer.respawnPosition;

		//reactivate the player object
		thePlayer.gameObject.SetActive (true);
	}


	public void AddCoins(int coinstoAdd){
		coinCount += coinstoAdd;


	}
}

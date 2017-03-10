using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public float waitToRespawn;
	public PlayerController thePlayer;

	public GameObject deathSplosion;

	public int coinCount;

	public Text scoreText;

	public Image heart1;
	public Image heart2;
	public Image heart3;

	public Sprite heartFull;
	public Sprite heartHalf;
	public Sprite heartEmpty;

	public int maxHealth;
	public int healthCount;

	private bool respawning;

	public ResetOnRespawn[] objectsToReset;

	void Start () {
		//assign the player object
		thePlayer = FindObjectOfType<PlayerController> ();

		//set the score UI to be the coin count
		scoreText.text = "Score: " + coinCount;

		//set health to be full
		healthCount = maxHealth;

		objectsToReset = FindObjectsOfType<ResetOnRespawn> ();
	}
	

	void Update () {
		if (healthCount <= 0 && !respawning) {
			//if health becomes 0 and the player isnt respawning, respawn the player
			Respawn ();
			respawning = true;
		}
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

		//wait an amount of seconds before respawning
		yield return new WaitForSeconds (waitToRespawn);
		healthCount = maxHealth;
		respawning = false;

		//reset the health
		UpdateHeartMeter ();

		//lose all coins on death :(
		coinCount = 0;
		scoreText.text = "Score: " + coinCount;

		//move the player to the respawn location
		thePlayer.transform.position = thePlayer.respawnPosition;

		//reactivate player
		thePlayer.gameObject.SetActive (true);

		//reactivate and reset other objects
		for (int i = 0; i < objectsToReset.Length; i++) {
			objectsToReset [i].gameObject.SetActive (true);
			objectsToReset [i].ResetObject ();
		}
	}


	public void AddCoins(int coinstoAdd){
		//add a coin and update the score
		coinCount += coinstoAdd;
		scoreText.text = "Score: " + coinCount;
	}

	public void HurtPlayer(int damageToTake){
		//subtract the damage taken from the health and update the UI
		healthCount -= damageToTake;
		UpdateHeartMeter ();
	}

	public void UpdateHeartMeter(){
		//Change the heart UI sprites based on health amount
		switch (healthCount) {
		case 6:
			heart1.sprite = heartFull;
			heart2.sprite = heartFull;
			heart3.sprite = heartFull;
			return;
		case 5:
			heart1.sprite = heartFull;
			heart2.sprite = heartFull;
			heart3.sprite = heartHalf;
			return;
		case 4:
			heart1.sprite = heartFull;
			heart2.sprite = heartFull;
			heart3.sprite = heartEmpty;
			return;
		case 3:
			heart1.sprite = heartFull;
			heart2.sprite = heartHalf;
			heart3.sprite = heartEmpty;
			return;
		case 2:
			heart1.sprite = heartFull;
			heart2.sprite = heartEmpty;
			heart3.sprite = heartEmpty;
			return;
		case 1:
			heart1.sprite = heartHalf;
			heart2.sprite = heartEmpty;
			heart3.sprite = heartEmpty;
			return;
		case 0:
			heart1.sprite = heartEmpty;
			heart2.sprite = heartEmpty;
			heart3.sprite = heartEmpty;
			return;
		default:
			heart1.sprite = heartEmpty;
			heart2.sprite = heartEmpty;
			heart3.sprite = heartEmpty;
			return;
		}
	}
}

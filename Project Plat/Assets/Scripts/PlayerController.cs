using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	private Rigidbody2D myRigidbody;

	public float jumpSpeed;

	public bool agility;
	public bool sprint;
	public bool scrum;

	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;

	public bool isGrounded;

	private Animator myAnim;

	public Vector3 respawnPosition;

	public LevelManager theLevelManager;

	public AudioSource jumpsound;

	public GameObject stompBox;

	void Start () {
		//assign the animator and rigidbody(movement/gravity) to the player
		myRigidbody = GetComponent<Rigidbody2D> ();
		myAnim = GetComponent<Animator> ();

		//the position that the player respawns is his first position when entering game
		respawnPosition = transform.position;

		//find the level manager
		theLevelManager = FindObjectOfType<LevelManager> ();
	}
	

	void Update () {
		//check if the player is on the ground
		isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);

		if (Input.GetAxisRaw ("Horizontal") > 0f) {
			//move the player right
			myRigidbody.velocity = new Vector3 (moveSpeed, myRigidbody.velocity.y, 0f);
			//have the player sprite look right
			transform.localScale = new Vector3 (1f, 1f, 1f);

		
		} else if (Input.GetAxisRaw ("Horizontal") < 0f) {
			//move the player left
			myRigidbody.velocity = new Vector3 (-moveSpeed, myRigidbody.velocity.y, 0f);
			//have the player sprite look left
			transform.localScale = new Vector3 (-1f, 1f, 1f);

		} else {
			//Have the player stop moving when no buttons are pressed
			myRigidbody.velocity = new Vector3 (0f, myRigidbody.velocity.y, 0f);
		}

		if (Input.GetButtonDown ("Jump") && isGrounded) {
			//have the player jump, only if he is grounded
			myRigidbody.velocity = new Vector3 (myRigidbody.velocity.x, jumpSpeed, 0f);
			//play jump sound
			jumpsound.Play ();
		}

		//set the speed and grounded variables for the animator conditions
		myAnim.SetFloat ("Speed", Mathf.Abs(myRigidbody.velocity.x));
		myAnim.SetBool ("Grounded", isGrounded);

		if (myRigidbody.velocity.y < 0) {
			//only activate the stompbox if the player is moving down
			stompBox.SetActive (true);
		} else {
			stompBox.SetActive (false);
		}

	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "KillPlane") {
			//if the player touches a kill plane, respawn the player
			theLevelManager.Respawn ();
		}

		if (other.tag == "Checkpoint") {
			//if the player touches a checkpoint, set the respawn position to the checkpoint
			respawnPosition = other.transform.position;
		}

		if (other.tag == "Agility") {
			//if the player gets the agility object
			agility = true;
			jumpSpeed *= 1.5f;
		}

		if (other.tag == "Sprint") {
			//if the player gets the sprint object
			sprint = true;
		}	

		if (other.tag == "Scrum") {
			//if the player gets the scrum object
			scrum = true;
		}	
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "MovingPlatform") {
			//if the player is on a moving platform, move with it
			transform.parent = other.transform;
		}
	
	}

	void OnCollisionExit2D(Collision2D other){
		if (other.gameObject.tag == "MovingPlatform") {
			//if the player leaves a moving platform, do not move with it
			transform.parent = null;
		}

	}
}

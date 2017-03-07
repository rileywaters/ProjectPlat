using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	//true when facing right
	public bool facing = true;
	public bool jump = true;
	public float force = 300f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public Transform groundCheck;

	private bool grounded = false;
	private Animator anim;
	private Rigidbody2D rbody;

	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
		rbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		if (Input.GetButtonDown("Jump") && grounded) {
			jump = true;
		}	
	}

	void FixedUpdate () {
		float h = Input.GetAxis("Horizontal");
		anim.SetFloat("Speed", Mathf.Abs (h));

		if (h * rbody.velocity.x < maxSpeed){
			rbody.AddForce(Vector2.right * h * force);
		}

		if (Mathf.Abs(rbody.velocity.x) > maxSpeed){
			rbody.velocity = new Vector2(Mathf.Sign(rbody.velocity.x) * maxSpeed, rbody.velocity.y);
		}

		if (h > 0 && !facing){
			Flip();
		} else if (h < 0 && facing) {
			Flip();
		}

		if(jump){
			anim.SetTrigger("Jump");
			rbody.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}
	}

	void Flip (){
		facing = !facing;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}

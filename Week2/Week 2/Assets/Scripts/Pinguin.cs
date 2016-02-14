using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;


public class Pinguin : MonoBehaviour {

	public float movementSpeed =5;

	Rigidbody2D rb;
	Animator anim;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}

	bool isGrounded = false;

	string lastState = "anim";


	// Update is called once per frame
	void FixedUpdate () {
		float direction = 0;
		direction = CrossPlatformInputManager.GetAxis ("Horizontal");
		//Debug.Log ("Direction = {0}", direction);
		var upDir = CrossPlatformInputManager.GetAxis ("Vertical");
		var newPos = rb.position;
		newPos.x += movementSpeed * direction * Time.deltaTime;
		rb.position = newPos;

		if (Mathf.Abs (direction) > 0.1f && lastState != "Running") {
			anim.SetTrigger ("Running");
			lastState = "Running";
		}
		if (Mathf.Abs (direction) < 0.1f && lastState != "Idle") {
			anim.SetTrigger ("Idle");
			lastState = "Idle";
		}
		if (direction > 0) {
			transform.localScale = new Vector3 (1, 1, 1);
		} else {
			transform.localScale = new Vector3 (-1, 1, 1);
		}

		if (upDir > 0.03f && isGrounded) {
			anim.SetTrigger ("Jumping");
			lastState = "Jumping";
			isGrounded = false;
			var velocity = rb.velocity;
			velocity.y = 5f;
			rb.velocity = velocity;
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.collider.tag.Contains("ground")) {
			isGrounded = true;
			Debug.Log ("Hit the ground");
		}
	}

}

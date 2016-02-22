using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
	public int score = 0;
	public float rotateSpeed = 5f;
	public float jumpHeight = 10f;
	public float moveSpeed = 5f;

	#region PlayerState
	bool isJumping = false;
	#endregion

	#region Component caches
	Rigidbody2D rb;
#endregion

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		CheckForMovement();
	}

	void CheckForMovement()
	{
		// Get the player input, if we're moving left or right, rotate the cube
		float direction = -CrossPlatformInputManager.GetAxis("Horizontal");
		if (direction != 0)
		{
			rb.angularVelocity = rotateSpeed * direction;
			rb.velocity = new Vector2(moveSpeed * -direction, rb.velocity.y);
		}

		if (!isJumping && CrossPlatformInputManager.GetAxis("Vertical") > 0.25f)
		{
			isJumping = true;
			// Make sure they don't do a tricky double-wall jump and sanitise the up velocity
			if (rb.velocity.y < jumpHeight * 0.5f)
				rb.velocity = new Vector2( rb.velocity.x, jumpHeight );
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		// Check for the ground!
		if (collision.collider.tag == "Ground")
			isJumping = false;
		

		//if (collision.relativeVelocity.magnitude > 2)
		//	audio.Play();

	}

	void OnCollisionExit2D(Collision2D collision)
	{
		// Let's see if we've left the ground, this might be fairly useful information to have
		if (collision.collider.tag == "Ground")
			isJumping = true;
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		// Check for the trigger
		if (other.tag == "Trigger")
		{
			TriggeredAction triggerScript = other.GetComponent<TriggeredAction>();
			triggerScript.Trigger();
		}
		else if (other.tag == "Pickup")
		{
			score++;
			other.gameObject.SetActive(false);
		}
	}

}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	public float speed;
	public Text countText;
	public Text winText;

	private Rigidbody rb;
	private int count;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText ();
		winText.gameObject.SetActive (false);
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);
	}

	void OnTriggerEnter(Collider other) 
	{
		Debug.Log (string.Format ("Player trigger {0}", other.gameObject.tag));
		if (other.gameObject.CompareTag ( "Pick Up"))
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}

	}
//	void OnCollisionEnter(Collision collision)
//	{
//		//Debug.Log (string.Format ("Player collided with {0}", collision.gameObject.tag));
//		if (collision.gameObject.CompareTag ( "Wall"))
//		{
//			Debug.Log (string.Format ("Wall: input velocity : {0} {1} {2}", rb.velocity.x, rb.velocity.y, rb.velocity.z));
//			rb.
////			float moveHorizontal = Input.GetAxis ("Horizontal");
////			float moveVertical = Input.GetAxis ("Vertical");
////
////			Vector3 movement = new Vector3 (-moveHorizontal, 0.0f, -moveVertical);
////
////			rb.AddForce (movement * speed);
//			//rb.velocity = rb.velocity * -10;
//
//			rb.velocity = new Vector3 (rb.velocity.x * 1, -rb.velocity.y, -rb.velocity.z * 10);
//
//			Debug.Log (string.Format ("Wall: output velocity : {0} {1} {2}", rb.velocity.x, rb.velocity.y, rb.velocity.z));
//		}
//	}

	void SetCountText ()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= 10)
		{
			//winText.text = "You Win!";
			winText.gameObject.SetActive(true);
		}
	}
}

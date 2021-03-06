﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	private Rigidbody rb;
	public float speed = 1;

	public Text countText;
	public Text winText;
	private int count;
//
	void Start ()
	{
		rb = GetComponent<Rigidbody>();

		count = 0;
		SetCountText ();
		winText.gameObject.SetActive (false);
	}
//
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

	void SetCountText ()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= 11)
		{
			//winText.text = "You Win!";
			winText.gameObject.SetActive(true);
		}
	}
}

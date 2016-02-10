using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public float speed = 10f;
	public float xVelocity = 1.0f;
	public float yVelocity = 1.0f;
	public float lowestZPos = -5f;
	public float minAngularVelocity = 0.2f;

	[Header("Sound")]
	public AudioClip bounceSound;
	public AudioClip brickSound;
	public AudioClip launchSound;

	// Component Caching
	Rigidbody rb;
	Transform paddleTransform;
	Vector3 resetPos;
	GameLogic logic;
	AudioSource audio;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		resetPos = transform.localPosition;
		paddleTransform = transform.parent;
		logic = GameObject.Find("/Logic").GetComponent<GameLogic>();
		audio = gameObject.AddComponent<AudioSource>();
		StartCoroutine(WaitForRelease());
	}
	
	IEnumerator WaitForRelease()
	{
		yield return new WaitForSeconds(3f);
		Fire();
	}

	// Update is called once per frame
	void Update () {
		// Check for failure
		if (transform.position.z < lowestZPos)
			Reset();

		// Check Y velocity sanity - sometimes the ball might end up going only sideways, which means the player will never get the ball back
		if (transform.parent == null)
			if (Mathf.Abs(rb.velocity.z) < minAngularVelocity || Mathf.Abs(rb.velocity.x) < minAngularVelocity)
			FixVelocity();
	}

	void FixVelocity()
	{
		Vector3 newVelocity = rb.velocity;
		if (newVelocity.z < 0)
			newVelocity.z = -minAngularVelocity;
		else
			newVelocity.z = minAngularVelocity;

		if (newVelocity.x < 0)
			newVelocity.x = -minAngularVelocity;
		else
			newVelocity.x = minAngularVelocity;


		rb.velocity = newVelocity;
	}

	void Reset()
	{
		rb.isKinematic = true;
		transform.parent = paddleTransform;
		transform.localPosition = resetPos;
		StartCoroutine(WaitForRelease());
	}

	public void Fire()
	{
		PlaySound(launchSound);
		transform.parent = null;				// Until we release, we're parented to the paddle so the player can position the ball for launch
		Vector3 newVelocity = new Vector3(xVelocity, 0, yVelocity);
		rb.isKinematic = false;
		rb.velocity = newVelocity;
		
	}

	void OnCollisionEnter(Collision collision)
	{
		ContactPoint contact = collision.contacts[0];
		
 		if (contact.otherCollider.tag == "Brick")
		{
			Brick hitBrick = contact.otherCollider.gameObject.GetComponent<Brick>();

			logic.BrickCollision(contact.point, hitBrick);
			if (hitBrick.Hits <= 0) {
				contact.otherCollider.gameObject.SetActive (false);
			}
			PlaySound(brickSound);
			return;
		}
		else if (contact.otherCollider.tag == "Wall")
		{
			logic.WallCollision(transform.position);
		}

		PlaySound(bounceSound);
	}

	void PlaySound(AudioClip newClip)
	{
		audio.clip = newClip;
		audio.Play();
	}
}

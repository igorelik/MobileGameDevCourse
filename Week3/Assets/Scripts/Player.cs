using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed = 5f;
	public float maxVelocity = 5f;
	public float maxXPos;
	public float maxZPos;

	public LayerMask controlMask;

	Vector3 targetPos;

	// Component caches
	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		transform.LookAt(targetPos);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		bool hitCollider = false;

		// Raycasting
		if (Input.GetMouseButton(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo, 100f, controlMask))
			{
				targetPos = hitInfo.point;
				hitCollider = true;
				Debug.DrawLine(transform.position, targetPos, Color.red);
				rb.angularVelocity = Vector3.zero;
			}
		}

		// Now we add velocity toward the target
		if (hitCollider)
		{
			Vector3 velocity = rb.velocity;

			// Look at the touch
			transform.LookAt(targetPos);
			rb.AddForce(transform.forward * speed);

			if (velocity.magnitude < maxVelocity)
			{
				velocity = Vector3.ClampMagnitude(velocity, maxVelocity);
			}
		}

		bool killVelocity = false;
		Vector3 currentPos = rb.position;

		// Clamp the position
		if (currentPos.x > maxXPos)
		{
			currentPos.x = (maxXPos) + Mathf.Epsilon;
			killVelocity = true;
		}
		else if (currentPos.x < -maxXPos)
		{
			currentPos.x = (-maxXPos) + Mathf.Epsilon;
			killVelocity = true;
		}

		if (currentPos.z > maxZPos)
		{
			currentPos.z = (maxZPos) + Mathf.Epsilon;
			killVelocity = true;
		}
		else if (currentPos.z < -maxZPos)
		{
			currentPos.z = (-maxZPos) + Mathf.Epsilon;
			killVelocity = true;
		}

		// If we've hit a wall, we should reset movement speed
		if (killVelocity)
		{
			rb.angularVelocity = Vector3.zero;
			rb.velocity = Vector3.zero;
			rb.position = currentPos;
		}


	}
}

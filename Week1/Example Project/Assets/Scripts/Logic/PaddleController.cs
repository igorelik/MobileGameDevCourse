using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour {

	[Header("Input Controls")]
	[Tooltip("This is automatically toggled depending on platform but can be overridden at any point for testing")]
	public bool isMobile = false;

	[Header("Gameplay")]
	[Range(0,10f)]
	public float speed = 5f;
	[Tooltip("The farthest left the paddle can move")]
	public float minX = -3.3f;
	[Tooltip("The farthest right the paddle can move")]
	public float maxX = 3.3f;

	GameLogic logic;

	// Use this for initialization
	void Start () {
#if (UNITY_IPHONE || UNITY_ANDROID)
		// Switch platform input
		isMobile = true;
#endif

		logic = GameObject.Find("/Logic").GetComponent<GameLogic>();
	}
	
	// Update is called once per frame
	void Update () {
		if (isMobile)
			CheckTouchInput();
		else
			CheckInput();
	}

	void CheckTouchInput()
	{
		// This input is based on a mouse pointer, which will still work on mobile (although not amazingly)
		// For now, this allows us to better test on PC/Mac without building to mobile device
		// Proper multi touch will be covered later.

		if (Input.GetMouseButton(0))							// Is the player touching the screen?
		{
			Vector2 touchPosition = Input.mousePosition;		// Get the position

			float direction = 0;								// Default direction is 0 (still)
			if (touchPosition.x < Screen.width * 0.5f)			// If we touch the left side of the screen, the paddle goes left, right for right.
				direction = -1f;
			else
				direction = 1f;

			MovePaddle(direction);
		}
	}

	void CheckInput()
	{
		// Let's check for left or right buttons
		// We can check for individual buttons as well, but for now we'll use Unity's input handler
		float horizInput = Input.GetAxis("Horizontal");

		MovePaddle(horizInput);
	}

	void MovePaddle(float direction)
	{
		Vector3 newPos = transform.position;													// Sample our current position
		newPos.x = Mathf.Clamp(newPos.x += (direction * speed) * Time.deltaTime, minX, maxX);	// Add the direction, and clamp it so it can't leave the level
		transform.position = newPos;															// Apply the change
	}
}

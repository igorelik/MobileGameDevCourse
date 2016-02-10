using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

	[Tooltip("The reward for when the ball breaks this object")]
	public int score;

	[Tooltip("Number of time ball needs to hit the break to make it disappear")]
	public int Hits = 1;
		
}

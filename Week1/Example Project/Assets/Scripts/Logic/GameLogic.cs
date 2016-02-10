using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

	public int totalScore;
	public ParticleSystem brickHitEmitter;
	public ParticleSystem wallHitEmitter;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void BrickCollision(Vector3 pos, Brick brick)
	{
		brickHitEmitter.transform.position = pos;
		brickHitEmitter.Emit(brick.score);
		if (brick.Hits > 0) {
			brick.Hits--;
		}
		totalScore += brick.score;
	}

	public void WallCollision(Vector3 pos)
	{
		wallHitEmitter.transform.position = pos;
		wallHitEmitter.Emit(4);
	}
}

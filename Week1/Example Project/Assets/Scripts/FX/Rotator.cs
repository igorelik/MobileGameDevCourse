using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	public float speed;
	public Vector3 vector;
	public bool localSpace = false;

	Transform myTrans;
	Vector3 newEulerAngle;

	// Use this for initialization
	void Start () {
		myTrans = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (localSpace)
			myTrans.Rotate(vector * speed * Time.deltaTime, Space.Self);
		else
			myTrans.Rotate(vector * speed * Time.deltaTime, Space.World);
	}
}

using UnityEngine;
using System.Collections;

public class BackgroundCamera : MonoBehaviour {

	public Transform mainCamTrans;
	public float parallaxAmount = 0.25f;

	// Use this for initialization
	void Start () {
		mainCamTrans = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos = transform.position;
		newPos.x = mainCamTrans.position.x * parallaxAmount;
		transform.position = newPos;
	}
}

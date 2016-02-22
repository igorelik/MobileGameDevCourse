using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

	float zPos = -10f;
	public Transform followTrans;
	public float smoothTime = 1f;

	Vector3 currentVelocity = Vector3.zero;

	// Update is called once per frame
	void LateUpdate () {
		Vector3 targetPos = followTrans.position;
		Vector3 oldPos = transform.position;
		Vector3 newPos = Vector3.SmoothDamp(oldPos, targetPos, ref currentVelocity, smoothTime);
		newPos.z = zPos;

		transform.position = newPos;
	}
}

using UnityEngine;
using System.Collections;

public class CamraController : MonoBehaviour {
    public GameObject Player;

    private Vector3 offset;
    // Use this for initialization
    void Start ()
    {
	    offset = transform.position - Player.transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Player.transform.position + offset;
    }
}

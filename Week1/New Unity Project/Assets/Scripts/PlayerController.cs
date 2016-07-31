using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float Speed = 10;

    private Rigidbody rb;

	// Use this for initialization
	void Start ()
	{
	    rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
	{
        var horVelocity = Input.GetAxis("Horizontal");
        var verVelocity = Input.GetAxis("Vertical");

        rb.AddForce(horVelocity * Speed, 0, verVelocity * Speed);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
        }
    }
}

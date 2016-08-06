using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	private Rigidbody rb;

	public float speed = 10.0f;
	public float tilt;

	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
	}

	void Update () 
	{
	
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0.0f);
		rb.velocity = movement * speed;

		rb.position = new Vector3 
		(
			rb.position.x,
			rb.position.y,
			0
		);

		rb.rotation = Quaternion.Euler (0.0f, rb.velocity.x * - tilt, 0.0f);
	}
}

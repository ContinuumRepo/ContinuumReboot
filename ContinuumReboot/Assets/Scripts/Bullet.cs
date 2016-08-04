using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
	//private Player playerControllerScript;
	private Game gameControllerScript;
	public float speed;
	public Rigidbody rb;
	public bool useRigidBody;
	public Vector3 ForceVector;
	public bool xAxis, yAxis, zAxis;
	public float destroyDistanceA = 2000.0f;
	public float destroyDistanceB = -100.0f;
	public bool follower;

	void Start () 
	{
		//GameObject playerObject = GameObject.FindGameObjectWithTag ("PlayerObject");
		//playerControllerScript = playerObject.GetComponent<Player> ();

		GameObject gameObjectObject = GameObject.FindGameObjectWithTag ("GameController");
		gameControllerScript = gameObjectObject.GetComponent<Game> ();

		if (useRigidBody == true) 
		{
			rb = GetComponent<Rigidbody> ();
		}

		if (rb == null) 
		{
			//Debug.Log ("Object has no rigidbody");
		}
	}

	void Update () 
	{
		if (gameControllerScript.isGameOver == false) 
		{
			if (useRigidBody == true) 
			{
				// Adds force
				rb.AddRelativeForce (ForceVector);
			}

			if (transform.position.z > destroyDistanceA || transform.position.z < destroyDistanceB) 
			{
				Destroy (gameObject);
			}

			// One Axis
			if (xAxis == true && yAxis == false && zAxis == false) 
			{
				transform.Translate (Vector3.right * speed * Time.deltaTime);
			}

			if (xAxis == false && yAxis == true && zAxis == false) 
			{
				transform.Translate (Vector3.up * speed * Time.deltaTime);
			}

			if (xAxis == false && yAxis == false && zAxis == true) 
			{
				transform.Translate (Vector3.forward * speed * Time.deltaTime);
			}

			// Two Axis
			if (xAxis == true && yAxis == true && zAxis == false) 
			{
				transform.Translate (new Vector3 (1, 1, 0) * speed * Time.deltaTime);
			}
		
			if (xAxis == true && yAxis == true && zAxis == false) 
			{
				transform.Translate (new Vector3 (1, 1, 0) * speed * Time.deltaTime);
			}
		
			if (xAxis == true && yAxis == false && zAxis == true) 
			{
				transform.Translate (new Vector3 (1, 0, 1) * speed * Time.deltaTime);
			}

			// All axis
			if (xAxis == true && yAxis == true && zAxis == true) 
			{
				transform.Translate (new Vector3 (1, 1, 1) * speed * Time.deltaTime);
			}

			// No axis
			if (xAxis == false && yAxis == false && zAxis == false) 
			{
				transform.Translate (new Vector3 (0, 0, 0) * speed * Time.deltaTime);
			}
		}

		if (gameControllerScript.isGameOver == true) 
		{
			transform.Translate (Vector3.zero);
		}
	}
}

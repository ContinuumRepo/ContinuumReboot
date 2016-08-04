using UnityEngine;
using System.Collections;
[System.Serializable]

public class axes 
{
	public bool X, Y, Z;
}

public class RotateByDistance : MonoBehaviour {

	public axes Axes;
	public Transform Player;
	public float speed;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () 
	{
		// X only
		if (Axes.X == true && Axes.Y == false && Axes.Z == false)
		{
			gameObject.transform.Rotate 
				(
					1 * speed * Time.deltaTime / Vector3.Distance(gameObject.transform.position, Player.transform.position),
					0, 
					0
				);
		}

		// Y only
		if (Axes.X == false && Axes.Y == true && Axes.Z == false)
		{
			gameObject.transform.Rotate 
			(
				0,
				1 * speed * Time.deltaTime / Vector3.Distance(gameObject.transform.position, Player.transform.position), 
				0
			);
		}

		// Z only
		if (Axes.X == false && Axes.Y == false && Axes.Z == true)
		{
			gameObject.transform.Rotate 
				(
					0,
					0, 
					1 * speed * Time.deltaTime / Vector3.Distance(gameObject.transform.position, Player.transform.position)
				);
		}

		// X and Y
		if (Axes.X == true && Axes.Y == true && Axes.Z == false)
		{
			gameObject.transform.Rotate 
				(
					1 * speed * Time.deltaTime / Vector3.Distance(gameObject.transform.position, Player.transform.position),
					1 * speed * Time.deltaTime / Vector3.Distance(gameObject.transform.position, Player.transform.position), 
					0
				);
		}

		// X and Z
		if (Axes.X == true && Axes.Y == false && Axes.Z == true)
		{
			gameObject.transform.Rotate 
				(
					1 * speed * Time.deltaTime / Vector3.Distance(gameObject.transform.position, Player.transform.position),
					0, 
					1 * speed * Time.deltaTime / Vector3.Distance(gameObject.transform.position, Player.transform.position)
				);
		}

		// Y and Z
		if (Axes.X == false && Axes.Y == true && Axes.Z == true)
		{
			gameObject.transform.Rotate 
				(
					0,
					1 * speed * Time.deltaTime / Vector3.Distance(gameObject.transform.position, Player.transform.position), 
					1 * speed * Time.deltaTime / Vector3.Distance(gameObject.transform.position, Player.transform.position)
				);
		}

		// All X and Y and Z axes
		if (Axes.X == true && Axes.Y == true && Axes.Z == true)
		{
			gameObject.transform.Rotate 
				(
					1 * speed * Time.deltaTime / Vector3.Distance(gameObject.transform.position, Player.transform.position),
					1 * speed * Time.deltaTime / Vector3.Distance(gameObject.transform.position, Player.transform.position), 
					1 * speed * Time.deltaTime / Vector3.Distance(gameObject.transform.position, Player.transform.position)
				);
		}
	}
}

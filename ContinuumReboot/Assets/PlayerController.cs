using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public GameObject Player;
	public GameObject Geom;
	public float moveSpeed;
	public float tilt;
	public float xMin, xMax, yMin, yMax, zMin, zMax;
	public Vector2 mousePosition;
	public Vector3 newPos;
	public float offsetX, offsetY, offsetZ;
	public Vector2 MousePos;
	public Transform DivisionPos;
	public Transform DivisionPosA;
	public Transform DivisionPosB;
	public Transform DivisionPosC;
	public SmoothFollowOrig smoothFollowScript;

	[Range (1,3)]
	public int Division = 2;

	[Range (0,5)]
	public float sensitivity;

	void Start () 
	{
		mousePosition = new Vector3 (offsetX, offsetY, offsetZ);
		Division = 2;
	}
	


	void Update () 
	{
		smoothFollowScript.target = DivisionPos.transform;
/*
		MousePos = mousePosition;
		//mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Geom.transform.position.z);
		mousePosition =	Camera.main.ScreenToWorldPoint (mousePosition);
*/
		gameObject.transform.position = new Vector3
			(
				Mathf.Clamp(gameObject.transform.position.x, xMin, xMax),
				Mathf.Clamp(gameObject.transform.position.y, yMin, yMax),
				Mathf.Clamp(gameObject.transform.position.z, zMin, zMax)
			);

		if (Division == 1) 
		{
			DivisionPos = DivisionPosA.transform;
		}

		if (Division == 2) 
		{
			DivisionPos = DivisionPosB.transform;
		}

		if (Division == 3) 
		{
			DivisionPos = DivisionPosC.transform;
		}

	}

	void FixedUpdate ()
	{
		/*
		if (Input.GetMouseButton (0)) 
		{
			Cursor.visible = false;
			transform.position = Vector3.Lerp (transform.position, mousePosition * sensitivity, moveSpeed);
		
			GetComponent<Rigidbody> ().position = new Vector3
			(
				Mathf.Clamp (GetComponent<Rigidbody> ().position.x, xMin, xMax),
				Mathf.Clamp (GetComponent<Rigidbody> ().position.y, yMin, yMax), 
				Mathf.Clamp (GetComponent<Rigidbody> ().position.z, zMin, zMax)
			);
		}*/

		if (Input.GetMouseButtonUp (0)) 
		{
			Cursor.visible = true;
		}

		GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().position.x * tilt);
	}
}

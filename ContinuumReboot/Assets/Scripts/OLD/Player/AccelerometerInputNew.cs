using UnityEngine;
using System.Collections;

public class AccelerometerInputNew : MonoBehaviour 

{
	public float Xmult = 1.0f;
	public float Ymult = 1.0f; 
	public float xMin, xMax, yMin, yMax;
	public float startingRotation;
	public float dampenRotate;
	public bool isForMobile;
	public float speed = 10.0f;

	void FixedUpdate () 
	{
		if (isForMobile == true) 
		{
			GameObject PlayerRotateObject = GameObject.FindGameObjectWithTag ("PlayerRotate");
			PlayerRotateObject.transform.rotation = Quaternion.Euler
				(
					(-Input.acceleration.y * speed * Time.unscaledDeltaTime), 
					0,
					0
				);
			// Adds Boundaries
			GetComponent<Transform>().rotation = Quaternion.Euler 
				(
					Mathf.Clamp(GetComponent<Transform>().rotation.x, xMin, xMax), 
				 	0, 
				 	0
				);
		}


		/*

		GameObject PlayerRotateObject = GameObject.FindGameObjectWithTag ("PlayerRotate");
		PlayerRotateObject.transform.Rotate
			(
				(-Input.acceleration.y * Time.timeScale + startingRotation) / dampenRotate, 
				0,
				0
				);
		
		GameObject PlayerActual = GameObject.FindGameObjectWithTag ("Player");
		//create boundries
		PlayerActual.GetComponent<Transform>().position = new Vector3
			(
				Mathf.Clamp(GetComponent<Transform>().position.x, -3, 3), 
				Mathf.Clamp(GetComponent<Transform>().position.y, 62, 62),
				Mathf.Clamp(GetComponent<Transform>().position.z, 0, 0)
				);
		
		PlayerRotateObject.GetComponent<Transform>().rotation = Quaternion.Euler
			(
				Mathf.Clamp(GetComponent<Transform>().rotation.x, 262, 283), 
				Mathf.Clamp(GetComponent<Transform>().rotation.y, 0, 0),
				Mathf.Clamp(GetComponent<Transform>().rotation.z, 0, 0)
				);
				*/
	}
}
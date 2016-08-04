using UnityEngine;
using System.Collections;

public class AccelerometerInput : MonoBehaviour 

{
	public float Xmult = 1.0f;
	public float Ymult = 1.0f; 
	public float xMin, xMax, yMin, yMax;
	void FixedUpdate () 
	{
		transform.Translate(Input.acceleration.x * Xmult, Input.acceleration.y * Ymult + 0.45f, 0);

		//create boundries
		GetComponent<Rigidbody>().position = new Vector2(
			Mathf.Clamp(GetComponent<Rigidbody>().position.x, xMin, xMax), 
			Mathf.Clamp(GetComponent<Rigidbody>().position.y, yMin, yMax)
			);
	}
}
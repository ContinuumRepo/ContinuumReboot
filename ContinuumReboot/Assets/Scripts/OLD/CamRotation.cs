using UnityEngine;
using System.Collections;

public class CamRotation : MonoBehaviour 
{
	public float amount;

	void Start () 
	{
	
	}

	void FixedUpdate () 
	{
		gameObject.transform.rotation = Quaternion.Euler 
			(
				transform.rotation.x, 
				transform.rotation.y, 
				transform.position.x * amount
			);
	}
}

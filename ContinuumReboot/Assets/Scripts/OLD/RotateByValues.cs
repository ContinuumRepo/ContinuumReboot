using UnityEngine;
using System.Collections;

public class RotateByValues : MonoBehaviour 
{
	public float amount;
	public Vector3 offset;

	void Start () 
	{
	
	}

	void FixedUpdate () 
	{
		gameObject.transform.localRotation = Quaternion.Euler 
			(
				offset.x, 
				offset.y, 
				(transform.localPosition.x * amount) + offset.z
			);
	}
}

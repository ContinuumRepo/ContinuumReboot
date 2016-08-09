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
		gameObject.transform.localRotation = Quaternion.Euler 
			(
				transform.localRotation.x, 
				transform.localRotation.y, 
				transform.localPosition.x * amount
			);
	}
}

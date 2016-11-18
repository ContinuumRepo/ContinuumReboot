using UnityEngine;
using System.Collections;

public class RotateByValues : MonoBehaviour 
{
	public float amount;
	public Vector3 offset;
	public enum axis {X, Y, Z}
	public axis Axis;

	void Start () 
	{
	
	}

	void FixedUpdate () 
	{
		if (Axis == axis.X) 
		{
			gameObject.transform.localRotation = Quaternion.Euler 
				(
					(transform.localPosition.x * amount) + offset.x,
					offset.y,
					offset.z 
				);
		}

		if (Axis == axis.Y) 
		{
			gameObject.transform.localRotation = Quaternion.Euler 
				(
					offset.x, 
					(transform.localPosition.x * amount) + offset.y,
					offset.z 
				);
		}

		if (Axis == axis.Z) 
		{
			gameObject.transform.localRotation = Quaternion.Euler 
			(
				offset.x, 
				offset.y, 
				(transform.localPosition.x * amount) + offset.x
			);
		}
	}
}

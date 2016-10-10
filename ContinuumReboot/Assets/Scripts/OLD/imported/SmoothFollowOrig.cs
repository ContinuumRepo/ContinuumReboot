﻿using UnityEngine;

public class SmoothFollowOrig : MonoBehaviour
{
	public bool OnlyInStart;
	public float SMOOTH_TIME = 0.3f;
	public Vector3 offset;
	
	#region Public Properties
	public bool LockX;
	public bool LockY;
	public bool LockZ;
	public bool useSmoothing;
	public Transform target;
	#endregion
	
	#region Private Properties
	private Transform thisTransform;
	public Vector3 velocity;
	#endregion

	public float xMin, xMax;
	public float yMin, yMax;
	public float zMin, zMax;
	public bool useRB;
	public float lockedPos;
	public bool hasLockedPos;

	private void Awake()
	{
		thisTransform = transform;
		velocity = new Vector3(0.5f, 0.5f, 0.5f);

		if (target == null) 
		{
			target = GameObject.FindGameObjectWithTag ("Player").transform;
		}
	}

	void start ()
	{
		if (target == null) 
		{
			target = GameObject.FindGameObjectWithTag ("Player").transform;
		}

		if (OnlyInStart == true) 
		{
			if (useRB == true) 
			{
				GetComponent<Transform> ().position = new Vector3
				(
					Mathf.Clamp (GetComponent<Rigidbody> ().position.x, xMin, xMax),
					Mathf.Clamp (GetComponent<Rigidbody> ().position.y, yMin, yMax),
					Mathf.Clamp (GetComponent<Rigidbody> ().position.z, zMin, zMax)
				);
			}

			var newPos = Vector3.zero;
			
			if (useSmoothing) 
			{
				newPos.x = Mathf.SmoothDamp (thisTransform.position.x, target.position.x, ref velocity.x, SMOOTH_TIME);
				newPos.y = Mathf.SmoothDamp(thisTransform.position.y, target.position.y, ref velocity.y, SMOOTH_TIME);
				newPos.z = Mathf.SmoothDamp (thisTransform.position.z, target.position.z, ref velocity.z, SMOOTH_TIME);
			} 

			else 
			
			{
				thisTransform.position = new Vector3 (target.position.x, target.position.y, target.position.z);
			}
			
			#region Locks
			if (LockX) 
			{
				newPos.x = thisTransform.position.x + offset.x;
			}
			
			if (LockY) 
			{
				newPos.y = thisTransform.position.y + offset.y;
			}
			
			if (LockZ) 
			{
				newPos.z = thisTransform.position.z + offset.z;
			}
			#endregion
		}
	}

	public void LateUpdate()
	{
		if (hasLockedPos == true) 
		{
			gameObject.GetComponent<Transform> ().position = new Vector3 (lockedPos, gameObject.transform.position.y, gameObject.transform.position.z);
		}
		
		if (OnlyInStart == false) 
		{
			if (useRB == true) 
			{
				GetComponent<Transform> ().position = new Vector3
				(
					Mathf.Clamp (GetComponent<Rigidbody> ().position.x, xMin, xMax),
					Mathf.Clamp (GetComponent<Rigidbody> ().position.y, yMin, yMax),
					Mathf.Clamp (GetComponent<Rigidbody> ().position.z, zMin, zMax)
				);
			}

			var newPos = Vector3.zero;
			
			if (useSmoothing) 
			{
				newPos.x = Mathf.SmoothDamp (thisTransform.position.x, target.position.x, ref velocity.x, SMOOTH_TIME);
				newPos.y = Mathf.SmoothDamp (thisTransform.position.y, target.position.y, ref velocity.y, SMOOTH_TIME);
				newPos.z = Mathf.SmoothDamp (thisTransform.position.z, target.position.z, ref velocity.z, SMOOTH_TIME);
			} 

			else 
			
			{
				newPos.x = target.position.x + offset.x;
				newPos.y = target.position.y + offset.y;
				newPos.z = target.position.z + offset.z;
			}
			
			#region Locks
			if (LockX) 
			{
				newPos.x = thisTransform.position.x + offset.x;
			}
			
			if (LockY) 
			{
				newPos.y = thisTransform.position.y + offset.y;
			}
			
			if (LockZ) 
			{
				newPos.z = thisTransform.position.z + offset.z;
			}
			#endregion
			
			transform.position = Vector3.Slerp (transform.position, newPos, Time.time);
		}
	}
}
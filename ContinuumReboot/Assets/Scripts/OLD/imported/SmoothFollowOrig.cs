using UnityEngine;

public class SmoothFollowOrig : MonoBehaviour
{
	public bool OnlyInStart;
	public float SMOOTH_TIME = 0.3f;

	
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
	public bool isMainCam;
	public float camRotationX = 10.0f;
	public bool PlayerM;

	private void Awake()
	{
		thisTransform = transform;
		
		velocity = new Vector3(0.5f, 0.5f, 0.5f);
	}

	void start ()
	{

		if (OnlyInStart == true) 
		{
			if (useRB == true) {
				GetComponent<Transform> ().position = new Vector3
					(
						Mathf.Clamp (GetComponent<Rigidbody> ().position.x, xMin, xMax),
						Mathf.Clamp (GetComponent<Rigidbody> ().position.y, yMin, yMax),
						Mathf.Clamp (GetComponent<Rigidbody> ().position.z, zMin, zMax)
						);
			}
			var newPos = Vector3.zero;
			
			if (useSmoothing) {
				newPos.x = Mathf.SmoothDamp (thisTransform.position.x, target.position.x, ref velocity.x, SMOOTH_TIME);
				newPos.y = Mathf.SmoothDamp(thisTransform.position.y, target.position.y, ref velocity.y, SMOOTH_TIME);
				newPos.z = Mathf.SmoothDamp (thisTransform.position.z, target.position.z, ref velocity.z, SMOOTH_TIME);
			} else {
				newPos.x = target.position.x;
				newPos.y = target.position.y;
				newPos.z = target.position.z;
			}
			
			#region Locks
			if (LockX) {
				newPos.x = thisTransform.position.x;
			}
			
			if (LockY) {
				newPos.y = thisTransform.position.y;
			}
			
			if (LockZ) {
				newPos.z = thisTransform.position.z;
			}
			#endregion
			
			transform.position = Vector3.Slerp (transform.position, newPos, Time.time);
			//Mathf.Clamp (transform.position.x, xMin, xMax);
		}
	}
	
	// ReSharper disable UnusedMember.Local
	public void LateUpdate()
		// ReSharper restore UnusedMember.Local
	{
		if (PlayerM == true) {
			if (hasLockedPos == true) {
				gameObject.GetComponent<Transform> ().position = new Vector3 (lockedPos, gameObject.transform.position.y, gameObject.transform.position.z);
			}


			if (OnlyInStart == false) {
				if (useRB == true) {
					GetComponent<Transform> ().position = new Vector3
			(
				Mathf.Clamp (GetComponent<Rigidbody> ().position.x, xMin, xMax),
				Mathf.Clamp (GetComponent<Rigidbody> ().position.y, yMin, yMax),
				Mathf.Clamp (GetComponent<Rigidbody> ().position.z, zMin, zMax)
					);
				}
				var newPos = Vector3.zero;
		
				if (useSmoothing) {
					newPos.x = Mathf.SmoothDamp (thisTransform.position.x, target.position.x, ref velocity.x, SMOOTH_TIME);
					newPos.y = Mathf.SmoothDamp (thisTransform.position.y, target.position.y, ref velocity.y, SMOOTH_TIME);
					newPos.z = Mathf.SmoothDamp (thisTransform.position.z, target.position.z, ref velocity.z, SMOOTH_TIME);
				} else {
					newPos.x = target.position.x;
					newPos.y = target.position.y;
					newPos.z = target.position.z;
				}
		
				#region Locks
				if (LockX) {
					newPos.x = thisTransform.position.x;
				}
		
				if (LockY) {
					newPos.y = thisTransform.position.y;
				}
		
				if (LockZ) {
					newPos.z = thisTransform.position.z;
				}
				#endregion
		
				transform.position = Vector3.Slerp (transform.position, newPos, Time.time);
				//Mathf.Clamp (transform.position.x, xMin, xMax);
			}
		}

		if (PlayerM == false) {
			if (hasLockedPos == true) {
				gameObject.GetComponent<Transform> ().position = new Vector3 (lockedPos, gameObject.transform.position.y, gameObject.transform.position.z);
			}
			
			if (isMainCam == true) {
				//GameObject PlayerPivot = GameObject.FindGameObjectWithTag ("PlayerRotate");
				// gameObject.transform.rotation = Quaternion.Euler (PlayerPivot.transform.rotation.x * 10, 0, 0);
				//gameObject.transform.LookAt (PlayerPivot.transform);
			}
			
			if (OnlyInStart == false) {
				if (useRB == true) {
					GetComponent<Transform> ().position = new Vector3
						(
							Mathf.Clamp (GetComponent<Rigidbody> ().position.x, xMin, xMax),
							Mathf.Clamp (GetComponent<Rigidbody> ().position.y, yMin, yMax),
							Mathf.Clamp (GetComponent<Rigidbody> ().position.z, zMin, zMax)
							);
				}
				var newPos = Vector3.zero;
				
				if (useSmoothing) {
					newPos.x = Mathf.SmoothDamp (thisTransform.position.x, target.position.x, ref velocity.x, SMOOTH_TIME);
					newPos.y = Mathf.SmoothDamp (thisTransform.position.y, target.position.y, ref velocity.y, SMOOTH_TIME);
					newPos.z = Mathf.SmoothDamp (thisTransform.position.z, target.position.z, ref velocity.z, SMOOTH_TIME);
				} else {
					newPos.x = target.position.x;
					newPos.y = target.position.y;
					newPos.z = target.position.z;
				}
				
				#region Locks
				if (LockX) {
					newPos.x = thisTransform.position.x;
				}
				
				if (LockY) {
					newPos.y = thisTransform.position.y;
				}
				
				if (LockZ) {
					newPos.z = thisTransform.position.z;
				}
				#endregion
				
				transform.position = Vector3.Slerp (transform.position, newPos, Time.time);
				//Mathf.Clamp (transform.position.x, xMin, xMax);
			}
		}

	}
}

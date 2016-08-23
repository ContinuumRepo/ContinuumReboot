using UnityEngine;
using System.Collections;

public class AIScript : MonoBehaviour 
{
	public Transform target;
	public int moveSpeed = 20;
	public int rotationSpeed = 45;

	void Start() 
	{
		target = GameObject.FindGameObjectWithTag("Cube").transform;
	}

	void Update() 
	{    
		if (target != null) 
		{
			Vector3 dir = target.position - transform.position;
			// Only needed if objects don't share 'z' value.
			dir.z = 0.0f;
			if (dir != Vector3.zero) 
				transform.rotation = Quaternion.Slerp ( transform.rotation, 
					Quaternion.FromToRotation (Vector3.right, dir), 
					rotationSpeed * Time.deltaTime);

			//Move Towards Target
			transform.position += (target.position - transform.position).normalized 
				* moveSpeed * Time.deltaTime;
		}

		if (target == null) 
		{
			Destroy (gameObject);
		}
	}
}
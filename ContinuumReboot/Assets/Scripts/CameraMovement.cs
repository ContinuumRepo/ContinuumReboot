using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour 
{

	void Start () 
	{
		GetComponent<Transform> ().position = new Vector3 (0.2f, 2.1f, -188.1f);
		GetComponent<Transform> ().rotation = Quaternion.Euler (0, 0, 225);
		GetComponent<Animator> ().enabled = false;
	}

	void Update () 
	{
		if (GetComponent<SmoothFollowOrig> ().target == null) 
		{
			GetComponent<SmoothFollowOrig> ().target = GameObject.Find ("Player 1").transform;
		}
	}
}

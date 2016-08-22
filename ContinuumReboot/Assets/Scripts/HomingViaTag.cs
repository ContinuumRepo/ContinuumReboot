using UnityEngine;
using System.Collections;

public class HomingViaTag : MonoBehaviour 
{
	public string tagName = "Cube";
	private Transform homingObject;
	private SmoothFollowOrig smoothFollowScript;

	void Start () 
	{
		homingObject = GameObject.FindGameObjectWithTag (tagName).transform;
		smoothFollowScript = GetComponent<SmoothFollowOrig> ();
		smoothFollowScript.target = homingObject.transform;

		if (homingObject == null || homingObject.transform == null) 
		{
			Destroy (gameObject);
		}
	}

	void Update () 
	{
		if (homingObject == null || homingObject.transform == null) 
		{
			Destroy (gameObject);
		}
	}
}

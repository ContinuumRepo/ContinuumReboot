﻿using UnityEngine;
using System.Collections;

namespace UnityStandardAssets.Utility
{

public class BulletScript : MonoBehaviour 
{
	private AutoMoveAndRotate MoveAndRotateScript;
	public float newSpeed = -70.0f;

	void Start () 
	{
		MoveAndRotateScript = GetComponent<AutoMoveAndRotate> ();
	}

	void Update ()
	{
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Brick" || other.tag == "Cube") 
		{
			gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(-360, 360));
			MoveAndRotateScript.moveUnitsPerSecond.value = new Vector3 (0.0f, newSpeed, 0.0f);
			Debug.Log ("Ricoshet from hit object!");
		}

		if (other.tag == "Barrier") 
		{
			gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(-360, 360));
			Debug.Log ("Ricoshet from barrier!");
		}
	}
}
}

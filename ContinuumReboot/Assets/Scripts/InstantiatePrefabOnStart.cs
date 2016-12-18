using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePrefabOnStart : MonoBehaviour
{
	public GameObject Object;

	void Start () 
	{
		Instantiate (Object, Vector3.zero, Quaternion.identity);
	}

	void Update ()
	{
		GetComponent<InstantiatePrefabOnStart> ().enabled = false;
	}
}

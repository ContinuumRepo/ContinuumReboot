﻿using UnityEngine;
using System.Collections;

public class PressAnyKeyDeactivate : MonoBehaviour 
{
	public GameObject Deactivator;
	public GameObject Enabler;
	public AudioSource PressStartSound;

	void Start ()
	{
		Enabler.SetActive (false);
		Deactivator.SetActive (true);
	}

	void Update () 
	{
		if (Input.anyKeyDown) 
		{
			Enabler.SetActive (true);
			Deactivator.SetActive (false);
			PressStartSound.Play ();
			GetComponent<PressAnyKeyDeactivate> ().enabled = false;
		}
	}
}

using UnityEngine;
using System.Collections;

public class PressAnyKeyDeactivate : MonoBehaviour 
{
	public GameObject Deactivator;
	public GameObject Enabler;
	public GameObject SocialMedia;
	public AudioSource PressStartSound;

	void Start ()
	{
		Enabler.SetActive (false);
		Deactivator.SetActive (true);
		SocialMedia.SetActive (false);
	}

	void Update () 
	{
		if (Input.anyKeyDown) 
		{
			Enabler.SetActive (true);
			SocialMedia.SetActive (true);
			Deactivator.SetActive (false);
			PressStartSound.Play ();
			GetComponent<PressAnyKeyDeactivate> ().enabled = false;
		}
	}
}

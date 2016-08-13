using UnityEngine;
using System.Collections;

public class PressAnyKeyDeactivate : MonoBehaviour 
{
	public GameObject Deactivator;
	public GameObject Enabler;
	public GameObject SocialMedia;
	public AudioSource PressStartSound;
	public bool useInput;
	public Animator TitleAnim;

	void Start ()
	{
		Enabler.SetActive (false);
		Deactivator.SetActive (true);
		SocialMedia.SetActive (false);
	}

	void Update () 
	{
		if (Input.anyKeyDown && useInput == true) 
		{
			Enabler.SetActive (true);
			SocialMedia.SetActive (true);
			Deactivator.SetActive (false);
			PressStartSound.Play ();
			TitleAnim.Play ("MoveUp");
			GetComponent<PressAnyKeyDeactivate> ().enabled = false;
		}
	}
}

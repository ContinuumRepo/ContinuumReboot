using UnityEngine;
using System.Collections;

public class DestroyOrDeactivateByTime : MonoBehaviour 
{
	public float delay;
	public bool deactivateOnly;
	public bool activateOnly;
	public GameObject ActiveObject;
	public PressAnyKeyDeactivate PressAnyKeyScript;

	void Start () 
	{
		if (deactivateOnly == true) 
		{
			StartCoroutine (Deactivate ());
		}

		if (activateOnly == true) 
		{
			ActiveObject.SetActive (false);
			PressAnyKeyScript.useInput = false;
			StartCoroutine (Activate ());
		}

		if (deactivateOnly == false && activateOnly == false) 
		{
			Destroy (gameObject, delay);
		}


	}

	void Update () 
	{
	
	}

	IEnumerator Deactivate ()
	{
		yield return new WaitForSeconds (delay);
		gameObject.SetActive (false);
	}

	IEnumerator Activate ()
	{
		yield return new WaitForSeconds (delay);
		ActiveObject.SetActive (true);
		PressAnyKeyScript.useInput = true;
		//gameObject.GetComponent<DestroyOrDeactivateByTime> ().enabled = false;
	}
}

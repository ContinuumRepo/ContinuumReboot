using UnityEngine;
using System.Collections;

public class DestroyOrDeactivateByTime : MonoBehaviour 
{
	public float delay;
	public float timeLeft;
	public enum destroyType
	{
		Destroy,
		Activate,
		Deactivate
	}

	public destroyType DestroyType;
	//public bool deactivateOnly;
	//public bool activateOnly;
	public GameObject ActiveObject;
	public PressAnyKeyDeactivate PressAnyKeyScript;

	void Start () 
	{
		timeLeft = delay;
		/*
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
		}*/


	}

	void Update () 
	{
		// For destroying.
		if (DestroyType == destroyType.Destroy) 
		{
			if (timeLeft > 0) 
			{
				timeLeft -= Time.unscaledDeltaTime;
			}

			if (timeLeft <= 0) 
			{
				timeLeft = delay;
				Destroy (gameObject);
			}
		}

		// For Deactivating things.
		if (DestroyType == destroyType.Deactivate) 
		{
			if (timeLeft > 0) 
			{
				timeLeft -= Time.unscaledDeltaTime;
			}

			if (timeLeft <= 0) 
			{
				timeLeft = delay;
				GetComponent<DestroyOrDeactivateByTime> ().enabled = false;
				gameObject.SetActive (false);
			}
		}

		// For activation.
		if (DestroyType == destroyType.Activate) 
		{
			if (timeLeft > 0)
			{
				timeLeft -= Time.unscaledDeltaTime;
			}

			if (timeLeft < 0) 
			{
				timeLeft = delay;
				ActiveObject.SetActive (true);
				PressAnyKeyScript.useInput = true;
				GetComponent<DestroyOrDeactivateByTime> ().enabled = false;
				gameObject.SetActive (false);
			}
		}
			
	
	}

	/*
	IEnumerator Deactivate ()
	{
		yield return new WaitForSeconds (delay);
		gameObject.SetActive (false);
		GetComponent<DestroyOrDeactivateByTime> ().enabled = false;
		StopCoroutine (Deactivate ());
	}


	IEnumerator Activate ()
	{
		yield return new WaitForSeconds (delay);
		ActiveObject.SetActive (true);
		PressAnyKeyScript.useInput = true;
		//gameObject.GetComponent<DestroyOrDeactivateByTime> ().enabled = false;
	
	}
	*/
}

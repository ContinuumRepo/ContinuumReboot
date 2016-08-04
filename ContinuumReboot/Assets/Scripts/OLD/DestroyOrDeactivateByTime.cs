using UnityEngine;
using System.Collections;

public class DestroyOrDeactivateByTime : MonoBehaviour 
{
	public float delay;
	public bool deactivateOnly;

	void Start () 
	{
		if (deactivateOnly == true) 
		{
			StartCoroutine (Deactivate ());
		}

		if (deactivateOnly == false) 
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
}

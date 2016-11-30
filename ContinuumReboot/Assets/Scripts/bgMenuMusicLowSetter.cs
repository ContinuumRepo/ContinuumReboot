using UnityEngine;
using System.Collections;

public class bgMenuMusicLowSetter : MonoBehaviour
{
	void Start ()
	{
		StartCoroutine (DisableLowPassFilter ());
	}

	IEnumerator DisableLowPassFilter ()
	{
		yield return new WaitForSeconds (0.05f);
		GetComponent<AudioLowPassFilter> ().enabled = false;	
	}
}

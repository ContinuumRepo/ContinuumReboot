using UnityEngine;
using System.Collections;

public class DecreaseAudioVolumeOverTime : MonoBehaviour 
{
	public float seconds;

	void Start () 
	{
	
	}

	void Update () 
	{
		GetComponent<AudioSource> ().volume -= Time.unscaledDeltaTime / seconds;
	}
}

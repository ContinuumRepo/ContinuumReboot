using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

/* Instructions: 

1. Attach script to gameObject with an audio source
2. Set values

 */

public class AudioSourcePitchByTimescale : MonoBehaviour 
{

	public float startingPitch = 1.0f;
	public float multiplierPitch = 1.0f;
	public float minimumPitch = 0.0001f;
	public float maximumPitch = 20.0f;
	public float addPitch;
	public bool reachedMinPitch;
	public bool reachedMaxPitch;
	private AudioSource Audio;
	public bool dontUseStartPitch;
	
	void Start () 
	{
		Audio = GetComponent<AudioSource> ();
		//startingPitch = 1 // Time.timeScale; // Sets starting pitch based on Time.timeScale
		if (dontUseStartPitch == false) 
		{
			Audio.pitch = startingPitch; // Gives value to audio pitch
		}

		reachedMaxPitch = false;
	}
	

	void Update () 
	{
		// Makes pitch of the audio source with a multiplier
		if (Audio.pitch > minimumPitch) 
		{
			Audio.pitch = (Time.timeScale * multiplierPitch * startingPitch) + addPitch;
		}

		// Gives minimum pitch to audio source
		if (Audio.pitch <= minimumPitch && reachedMinPitch == false) 
		{
			Audio.pitch = (Time.timeScale * multiplierPitch * startingPitch) + addPitch;
			Debug.LogWarning ("Audio pitch is below 0!");
			reachedMinPitch = true;
		}

		if (Audio.pitch <= minimumPitch && reachedMinPitch == true) 
		{
			Audio.pitch = (Time.timeScale * multiplierPitch * startingPitch) + addPitch;
			//Debug.LogWarning ("Audio pitch is below 0!");
			//reachedMinPitch = true;
		}

		Audio.pitch = Mathf.Clamp (Audio.pitch, minimumPitch, maximumPitch);

		// Gives maximum pitch to audio source before reaching it
		if (Audio.pitch > maximumPitch)
		{
			if (reachedMaxPitch == false)
			{
				Audio.pitch = maximumPitch;
				Debug.Log ("Reached maximum audio pitch");
				reachedMaxPitch = true;
			}

			Audio.pitch = maximumPitch;
		}
	}
}

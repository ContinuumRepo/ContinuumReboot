using UnityEngine;
using System.Collections;
//using UnityStandardAssets.CinematicEffects;
using UnityStandardAssets.ImageEffects;

public class BloomIntensityByVolume : MonoBehaviour 
{
	public AudioSource Audio;
	public Bloom bloomScript;
	public Vector2 Limits;
	public float offset;

	public enum method 
	{
		Normal, 
		SideChained
	}

	public method Method;

	void Start () 
	{
		bloomScript = GetComponent <Bloom> ();
	}

	void Update ()
	{
		if (Method == method.Normal) 
		{
			//bloomScript.settings.intensity
			bloomScript.bloomIntensity = Mathf.Clamp (Audio.GetComponent<AudioSourceLoudnessTester> ().clipLoudness, Limits.x, Limits.y) + offset;
		}

		if (Method == method.SideChained) 
		{
			//bloomScript.settings.intensity
			if (bloomScript.bloomIntensity > offset) 
			{
				bloomScript.bloomIntensity -= Time.deltaTime;
			}

			if (Audio.GetComponent<AudioSourceLoudnessTester> ().clipLoudness > Limits.x) 
			{
				//bloomScript.settings.intensity
				bloomScript.bloomIntensity = Limits.y + offset;
			}
		}
	}
}

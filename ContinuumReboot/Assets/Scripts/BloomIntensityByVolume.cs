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
	public float CurrentIntensity;
	public float lerpIntensity;
	public float lerpThreshold = 0.4f;
	public float lerpMin = 0;
	public float lerpMax = 1;
	public float lerpSpeed = 2.0f;

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
		lerpIntensity = Mathf.Clamp (Audio.GetComponent<AudioSourceLoudnessTester> ().clipLoudness, Limits.x, Limits.y) + offset;

		if (Method == method.Normal) 
		{
			//bloomScript.settings.intensity
			bloomScript.bloomIntensity = Mathf.Lerp (bloomScript.bloomIntensity, lerpIntensity, lerpSpeed * Time.deltaTime);
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

		if (lerpIntensity > lerpThreshold)
		{
			lerpIntensity = lerpMax;
		}

		if (CurrentIntensity <= lerpThreshold)
		{
			lerpIntensity = lerpMin;
		}

	}
}

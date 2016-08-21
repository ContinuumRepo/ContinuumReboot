using UnityEngine;
using System.Collections;

public class FadeFilterAudio : MonoBehaviour 
{
	public AudioLowPassFilter LowPassFilter;
	public float filterSpeed = 10.0f;

	void Start () 
	{
		LowPassFilter.enabled = true;
	}
	
	void Update () 
	{
		LowPassFilter.cutoffFrequency += filterSpeed * Time.deltaTime;
		if (LowPassFilter.cutoffFrequency > 20000) 
		{
			GetComponent<FadeFilterAudio> ().enabled = false;
		}
	
	}
}

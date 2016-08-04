using UnityEngine;
using System.Collections;

public class TimeScaleByVolume : MonoBehaviour

{

	public float amp;
	public float[] smooth = new float[2];

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		Time.timeScale = GetComponent<AudioSource>().volume * amp / 3; 
	
	}

	void OnAudioFilterRead (float[] data, int channels)
	{		
		for (var i = 0; i < data.Length; i = i + channels) {
			// the absolute value of every sample
			float absInput = Mathf.Abs(data[i]);
			// smoothening filter doing its thing
			smooth[0] = ((1.0f * absInput) + (0.99f * smooth[1]));
			// exaggerating the amplitude
			amp = smooth[0]*2.0f - 1.2f;
			// it is a recursive filter, so it is doing its recursive thing
			smooth[0] = smooth[1];
		}
	}
}


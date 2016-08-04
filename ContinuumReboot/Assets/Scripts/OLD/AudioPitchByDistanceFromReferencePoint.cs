using UnityEngine;
using System.Collections;

public class AudioPitchByDistanceFromReferencePoint : MonoBehaviour 
{
	public Transform Player;
	public Transform ReferencePoint;
	public TimescaleCalculator TSCalcScript;
	private AudioSource BackgroundMusic;
	public float Dampen;
	public float Modifier;
	private GameController GameControllerScript;
	private bool noGCS;
	public float minPitch, maxPitch;
	
	void Start () 
	{
		BackgroundMusic = GetComponent <AudioSource>();
	}

	void Update () 
	{
		if (TSCalcScript.TimeScaleNow < minPitch) 
		{
			BackgroundMusic.pitch = minPitch;
		}

		if (TSCalcScript.TimeScaleNow > maxPitch) 
		{
			BackgroundMusic.pitch = maxPitch;
		}

			GameObject TSCalc = GameObject.FindGameObjectWithTag ("TSCalc");
			GameObject PlayerObject = GameObject.FindGameObjectWithTag ("Player");

		if (PlayerObject == null) 
		{
			Debug.Log ("No player object found.");
		}

		if (PlayerObject != null) 
		{
			Player = PlayerObject.transform;
			ReferencePoint = TSCalc.transform;
			//BackgroundMusic.pitch = (Vector3.Distance (PlayerObject.transform.position, ReferencePoint.transform.position) / Dampen) - Modifier;

			if (TSCalcScript.TimeScaleNow < maxPitch)
			{
				BackgroundMusic.pitch = TSCalcScript.TimeScaleNow;
			}
		}
	}
}

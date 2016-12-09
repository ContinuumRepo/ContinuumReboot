using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;

public class TimescaleController : MonoBehaviour 
{
	
	public calcMode CalculationMode;
	public enum calcMode
	{
		timeScale,							// Calculates music pitch by Time.timeScale directly
		none								// Does nothing to the music pitch.
	}

	public enum mode {onePlayer}
	public mode PlayerMode;

	[Header ("Time settings")]
	// Time settings.
	public float timeScaleReadOnly;			// The actual value of Time.timeScale.
	public float startTimeScale = 1.0f; 	// So there is no runtime error that timescale is below 0 at the start of the game.
	public float timeSpeedIncreaseSens;		// Multipler to minimum time increase per frame.
	public float addMinTime = 0.1f; 		// Minimum time scale.
	public float timeSpeedSens; 			// Dampens change in timescale as the distance between player and reference point increase.
	private float distance;					// y-distance from Reference point to player.
	private float currentTimeScale; 		// Stores calculation of timescale here.
	private Transform referencePoint;		// Needs this to calculate.
	public Text MultiplierText;				// Multipler text UI in %.

	[Header ("Music layers")]
	// The layered soundtracks to add in inspector.
	public AudioSource BassDrums;
	public AudioSource Synth1;
	public AudioSource Synth2;
	public AudioSource Synth3;
	public AudioSource Riff;

	[Header ("Volume interpolation")]
	// Linear interpolating volume.
	public float lerpBassVol;
	public float lerpSynth1Vol;
	public float lerpSynth2Vol;
	public float lerpSynth3Vol;
	public float lerpRiffVol;

	[Header ("Pitch interpolation")]
	// Linear interpolating pitch.
	public float lerpBassPitch;
	public float lerpSynth1Pitch;
	public float lerpSynth2Pitch;
	public float lerpSynth3Pitch;
	public float lerpRiffPitch;

	[Header ("MISC")]
	// MISC.
	public MeshRenderer PlayerMat; 			// The player material.
	private Transform playerOne;			// Player one transform.

	[Header ("Volume tweak values")]
	[Range (0, 1f)]
	public float lowVolSetting = 0.0f;
	[Range (0, 1)]
	public float mediumVolSetting = 0.5f;
	[Range (0, 1)]
	public float highVolSetting = 1.0f;

	[Header ("Pitch tweak values")]
	public float floorPitchSetting = 0.1f;
	public float lowPitchSetting = 0.4f;
	public float mediumPitchSetting = 1.0f;
	public float medhighPitchSetting = 1.1f;
	public float highPitchSetting = 1.25f;
	public float roofPitchSetting = 1.5f;

	[Header ("Time.timeScale checkpoints")]
	public float time0Layers = 0.5f;
	public float time1Layers = 1.0f;
	public float time2Layers = 2.0f;
	public float time3Layers = 3.0f;
	public float timeRiffLayers = 4.0f;

	public void Start () 
	{
		Time.timeScale = startTimeScale; 	// Sets start Time.timeScale.
		CalculationMode = calcMode.none;	// Starts music pitch mode on none.

		// Finds reference point gameObject.transform.
		referencePoint = GameObject.FindGameObjectWithTag ("ReferencePoint").transform; 

		if (PlayerMode == mode.onePlayer)
		{
			// Finds player transform.
			playerOne = GameObject.Find ("Player").transform; 
		}
	}
		
	public void Update ()
	{	
		AssignVolAndPitch ();
		RefreshTimeScale ();

		if (PlayerMode == mode.onePlayer) 
		{
			CalculateDistance ();
		}

		if (Time.timeScale <= Time.unscaledDeltaTime) 
		{
			Time.timeScale = 0.001f;
		}
			
		// This mode occurs when the player GameObject is hit and recovering.
		if (CalculationMode == calcMode.none) 
		{
			if (Time.timeScale < 1) 
			{
				Time.timeScale += 0.2f * Time.unscaledDeltaTime;
				BassDrums.pitch = Time.timeScale;
			}

			if (Time.timeScale >= 1) 
			{
				CalculationMode = calcMode.timeScale;
			}
		}
			
		// Calculates pitch and volume of each layer of soundtrack.
		if (CalculationMode == calcMode.timeScale) 
		{
			if (Time.timeScale < time0Layers) 
			{
				lerpBassPitch = floorPitchSetting;

				lerpBassVol = mediumVolSetting;
				lerpSynth1Vol = lowVolSetting;
				lerpSynth2Vol = lowVolSetting;
				lerpSynth3Vol = lowVolSetting;
				lerpRiffVol = lowVolSetting;
			}

			if (Time.timeScale >= time0Layers && Time.timeScale < time1Layers) 
			{
				lerpBassPitch = lowPitchSetting;

				lerpBassVol = highVolSetting;
				lerpSynth1Vol = mediumVolSetting;
				lerpSynth2Vol = lowVolSetting;
				lerpSynth3Vol = lowVolSetting;
				lerpRiffVol = lowVolSetting;
			}

			if (Time.timeScale >= time1Layers && Time.timeScale < time2Layers) 
			{
				lerpBassPitch = mediumPitchSetting;

				lerpBassVol = highVolSetting;
				lerpSynth1Vol = highVolSetting;
				lerpSynth2Vol = mediumVolSetting;
				lerpSynth3Vol = lowVolSetting;
				lerpRiffVol = lowVolSetting;
			}

			if (Time.timeScale >= time1Layers && Time.timeScale < time3Layers) 
			{
				lerpBassPitch = medhighPitchSetting;

				lerpBassVol = highVolSetting;
				lerpSynth1Vol = highVolSetting;
				lerpSynth2Vol = highVolSetting;
				lerpSynth3Vol = mediumVolSetting;
				lerpRiffVol = lowVolSetting;
			}

			if (Time.timeScale >= time3Layers && Time.timeScale < timeRiffLayers) 
			{
				lerpBassPitch = highPitchSetting;

				lerpBassVol = highVolSetting;
				lerpSynth1Vol = highVolSetting;
				lerpSynth2Vol = highVolSetting;
				lerpSynth3Vol = highVolSetting;
				lerpRiffVol = mediumVolSetting;
			}

			if (Time.timeScale >= timeRiffLayers) 
			{
				lerpBassPitch = roofPitchSetting;

				lerpBassVol = highVolSetting;
				lerpSynth1Vol = highVolSetting;
				lerpSynth2Vol = highVolSetting;
				lerpSynth3Vol = highVolSetting;
				lerpRiffVol = highVolSetting;
			}
		}
	}

	void CalculateDistance ()
	{
		distance = playerOne.transform.position.y - referencePoint.transform.position.y;
		Time.timeScale = ((distance + currentTimeScale) * (0.01f * timeSpeedSens)) + addMinTime;
		currentTimeScale += Time.unscaledDeltaTime * (0.01f * timeSpeedIncreaseSens);
	}

	void RefreshTimeScale ()
	{
		// See actual time.TimeScale in inspector so you dont have to always check in 
		// edit -> 
		//		  Project Settings 
		//						  -> Time.
		timeScaleReadOnly = Time.timeScale;
	}

	void AssignVolAndPitch ()
	{
		// Volume changes on soundtracks separately.
		BassDrums.volume = Mathf.Lerp (BassDrums.volume, lerpBassVol, 2 * Time.unscaledDeltaTime);
		Synth1.volume = Mathf.Lerp (Synth1.volume, lerpSynth1Vol, 2 * Time.unscaledDeltaTime);
		Synth2.volume = Mathf.Lerp (Synth2.volume, lerpSynth2Vol, 2 * Time.unscaledDeltaTime);
		Synth3.volume = Mathf.Lerp (Synth3.volume, lerpSynth3Vol, 2 * Time.unscaledDeltaTime);
		Riff.volume = Mathf.Lerp (Riff.volume, lerpRiffVol, 2 * Time.unscaledDeltaTime);

		// Only have to change pitch on the bass, the other synth soundtracks will follow if 
		// they have the PitchSync script attached.
		BassDrums.pitch = Mathf.Lerp (BassDrums.pitch, lerpBassPitch, 2 * Time.unscaledDeltaTime);
	}
}
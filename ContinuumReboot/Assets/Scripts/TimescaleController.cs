using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;

public class TimescaleController : MonoBehaviour 
{
	public float timeScaleReadOnly;			// The actual value of Time.timeScale.

	public enum calcMode
	{
		Distance, 							// Calculates music pitch by via distance
		Continuous,
		timeScale,							// Calculates music pitch by Time.timeScale directly
		none								// Does nothing to the music pitch.
	}

	public calcMode CalculationMode;

	public enum mode
	{
		onePlayer, 
		//twoPlayers, 
		//threePlayers, 
		//fourPlayers
	}

	public mode PlayerMode;

	public float highestTimeScale; 			// For stats screen.

	public float startTimeScale = 1.0f; 	// So there is no runtime error that timescale is below 0 at the start of the game.
	public float timeSpeedIncreaseSens;		// Multipler to minimum time increase per frame.
	public float addMinTime = 0.1f; 		// Minimum time scale.
	public float timeSpeedSens; 			// Dampens change in timescale as the distance between player and reference point increase.
	public float distance;					// y-distance from Reference point to player.
	public Text MultiplierText;				// Multipler text UI in %.
	private float currentTimeScale; 		// Stores calculation of timescale here.
	private Transform referencePoint;		// Needs this to calculate.
	public AudioSource Music;				// The main game music.

	public AudioSource BassDrums;
	public AudioSource Synth1;
	public AudioSource Synth2;
	public AudioSource Synth3;
	public AudioSource Riff;

	public float lerpBassVol;
	public float lerpSynth1Vol;
	public float lerpSynth2Vol;
	public float lerpSynth3Vol;
	public float lerpRiffVol;

	public float lerpBassPitch;
	public float lerpSynth1Pitch;
	public float lerpSynth2Pitch;
	public float lerpSynth3Pitch;
	public float lerpRiffPitch;

	public ParticleSystem DistantStars;		// The star field particles.
	public MeshRenderer PlayerMat; 			// The player material.

	private Transform playerOne;			// Player one transform.

	public void Start () 
	{
		Time.timeScale = startTimeScale; // Sets start Time.timeScale.
		CalculationMode = calcMode.none; // Starts music pitch mode on none.

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
		BassDrums.volume = Mathf.Lerp (BassDrums.volume, lerpBassVol, 2 * Time.unscaledDeltaTime);
		Synth1.volume = Mathf.Lerp (Synth1.volume, lerpSynth1Vol, 2 * Time.unscaledDeltaTime);
		Synth2.volume = Mathf.Lerp (Synth2.volume, lerpSynth2Vol, 2 * Time.unscaledDeltaTime);
		Synth3.volume = Mathf.Lerp (Synth3.volume, lerpSynth3Vol, 2 * Time.unscaledDeltaTime);
		Riff.volume = Mathf.Lerp (Riff.volume, lerpRiffVol, 2 * Time.unscaledDeltaTime);

		BassDrums.pitch = Mathf.Lerp (BassDrums.volume, lerpBassPitch, 2 * Time.unscaledDeltaTime);

		//Music.pitch = Mathf.Lerp (Music.pitch, lerpPitch, 2 * Time.unscaledDeltaTime);
		Time.timeScale = Mathf.Clamp (Time.timeScale, 0.05f, 10.0f);

		if (Time.timeScale <= 0) 
		{
			Time.timeScale = 0.001f;
		}
			
		if (CalculationMode == calcMode.none) 
		{
			if (Time.timeScale < 1) 
			{
				Time.timeScale += 0.2f * Time.unscaledDeltaTime;
				Music.pitch = Time.timeScale;
			}

			if (Time.timeScale >= 1) 
			{
				CalculationMode = calcMode.timeScale;
			}
		}

		if (CalculationMode == calcMode.Continuous) 
		{
			distance = playerOne.transform.position.y - referencePoint.transform.position.y;
			Time.timeScale = distance;
			Music.pitch = Time.timeScale;

			// Stores the highest timescale value for stats.
			if (Time.timeScale > highestTimeScale) {
				highestTimeScale = Time.timeScale;
			}

			if (Time.timeScale < 0.5f) 
			{
				//lerpPitch = 0.15f;
			}

			if (Time.timeScale >= 0.5f && Time.timeScale < 1f) 
			{
				//lerpPitch = 0.4f;
			}

			if (Time.timeScale >= 1f && Time.timeScale < 2f) 
			{
				//lerpPitch = 1f;
			}

			if (Time.timeScale >= 2f && Time.timeScale < 3f) 
			{
				//lerpPitch = 1.25f;
			}

			if (Time.timeScale >= 3f && Time.timeScale < 4f) 
			{
				//lerpPitch = 1.5f;
			}

			if (Time.timeScale >= 4f) 
			{
				//lerpPitch = 2f;
			}
		}

		// See actual time.TimeScale in inspector so you dont have to always check in edit > Project Settings > Time.
		timeScaleReadOnly = Time.timeScale;

		if (PlayerMode == mode.onePlayer) 
		{
			distance = playerOne.transform.position.y - referencePoint.transform.position.y;
			Time.timeScale = (distance * timeSpeedSens) + addMinTime;
		}
			
		if (CalculationMode == calcMode.timeScale) 
		{
			// Stores the highest timescale value for stats.
			if (Time.timeScale > highestTimeScale)
			{
				highestTimeScale = Time.timeScale;
			}

			if (Time.timeScale < 0.5f) 
			{
				lerpBassPitch = 0.1f;

				lerpBassVol = 0.5f;
				lerpSynth1Vol = 0.0f;
				lerpSynth2Vol = 0.0f;
				lerpSynth3Vol = 0.0f;
				lerpRiffVol = 0.0f;
			}

			if (Time.timeScale >= 0.5f && Time.timeScale < 1f) 
			{
				lerpBassPitch = 0.4f;

				lerpBassVol = 1.0f;
				lerpSynth1Vol = 0.5f;
				lerpSynth2Vol = 0.0f;
				lerpSynth3Vol = 0.0f;
				lerpRiffVol = 0.0f;
			}

			if (Time.timeScale >= 1f && Time.timeScale < 2f) 
			{
				lerpBassPitch = 1f;

				lerpBassVol = 1.0f;
				lerpSynth1Vol = 1f;
				lerpSynth2Vol = 0.5f;
				lerpSynth3Vol = 0.0f;
				lerpRiffVol = 0.01f;
			}

			if (Time.timeScale >= 2f && Time.timeScale < 3f) 
			{
				lerpBassPitch = 1.1f;

				lerpBassVol = 1.0f;
				lerpSynth1Vol = 1f;
				lerpSynth2Vol = 1f;
				lerpSynth3Vol = 0.5f;
				lerpRiffVol = 0.0f;
			}

			if (Time.timeScale >= 3f && Time.timeScale < 4f) 
			{
				lerpBassPitch = 1.25f;

				lerpBassVol = 1.0f;
				lerpSynth1Vol = 1f;
				lerpSynth2Vol = 1f;
				lerpSynth3Vol = 1f;
				lerpRiffVol = 0.5f;
			}

			if (Time.timeScale >= 4f) 
			{
				lerpBassPitch = 1.5f;

				lerpBassVol = 1.0f;
				lerpSynth1Vol = 1.0f;
				lerpSynth2Vol = 1.0f;
				lerpSynth3Vol = 1.0f;
				lerpRiffVol = 1.0f;
			}
		}

		if (CalculationMode == calcMode.Distance) 
		{
			if (distance > 0)
			{
				Time.timeScale = ((distance + currentTimeScale) * timeSpeedSens) + addMinTime; // Stores values into time.TimeScale.
				currentTimeScale += Time.unscaledDeltaTime * timeSpeedIncreaseSens; // Increases minimum timescale.
			}

			//Music.pitch = Time.timeScale;
			if (distance < 7) 
			{
				//lerpPitch = 0.25f;
			}

			if (distance < 14 && distance >= 7) 
			{
				if (Time.timeScale < 1 || Music.pitch < 1)
				{
					//lerpPitch = 0.5f;
				}

				if (Time.timeScale >= 1 || Music.pitch > 1) 
				{
					//lerpPitch = 1;
				}
			}

			if (distance >= 14 && distance < 21) 
			{
				//lerpPitch = 1f;
			}

			if (distance >= 21 && distance < 28) 
			{
				//lerpPitch = 1.25f;
			}

			if (distance >= 28 && distance < 35) 
			{
				//lerpPitch = 1.5f;
			}

			if (distance >= 35) 
			{
				//lerpPitch = 1.75f;
			}
		}
	}
}
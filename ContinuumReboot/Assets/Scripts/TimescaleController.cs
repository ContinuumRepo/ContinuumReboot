using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;

public class TimescaleController : MonoBehaviour 
{
	public float highestTimeScale; 			// For stats screen.
	public float timeScaleReadOnly;			// The actual value of time.timeScale.
	public float startTimeScale = 1.0f; 	// So there is no runtime error that timescale is below 0 at the start of the game.
	public float timeSpeedIncreaseSens;		// Multipler to minimum time increase per frame.
	public float addMinTime = 0.1f; 		// Minimum time scale.
	public float timeSpeedSens; 			// Dampens change in timescale as the distance between player and reference point increase.
	public float distance;					// y-distance from Reference point to player.
	public Text MultiplierText;				// Multipler text UI in %.
	private float currentTimeScale; 		// Stores calculation of timescale here.
	private Transform referencePoint;		// Needs this to calculate.
	public AudioSource Music;				// The main game music.

	public enum calcMode
	{
		Distance, 							// Calculates music pitch by via distance
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
	public ParticleSystem DistantStars;		// The star field particles.
	public MeshRenderer PlayerMat; 			// The player material.


	private Transform playerOne;		// Player one transform.
		
	/*
	private Transform playerTwo;		// Player two transform.
	private Transform playerThree;		// Player three transform.
	private Transform playerFour;		// Player four transform.
	*/

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

		/*
		if (PlayerMode == mode.twoPlayers) 
		{
			playerOne = GameObject.Find ("Player").transform; 
			playerTwo = GameObject.Find ("PlayerTwo").transform;
		}

		if (PlayerMode == mode.threePlayers) 
		{
			playerOne = GameObject.Find ("Player").transform; 
			playerTwo = GameObject.Find ("PlayerTwo").transform;
			playerThree = GameObject.Find ("PlayerThree").transform;
		}

		if (PlayerMode == mode.fourPlayers) 
		{
			playerOne = GameObject.Find ("Player").transform; 
			playerTwo = GameObject.Find ("PlayerTwo").transform;
			playerThree = GameObject.Find ("PlayerThree").transform;
			playerFour = GameObject.Find ("PlayerFour").transform;
		}*/
	}

	public void Update () 
	{	
		if (Time.timeScale <= 0) 
		{
			Time.timeScale = Mathf.Clamp (Time.timeScale, 0.05f, 10.0f);
			//Time.timeScale = 0.05f;
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
				CalculationMode = calcMode.Distance;
			}
		}

		MultiplierText.color = new Color ((43 - distance) / 43, (43 - distance) / 20, distance / 43);

		DistantStars.GetComponent<ParticleSystemRenderer> ().velocityScale = Time.timeScale / 8;
		timeScaleReadOnly = Time.timeScale; // See actual time.TimeScale in inspector so you dont have to always check in edit > Project Settings > Time.

		if (PlayerMode == mode.onePlayer) 
		{
			distance = playerOne.transform.position.y - referencePoint.transform.position.y;
		}

		/*
		if (PlayerMode == mode.twoPlayers)
		{
			distance = ((playerOne.transform.position.y + playerTwo.transform.position.y) / 2) - referencePoint.transform.position.y; // Calculates average distance y the two players. distance.
		}

		if (PlayerMode == mode.threePlayers)
		{
			distance = ((playerOne.transform.position.y + playerTwo.transform.position.y + playerThree.transform.position.y) / 3) - referencePoint.transform.position.y; // Calculates average distance y the two players. distance.
		}

		if (PlayerMode == mode.fourPlayers)
		{
			distance = ((playerOne.transform.position.y + playerTwo.transform.position.y + playerThree.transform.position.y + playerFour.transform.position.y) / 4) - referencePoint.transform.position.y; // Calculates average distance y the two players. distance.
		}*/

		if (CalculationMode == calcMode.Distance) 
		{
			Time.timeScale = ((distance + currentTimeScale) * timeSpeedSens) + addMinTime; // Stores values into time.TimeScale.
			currentTimeScale += Time.unscaledDeltaTime * timeSpeedIncreaseSens; // Increases minimum timescale.

			if (distance < 7) 
			{
				Music.pitch = 0.2f;
			}

			if (distance < 14 && distance >= 7) 
			{
				if (Time.timeScale < 1 || Music.pitch < 1)
				{
					Music.pitch = 0.5f;
				}

				if (Time.timeScale >= 1 || Music.pitch > 1) 
				{
					Music.pitch = 1;
				}
			}

			if (distance >= 14 && distance < 24) 
			{
				Music.pitch = 1f;
			}

			if (distance >= 24 && distance < 34) 
			{
				Music.pitch = 1.25f;
			}

			if (distance >= 34 && distance < 44) 
			{
				Music.pitch = 1.5f;
			}

			if (distance >= 44) 
			{
				Music.pitch = 2f;
			}
		}

		if (CalculationMode == calcMode.timeScale) 
		{
			// Stores the highest timescale value for stats.
			if (Time.timeScale > highestTimeScale) {
				highestTimeScale = Time.timeScale;
			}

			if (Time.timeScale < 0.5f) 
			{
				Music.pitch = 0.15f;
			}

			if (Time.timeScale >= 0.5f && Time.timeScale < 1f) {
				Music.pitch = 0.4f;
			}

			if (Time.timeScale >= 1f && Time.timeScale < 2f) {
				Music.pitch = 1f;
			}

			if (Time.timeScale >= 2f && Time.timeScale < 3f) {
				Music.pitch = 1.25f;
			}

			if (Time.timeScale >= 3f && Time.timeScale < 4f) {
				Music.pitch = 1.5f;
			}

			if (Time.timeScale >= 4f) 
			{
				Music.pitch = 2f;
			}
		}
	}
}
using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class TimescaleController : MonoBehaviour 
{
	public float highestTimeScale; // For stats screen.
	public float timeScaleReadOnly; // The actual value of time.timeScale.
	public float startTimeScale = 1.0f; // So there is no runtime error that timescale is below 0 at the start of the game.
	public float timeSpeedIncreaseSens; // Multipler to minimum time increase per frame.
	public float addMinTime = 0.1f; // Minimum time scale.
	public float timeSpeedSens; // Dampens change in timescale as the distance between player and reference point increase.
	public float distance; // y-distance from Reference point to player.

	private float currentTimeScale; // Stores calculation of timescale here.
	private Transform playerOne;
	private Transform playerTwo;
	private Transform playerThree;
	private Transform playerFour;
	private Transform referencePoint;
	public AudioSource Music;

	public enum calcMode
	{
		Distance,
		timeScale
	}

	public calcMode CalculationMode;

	public enum mode
	{
		onePlayer, twoPlayers, threePlayers, fourPlayers
	}

	public mode PlayerMode;

	public ParticleSystem DistantStars;
	public VignetteAndChromaticAberration vignetteScript;
	public MeshRenderer PlayerMat;

	public void Start () 
	{
		Time.timeScale = startTimeScale; // Sets timescale to 1

		if (PlayerMode == mode.onePlayer)
		{
			playerOne = GameObject.Find ("Player").transform; 
		}

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
		}

		referencePoint = GameObject.FindGameObjectWithTag ("ReferencePoint").transform; // Finds reference point gameObject.transform.
	}

	public void Update () 
	{
		DistantStars.GetComponent<ParticleSystemRenderer> ().velocityScale = Time.timeScale / 8;
		vignetteScript.intensity = 0.3f - (Time.timeScale / 10);
		timeScaleReadOnly = Time.timeScale; // See actual time.TimeScale in inspector so you dont have to always check in edit > Project Settings > Time.
		Time.timeScale = ((distance + currentTimeScale) * timeSpeedSens) + addMinTime; // Stores values into time.TimeScale.
		currentTimeScale += Time.unscaledDeltaTime * timeSpeedIncreaseSens; // Increases minimum timescale.

		if (PlayerMode == mode.onePlayer) 
		{
			distance = playerOne.transform.position.y - referencePoint.transform.position.y;
		}

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
		}

		if (CalculationMode == calcMode.timeScale) {

			// Stores the highest timescale value for stats.
			if (Time.timeScale > highestTimeScale) {
				highestTimeScale = Time.timeScale;
			}

			if (Time.timeScale < 0.5f) {
				Music.pitch = 0.25f;
			}

			if (Time.timeScale >= 1f && Time.timeScale < 1.25f) {
				Music.pitch = 0.75f;
			}

			if (Time.timeScale >= 1.25f && Time.timeScale < 1.5f) {
				Music.pitch = 1.0f;
			}

			if (Time.timeScale >= 1.5f && Time.timeScale < 1.75f) {
				Music.pitch = 1.25f;
			}

			if (Time.timeScale >= 1.75f) {
				Music.pitch = 1.5f;
			}
		}

		if (CalculationMode == calcMode.Distance) 
		{
			if (distance < 5)
			{
				//DistantStars.GetComponent<ParticleSystemRenderer> ().velocityScale = 0.0f;
				Music.pitch = 0.25f;
			}

			if (distance >= 5 && distance < 10) 
			{
				//DistantStars.GetComponent<ParticleSystemRenderer> ().velocityScale = 0.5f;
				Music.pitch = 0.5f;
			}

			if (distance >= 10 && distance < 15) 
			{
				//DistantStars.GetComponent<ParticleSystemRenderer> ().velocityScale = 1f;
				Music.pitch = 0.75f;
			}

			if (distance >= 15 && distance < 20) 
			{
				//DistantStars.GetComponent<ParticleSystemRenderer> ().velocityScale = 1.5f;
				Music.pitch = 1.0f;
			}

			if (distance >= 20 && distance < 25) 
			{
				//DistantStars.GetComponent<ParticleSystemRenderer> ().velocityScale = 2f;
				Music.pitch = 1.25f;
			}

			if (distance >= 25 && distance < 30) 
			{
				//DistantStars.GetComponent<ParticleSystemRenderer> ().velocityScale = 2.5f;
				Music.pitch = 1.5f;
			}

			if (distance >= 30) 
			{
				//DistantStars.GetComponent<ParticleSystemRenderer> ().velocityScale = 3f;
				Music.pitch = 2.0f;
			}
		}

	}
}


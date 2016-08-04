using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour 
{
	[Header ("TIME")]
	public Text TimescaleText;
	public float TimescaleValue;
	public TimescaleCalculator Tscalc;
	public GameObject debugInfoUI;

	[Header ("PRE GAME")]
	public GameObject PreGameUI;
	public bool PreGame;
	public GameObject PlayButton;

	[Header ("Pausing")]
	public float PauseTimeScale = 0.0001f;
	public bool isPaused;
	public GameObject PauseUI;

	public Transform SpawnTransformA;
	public Transform SpawnTransformB;
	public Transform SpawnTransformC;
	public Transform SpawnTransformD;

	[Header ("POINTS")]
	public GameObject[] Points;
	public int pointCount;
	public float pointStartWait;
	public float pointSpawnWait;
	public float pointWaveWait;
	public Vector3 pointSpawnValues;
	public int currentPoints;
	public Text currentPointsText;

	[Header ("ENEMIES")]
	public GameObject[] Enemies;
	public int enemyCount;
	public float enemyStartWait;
	public float enemySpawnWait;
	public float enemyWaveWait;
	public Vector3 enemySpawnValues;
	public int enemyKills;
	public Text EnemyKillsText;
	public float maxEnemySpawnRate = 0.15f;
	
	[Header ("LEVELMODS")]
	public GameObject[] LevelMods;
	public int levelModCount;
	public float levelModStartWait;
	public float levelModSpawnWait;
	public float levelModWaveWait;
	public Vector3 levelModSpawnValues;
	public int currentlevelModPoints;
	public Text currentLevelModPointsText;

	[Header ("POWERUPS")]
	public GameObject[] Powerups;
	public int powerupCount;
	public float powerupStartWait;
	public float powerupSpawnWait;
	public float powerupWaveWait;
	public Vector3 powerupSpawnValues;

	[Header ("LIVES")]
	public int lives = 3;
	public int startingLives = 3;
	public Text LivesText;
	public GameObject Player;
	public GameObject PlayerExplosion;
	public GameObject LoadedPlayer;
	public GameObject RespawnPlayerObject;
	public float invincibilityTime = 3.0f;
	public GameObject InvincibilityLoadingParticles;
	public float delayTime = 3.0f;
	private BasicPlayerMovement PlayerControllerScript;

	[Header ("Game Over")]
	public bool isGameOver;
	public GameObject GameOverPanel;
	public float decreaseTimeSpeed = 1.0f;
	public Button ReplayButton;
	public Text SavingScoreText;
	private AudioSourcePitchByTimescale MainAudioSource;
	public AudioPitchByDistanceFromReferencePoint AudioScript;
	[Header ("_____________________")]
	public Text GOPointsText;
	public Text GOLMText;
	public Text GOEnemyKillsText;
	public Text TotalPointsText;
	public Text TotalLevelModsText;

	[Header ("SCORING")]
	public int currentScore;
	public Text ScoreTextMobile;
	public float Addscore;
	public ParticleSystem MainEngine;
	public Text EndScoreText;
	public int HighScore;
	public Text HighScoreText;
	public GameObject CelebrationEffect;
	public bool savedStats = false; 
	public float metres;
	public Text LengthText;
	
	void Start () 
	{
		StartCoroutine (PointSpawnWaves ());
		StartCoroutine (LevelModSpawnWaves ());
		StartCoroutine (EnemySpawnWaves ());
		StartCoroutine (PowerupWaves ());
		debugInfoUI.SetActive (false);
		lives = startingLives;
		isGameOver = false;
		GameOverPanel.SetActive (false);
		PreGame = true;
		HighScoreText.text = "" + (PlayerPrefs.GetInt ("High Score")) + "";

		GameObject MainAudioSourceObject = GameObject.FindGameObjectWithTag ("MainAudioSource");
		MainAudioSource = MainAudioSourceObject.GetComponent<AudioSourcePitchByTimescale>();
		MainAudioSource.enabled = false;

		GameObject PlayerControllerObject = GameObject.FindGameObjectWithTag ("Player");
		
		if (PlayerControllerScript == null || PlayerControllerObject == null) 
		{
			Debug.Log ("Cannot find player script anymore.");
		}
		
		if (PlayerControllerScript != null || PlayerControllerObject != null) 
		{
			PlayerControllerScript = PlayerControllerObject.GetComponent<BasicPlayerMovement> ();
		}

	}

	void Update () 
	{

		// Game Over stats
		GOPointsText.text = "" + currentPoints + "";
		GOLMText.text = "" + currentlevelModPoints + "";
		GOEnemyKillsText.text = "" + enemyKills + "";
		TotalPointsText.text = "" + PlayerPrefs.GetInt ("Total Points") + "";
		TotalLevelModsText.text = "" + PlayerPrefs.GetInt ("Total LevelMods") + "";

		// Resets stats
		if (Input.GetKeyDown (KeyCode.H)) 
		{
			HighScore = PlayerPrefs.GetInt ("High Score");
			HighScore = 0;

			PlayerPrefs.SetInt ("High Score", HighScore - currentScore);
			PlayerPrefs.SetInt("Total Points", 0);
			PlayerPrefs.SetInt("Total LevelMods", 0);

			Debug.Log ("Reset stats.");
		}

		if (isPaused) 
		{
			Tscalc.levelOneTimeRemaining += Time.fixedDeltaTime;
			Time.timeScale = PauseTimeScale;
			PauseUI.SetActive (true);
		}
		/*
		if (MainEngine.emissionRate <= 300) 
		{
			MainEngine.emissionRate = 0.25f * currentPoints;
		}

		if (MainEngine.emissionRate > 300) 
		{
			MainEngine.emissionRate = 300;
		}*/

		if (PlayButton.activeSelf == true) 
		{
			PreGame = true;
			Tscalc.enabled = false;
		}

		if (PlayButton.activeSelf == false) 
		{
			PreGame = false;
			Tscalc.enabled = true;
		}

		if (PreGame == true) 
		{
			//Time.timeScale = 0.00001f;
			Tscalc.isLevelOne = false;
			Tscalc.enabled = false;
		}

		if (PreGame == false) 
		{
			metres += Time.deltaTime * 20;
			LengthText.text = "" + Mathf.RoundToInt(metres) + " m";
			// Sets TimescaleValue as the current Time.timeScale
			TimescaleValue = Time.timeScale;

			// Converts into 2 decimal places
			TimescaleText.text = "x " + string.Format ("{0:0.00}", Mathf.Round (TimescaleValue * 100f) / 100f) + "";

			// Shows kill count
			EnemyKillsText.text = "" + enemyKills + "";

			// sets UI World points
			currentPointsText.text = "" + currentPoints + "";
			currentLevelModPointsText.text = "" + currentlevelModPoints + "";

			//currentPointsText.GetComponent<ParticleSystem> ().Play ();
			LivesText.text = "" + lives;

			if (ScoreTextMobile.enabled == true && isGameOver == false) {
				ScoreTextMobile.text = "" + currentScore;
				EndScoreText.text = "";
			}

			if (Input.GetKeyDown (KeyCode.F1)) 
			{
				debugInfoUI.SetActive (true);
			}

			if (Input.GetKeyDown (KeyCode.R)) 
			{
				//Application.LoadLevel (Application.loadedLevel); (Deprecated)
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}

			if (Input.GetKeyDown (KeyCode.K) && lives > 0) 
			{
				KillPlayer ();
				Debug.Log ("You died, you now have " + lives + " lives left.");
			}

			if (enemySpawnWait < maxEnemySpawnRate)
			{
				enemySpawnWait = maxEnemySpawnRate;
			}

			if (PlayerControllerScript.currentHealth <= PlayerControllerScript.deathHealth)
			{
				KillPlayer ();
			}

			// SCORING
			currentScore += Mathf.RoundToInt (Time.timeScale * Time.deltaTime * 100.0f);

			if (currentScore < 0) 
			{
				currentScore = 1;
			}

			// Life
			if (lives == 0 && isGameOver == false) {
				Player.SetActive (false);
				lives = 0;
				isGameOver = true;
				GameOver ();
			}

			if (lives <= 0 && isGameOver == true) {
				AudioScript.enabled = false;
				Player.SetActive (false);
				ScoreTextMobile.text = "";
				EndScoreText.text = "" + currentScore;
				lives = 0;
				MainAudioSource.enabled = true;

				if (Time.timeScale > 0.01667f) {
					Time.timeScale -= Time.unscaledDeltaTime * decreaseTimeSpeed;
					ReplayButton.interactable = false;
					SavingScoreText.text = "Saving stats...";

					Tscalc.Level = 1;
					Tscalc.isLevelOne = true;
					Tscalc.isLevelTwo = false;
					Tscalc.isLevelThree = false;
					Tscalc.isLevelFour = false;
					Tscalc.isLevelFive = false;
				}

				if (Time.timeScale < 0.016667f) {
					Time.timeScale = 0.0001f;
					Tscalc.enabled = false;
					ReplayButton.interactable = true;
					SavingScoreText.text = "SAVED!";

					// Sets High Score
					if (PlayerPrefs.GetInt("High Score") < currentScore)
					{
						PlayerPrefs.SetInt("High Score", currentScore);
						CelebrationEffect.SetActive (true);
					}

					if (savedStats == false)
					{
						// Sets Total Points
						PlayerPrefs.SetInt("Total Points", PlayerPrefs.GetInt("Total Points") + currentPoints);
						// Sets Total LevelMods
						PlayerPrefs.SetInt("Total LevelMods", PlayerPrefs.GetInt("Total LevelMods") + currentlevelModPoints);

						HighScore = PlayerPrefs.GetInt("High Score");
						HighScoreText.text = "" + HighScore;
						savedStats = true;
					}



					Tscalc.Level = 1;
					Tscalc.isLevelOne = true;
					Tscalc.isLevelTwo = false;
					Tscalc.isLevelThree = false;
					Tscalc.isLevelFour = false;
					Tscalc.isLevelFive = false;
				}
			}
		}

	}

	void KillPlayer ()
	{
		StartCoroutine (RespawnPlayer ());
	}

	void GameOver ()
	{
		GameOverPanel.SetActive (true);
	}

	IEnumerator PointSpawnWaves ()
	{

			yield return new WaitForSeconds (pointStartWait);
			while (true) {
				for (int i = 0; i < pointCount; i++) {
					GameObject hazard = Points [UnityEngine.Random.Range (0, Points.Length)];

					Instantiate (hazard, SpawnTransformA.transform.position, SpawnTransformA.rotation);
					yield return new WaitForSeconds (pointSpawnWait);
					//spawnWait = spawnWait - 0.002f; // If you want to make it go faster over time.
				}

				yield return new WaitForSeconds (pointWaveWait);
			}
	}

	IEnumerator LevelModSpawnWaves ()
	{
		
		yield return new WaitForSeconds (levelModStartWait);
		while (true) {
			for (int i = 0; i < levelModCount; i++) {
				GameObject LevelMod = LevelMods [UnityEngine.Random.Range (0, LevelMods.Length)];
				
				Instantiate (LevelMod, SpawnTransformB.transform.position, SpawnTransformB.rotation);
				yield return new WaitForSeconds (levelModSpawnWait);
				//spawnWait = spawnWait - 0.002f; // If you want to make it go faster over time.
			}
			
			yield return new WaitForSeconds (levelModWaveWait);
		}
	}

	IEnumerator EnemySpawnWaves ()
	{
		yield return new WaitForSeconds (enemyStartWait);
		while (true) {
			for (int i = 0; i < enemyCount; i++) {
				GameObject enemy = Enemies [UnityEngine.Random.Range (0, Enemies.Length)];
				
				Instantiate (enemy, SpawnTransformC.transform.position, SpawnTransformC.rotation);
				yield return new WaitForSeconds (enemySpawnWait);
				enemySpawnWait = enemySpawnWait - 0.006f; // If you want to make it go faster over time.
			}
			
			yield return new WaitForSeconds (enemyWaveWait);
		}
	}

	IEnumerator PowerupWaves ()
	{
		yield return new WaitForSeconds (powerupStartWait);
		while (true) {
			for (int i = 0; i < powerupCount; i++) {
				GameObject powerup = Powerups [UnityEngine.Random.Range (0, Powerups.Length)];
				
				Instantiate (powerup, SpawnTransformD.transform.position, SpawnTransformD.rotation);
				yield return new WaitForSeconds (powerupSpawnWait);
				//spawnWait = spawnWait - 0.002f; // If you want to make it go faster over time.
			}
			
			yield return new WaitForSeconds (powerupWaveWait);
		}
	}

	IEnumerator RespawnPlayer ()
	{
		if (lives > 0 && LivesText.text != "0") 
		{
			PlayerControllerScript.currentHealth = PlayerControllerScript.startingHealth;

			// Finds Player with Player tag
			GameObject PlayerObject = GameObject.FindGameObjectWithTag ("Player");

			if (PlayerObject != null)
			{
			// Disables GameObject
			PlayerObject.SetActive (false);
			}

			// Decrements Lives
			lives -= 1;

			// Nice player explosion
			Instantiate (PlayerExplosion, PlayerObject.transform.position, PlayerObject.transform.rotation);
			Instantiate (InvincibilityLoadingParticles, PlayerObject.transform.position, PlayerObject.transform.rotation);

			// Start Death time
			yield return new WaitForSeconds (delayTime);

			// Respawn effect
			Instantiate (LoadedPlayer, PlayerObject.transform.position, PlayerObject.transform.rotation);

			// Enables Player GameObject
			if (PlayerObject != null)
			{
				PlayerObject.SetActive (true);
			}

			// Finds Player GameObject child
			GameObject PlayerObjectChild = GameObject.FindGameObjectWithTag ("PlayerGeom");

			if (PlayerObjectChild != null)
			{
			// Turns off mesh collider
			PlayerObjectChild.GetComponent<BoxCollider> ().enabled = false;
			}

			// Plays Finish Invincibility effect
			//GameObject InvincibilityEffect = GameObject.Find ("Invincibility Particles");
			//InvincibilityEffect.GetComponent<ParticleSystem> ().Play ();
			//Debug.Log ("Playing Invincibility particle effect.");

			// Invincibility Time
			yield return new WaitForSeconds (invincibilityTime);

			if (PlayerObjectChild != null)
			{
				// Turns mesh collider back on
				PlayerObjectChild.GetComponent<BoxCollider> ().enabled = true;
			}
		}

		if (lives <= 0 && LivesText.text == "0") 
		{

			// Finds Player with Player tag
			GameObject PlayerObject = GameObject.FindGameObjectWithTag ("Player");
			
			// Nice player explosion
			Instantiate (PlayerExplosion, PlayerObject.transform.position, PlayerObject.transform.rotation);

			// Disables GameObject
			PlayerObject.SetActive (false);
		}
	}
}

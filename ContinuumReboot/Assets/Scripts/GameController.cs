using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour 
{
	[Header ("Pre-game")]
	public bool isPreGame; 										// Is the game in pre-game mode?
	public GameObject PreGameUI; 								// Pre-game UI.
	public float CountDownDelay = 5.0f; 						// Countdown delay.
	public Animator PlayerAnim; 								// Player enter animation.
	public GameObject BottomBarrier; 							// The bottom barrier of the play space.
	public GameObject ControlsUI;
	public bool twoPlayerMode;

	[Header ("Spawning Objects")]
	public GameObject[] Hazards; 								// Array of different hazards to spawn.
	public Vector3 spawnValues; 								// Spawn X, Y, Z values.
	public float startWait; 									// Start spawn wait.
	public float spawnWait;										// Time between new spawned hazards.
	public float waveWait; 										// Time between new waves.
	public int hazardCount; 									// The amount of hazards to be spawned before a new wave.
	private float[] columnLocations;
	public float speedupSpawnRate = 0.005f;
	public float minSpawnWait = 0.05f;
	public Text LevelUpText;
	public int wave;
	public GameObject WaveLabel;

	[Header ("Spawning Powerups")]
	public GameObject[] Powerups; 								// Array of different powerups to spawn.
	public Vector3 powerupSpawnValues; 							// Spawn X, Y, Z values.
	public float powerupStartWait; 								// Start powerup spawn wait.
	public float powerupSpawnWait;								// Time between new spawned powerups.
	public float powerupWaveWait; 								// Time between powerup waves.
	public int powerupCount; 									// The amountof hazards to be spawned before a new wave of powerups.
	public Text PowerupText; 									// Powerup label for the player.

	[Header ("Spawning Bosses")]
	public GameObject[] Rareitems; 								// Array of different powerups to spawn.
	public Vector3 rareitemSpawnValues; 						// Spawn X, Y, Z values.
	public float rareitemStartWait; 							// Start powerup spawn wait.
	public float rareitemSpawnWait; 							// Time between new spawned powerups.
	public float rareitemWaveWait; 								// Time between powerup waves.
	public int rareitemCount; 									// The amountof hazards to be spawned before a new wave of powerups.

	[Header ("Pausing")]
	public bool isPaused; 										// Is the game paused right now?
	public GameObject PauseUI; 									// Pause UI object.
	public AudioSource PauseSound; 								// The pause sound.
	public AudioSource MainSound; 								// The main music.
	public AudioSource ResumeSound; 							// The resume sound.
	public GameObject ControlsScreen; 							// Controls screen.

	[Header ("Scoring")]
	public float displayedScore;
	public float CurrentScore; 									// The current score in the playthrough.
	public int LevelOneScore = 1000; 							// Reach this score so TWO types of hazards are able to be spawned.
	public int LevelTwoScore = 10000; 							// Reach this score so THREE types of hazards are able to be spawned.
	public int LevelThreeScore = 20000;							// Reach this score so FOUR types of hazards are able to be spawned.
	public int LevelFourScore = 50000; 							// Reach this score so FIVE types of hazards are able to be spawned.
	public int LevelFiveScore = 100000; 						// Reach this score so ALL types of hazards are able to be spawned.
	public float ScoreSpeed; 									// How fast the score increases per update.
	public GameObject Panel; 									// The panel behind the score text.
	public Text ScoreText; 										// The score text to display current score as an integer.
	public Text TimeScaleText; 									// The current time scale multiplier text.
	public Text GameOverScoreText; 								// The game over text to display final score.

	[Header ("Misc")]
	public GameObject ShowFpsText; 								// The UI text to display frames per second.
	public AudioSource CheatSound;
	public Animator CheatActivatedAnim;
	public GlobalMouseVisibility MouseScript; 					// Mouse visibility component.
	private Transform Player; 									// The Player's transform component.
	private PlayerController playerControllerScript;			// Player controller script component.
	private TimescaleController timeScaleControllerScript; 		// Timescale script component.
	private BrickStackController brickStackControllerScript; 	// Brick stack controller component.

	void Start () 
	{
		// Finds scripts.
		timeScaleControllerScript = GameObject.FindGameObjectWithTag ("TimeScaleController").GetComponent<TimescaleController> ();
		playerControllerScript = GameObject.Find ("Player").GetComponent<PlayerController> ();
		brickStackControllerScript = GameObject.FindGameObjectWithTag ("BrickStackController").GetComponent<BrickStackController> ();
		MouseScript = GameObject.FindGameObjectWithTag ("GlobalMouseController").GetComponent<GlobalMouseVisibility>();

		// Starts coroutines.
		StartCoroutine (PowerupSpawnWaves ());
		StartCoroutine (CountDown ());
		StartCoroutine (RareitemSpawnWaves ());

		isPreGame = true; 				// We are in pre-game mode.
		ShowFpsText.SetActive (false);  // Turns off showing FPS object.
		PauseUI.SetActive (false); 		// Disables Pause UI.

		// Start score.
		CurrentScore = 0;
		ScoreText.text = "" + 0 + "";
		wave = 1;

		//Sets application framerate to "as fast as possible"
		Application.targetFrameRate = -1;

		// Start cursor lock state.
		//Cursor.visible = true;
		Cursor.lockState = CursorLockMode.Locked;

		// turns off bottom barrier so the player can safely translate into the play space.
		BottomBarrier.GetComponent<BoxCollider>().enabled = false;

		// Checks if there is a time scale controller script present in scene.
		if (timeScaleControllerScript == null) 
		{
			Debug.Log ("Cannot find Timescale Controller script.");
		}

		// Checks if there is a brick stack controller script present in scene.
		if (brickStackControllerScript == null) 
		{
			Debug.Log ("Cannot find Brick Stack Controller script.");
		}

		// Finds Player transform.
		Player = GameObject.FindGameObjectWithTag ("Player").transform;

		if (Player == null) 
		{
			Debug.Log ("Cannot find Player.");
		}

		// Brick stack controller start conditions.
		int temp = brickStackControllerScript.GetTotalColumns;
		columnLocations = new float[temp];
		for (int i = 0; i < temp; i++) // X-loc for each column
			columnLocations [i] = brickStackControllerScript.GetBrickXPos (i);
	}

	void Update () 
	{
		// Sets fixed timestep to decrease proportionately to the timescale.
		Time.fixedDeltaTime = Time.timeScale * 0.01667f;
		// Fixed timestep   = time scale * 1/60;
		// E.g 				=        0.5 / 60 = 0.00833;
		// Since the time scale is lowered, the phyiscs update will be accurate.

		if (!isPaused)
		{
			if (GameObject.FindGameObjectWithTag ("Boss") != null) 
			{
				MainSound.GetComponent<AudioHighPassFilter> ().enabled = true;
			}
			if (GameObject.FindGameObjectWithTag ("Boss") == null) 
			{
				MainSound.GetComponent<AudioHighPassFilter> ().enabled = false;
			}

			LevelUpText.GetComponent<Text> ().text = "WAVE " + wave + "";
			WaveLabel.GetComponentInChildren<Text>().text = "WAVE " + wave + "";

			// Important!
			if (spawnWait > minSpawnWait) 
			{
				spawnWait -= speedupSpawnRate * Time.unscaledDeltaTime;
			}

			// If game is in pre-game mode.
			if (isPreGame == true) 
			{
				timeScaleControllerScript.enabled = false; 	// Turns off time scale controller script.
				Time.timeScale = 1.0f; 					   	// Sets time scale to 1.
				//isPaused = false; 							// Keeps game unpaused.
			}

			// If the game is not in pre-game mode, and player health is greater than 0.
			if (!isPreGame && playerControllerScript.Health > playerControllerScript.minHealth) 
			{
				// Sets UI for time multipler and converts to string format.
				TimeScaleText.text = "" + string.Format ("{0:0}", Mathf.Round (timeScaleControllerScript.timeScaleReadOnly * 100f)) + "%";

				// Sets score and shows on UI.
				ScoreText.text = "" + Mathf.Round (displayedScore) + "";
				displayedScore = Mathf.Lerp (displayedScore, CurrentScore, Time.deltaTime);

				if (twoPlayerMode == false) 
				{
					CurrentScore += Time.deltaTime * ScoreSpeed;
				}

				if (twoPlayerMode == true) 
				{
					CurrentScore += Time.deltaTime * (ScoreSpeed * 2);
				}
			}

			// Game Over scoring functionality has been moved to GameOverController.cs - Claire
			/*
			// If the game is not in pre-game mode, and the player's is at game over condition.
			if (!isPreGame && playerControllerScript.Health <= playerControllerScript.minHealth) 
			{
				// Sets game over score text.
				GameOverScoreText.text = "" + Mathf.Round(CurrentScore) + "";
			}*/

			// If the game's score is less than 0.
			if (CurrentScore < 0) 
			{
				CurrentScore = 0; // Make it equal to 0.
			}

			// HOTKEYS //
			GetInput();
			CheckForCheats();
		}
	}

	private void GetInput()
	{
		// Pauses game and enables mouse pointer.
		if (Input.GetKeyDown (KeyCode.Escape) && isPaused == false) 
		{
			PauseGame (); 												// Calls PauseGame method.

			Cursor.lockState = CursorLockMode.None; 					// Unlocks mouse cursor.
			Cursor.visible = true; 										// Makes cursor visible.

			MouseScript.visibleTime = MouseScript.visibleDuration; 		// Sets mouse duration
			MouseScript.enabled = false; 								// Turns off mouse visibility script.
		}

		// Pause via controller input.
		if (Input.GetButtonDown ("Pause") && isPaused == false)
		{
			if (playerControllerScript.useKeyboardControls == true) 
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				MouseScript.visibleTime = MouseScript.visibleDuration;
				MouseScript.enabled = false;
			}

			PauseGame ();
		}

		if (Input.GetKeyDown (KeyCode.K)) 
		{
			playerControllerScript.useKeyboardControls = true;
		}

		if (Input.GetKeyDown (KeyCode.G)) 
		{
			playerControllerScript.useKeyboardControls = false;
		}

		if (Input.GetKeyDown (KeyCode.P)) 
		{
			Debug.Log ("isPaused = " + isPaused);
		}

		if (Input.GetKeyDown (KeyCode.R) && playerControllerScript.Health > 0) 
		{
			// Restarts this scene.
			SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
		}

		if (Input.GetKeyDown (KeyCode.M) && playerControllerScript.Health > 0) 
		{
			// Loads back Menu.
			SceneManager.LoadScene ("menu");
		}

		if (Input.GetKeyDown (KeyCode.B) && playerControllerScript.Health > 0) 
		{
			// Loads game from the start
			SceneManager.LoadScene ("disclaimer");
		}

		// Show FPS.
		if (Input.GetKeyDown (KeyCode.F1)) 
		{
			ShowFpsText.SetActive (true); // Turns on FPS Game Object.
		}
	}

	private void CheckForCheats()
	{
		if (playerControllerScript.Health > 0) 
		{
			if (Input.GetKeyDown (KeyCode.Backslash))
			{
				int randomColumn = Random.Range (0, columnLocations.Length);
				Instantiate (Hazards[Random.Range(0, Hazards.Length)], new Vector3 (columnLocations [randomColumn], spawnValues.y, 0), Quaternion.identity);
				CheatSound.Play ();
				CheatActivatedAnim.Play ("CheatActivated");
				Debug.Log ("You pressed 'Backslach' and added an enemy to the game.");
			}

			if (Input.GetKeyDown (KeyCode.C)) 
			{
				CurrentScore += 10000;
				CheatSound.Play ();
				CheatActivatedAnim.Play ("CheatActivated");
				Debug.Log ("You pressed 'C' and added 10000 points.");
			}

			if (Input.GetKeyDown (KeyCode.V)) 
			{
				wave += 1;
				CheatSound.Play ();
				CheatActivatedAnim.Play ("CheatActivated");
				Debug.Log ("You pressed 'V' and increased your wave.");
			}

			if (Input.GetKeyDown (KeyCode.N) && spawnWait > 0.09f) 
			{
				spawnWait -= 0.1f;
				CheatSound.Play ();
				CheatActivatedAnim.Play ("CheatActivated");
				Debug.Log ("You pressed 'N' and decreased 'spawnwait'.");
			}

			if (Input.GetKeyDown (KeyCode.N) && spawnWait < 0.09f) 
			{
				spawnWait = 0.016f;
			}

			if (Input.GetKeyDown (KeyCode.Z)) 
			{
				hazardCount += 1;
				CheatSound.Play ();
				CheatActivatedAnim.Play ("CheatActivated");
				Debug.Log ("You pressed 'Z' and increased 'hazards'.");
			}

			if (Input.GetKeyDown (KeyCode.T)) 
			{
				playerControllerScript.powerupTime = 90;
				CheatSound.Play ();
				CheatActivatedAnim.Play ("CheatActivated");
				Debug.Log ("You pressed 'T' and added 90 seconds of powerup time.");
			}

			if (Input.GetKeyDown (KeyCode.Alpha1)) 
			{
				playerControllerScript.CurrentPowerup = PlayerController.powerup.RegularShot;
				CheatSound.Play ();
				CheatActivatedAnim.Play ("CheatActivated");
				Debug.Log ("You pressed '1' and powerup is regular shot.");
			}

			if (Input.GetKeyDown (KeyCode.Alpha2)) 
			{
				playerControllerScript.CurrentPowerup = PlayerController.powerup.DoubleShot;
				CheatSound.Play ();
				CheatActivatedAnim.Play ("CheatActivated");
				Debug.Log ("You pressed '2' and powerup is double shot.");
			}

			if (Input.GetKeyDown (KeyCode.Alpha3)) 
			{
				playerControllerScript.CurrentPowerup = PlayerController.powerup.TriShot;
				CheatSound.Play ();
				CheatActivatedAnim.Play ("CheatActivated");
				Debug.Log ("You pressed '3' and powerup is try shot.");
			}

			if (Input.GetKeyDown (KeyCode.Alpha4)) 
			{
				playerControllerScript.CurrentPowerup = PlayerController.powerup.BeamShot;
				CheatSound.Play ();
				CheatActivatedAnim.Play ("CheatActivated");
				Debug.Log ("You pressed '4' and powerup is beam shot.");
			}

			if (Input.GetKeyDown (KeyCode.Alpha5)) 
			{
				playerControllerScript.CurrentPowerup = PlayerController.powerup.horizontalBeam;
				CheatSound.Play ();
				CheatActivatedAnim.Play ("CheatActivated");
				Debug.Log ("You pressed '5' and powerup is horizontal beam shot.");
			}

			if (Input.GetKeyDown (KeyCode.Alpha6)) 
			{
				playerControllerScript.CurrentPowerup = PlayerController.powerup.Clone;
				Instantiate (playerControllerScript.ClonedPlayer, new Vector3 (0, -15, 0), Quaternion.Euler (90, 180, 0));
				CheatSound.Play ();
				CheatActivatedAnim.Play ("CheatActivated");
				Debug.Log ("You pressed '6' and powerup is clones.");
			}

			if (Input.GetKeyDown (KeyCode.Alpha7)) 
			{
				playerControllerScript.CurrentPowerup = PlayerController.powerup.shield;
				CheatSound.Play ();
				CheatActivatedAnim.Play ("CheatActivated");
				Debug.Log ("You pressed '7' and powerup is helix.");
			}

			if (Input.GetKeyDown (KeyCode.Alpha8)) 
			{
				playerControllerScript.CurrentPowerup = PlayerController.powerup.helix;
				CheatSound.Play ();
				CheatActivatedAnim.Play ("CheatActivated");
				Debug.Log ("You pressed '8' and powerup is shield.");
			}

			if (Input.GetKeyDown (KeyCode.Alpha9)) 
			{
				playerControllerScript.CurrentPowerup = PlayerController.powerup.wifi;
				CheatSound.Play ();
				CheatActivatedAnim.Play ("CheatActivated");
				Debug.Log ("You pressed '9' and powerup is ripple.");
			}

			if (Input.GetKeyDown (KeyCode.Alpha0)) 
			{
				playerControllerScript.Health -= 25;
				CheatSound.Play ();
				CheatActivatedAnim.Play ("CheatActivated");
				Debug.Log ("You pressed '0' and lost health.");

				if (playerControllerScript.Health < 25) 
				{
					playerControllerScript.GameOver ();
					Instantiate (playerControllerScript.gameOverExplosion, Player.transform.position, Quaternion.identity);
					Debug.Log ("You pressed '0' and GAME OVER.");
				}
			}
		}
	}

	public void StartSpawningBricks ()
	{
		StartCoroutine (BrickSpawnWaves ());
	}

	public void PauseGame ()
	{
		isPaused = true;
		timeScaleControllerScript.enabled = false; 						// Turns off Timescale controller script.
		Time.timeScale = 0; 											// Sets timescale to 0.
		PauseUI.SetActive (true); 										// Activates pause UI.
		PlayerPrefs.SetString ("InputMenu", "gamepause");
		Debug.Log ("Paused game.");
		MainSound.pitch = 1; 											// Sets custom pitch.
		MainSound.volume = 0.25f; 										// Sets custom volume.
		MainSound.GetComponent<AudioLowPassFilter> ().enabled = true; 	// Turns on low pass audio filter.
		playerControllerScript.LensScript.radius = 0;
	}

	public void UnPauseGame ()
	{
		timeScaleControllerScript.enabled = true; 						// Turns on Timescale controller script.
		PauseUI.SetActive (false); 										// Deactivates Pause UI.
		Time.timeScale = 1; 											// Sets timescale to 1.
		PlayerPrefs.SetString ("InputMenu", "");
		Debug.Log ("Unpaused game.");
		MainSound.pitch = 1; 											// Sets custom pitch.
		MainSound.volume = 0.7f;										// Sets custom volume.
		MainSound.GetComponent<AudioLowPassFilter> ().enabled = false; 	// Turns off low pass filter.
		isPaused = false;
		Cursor.visible = false;
	}

	IEnumerator CountDown ()
	{
		PreGameUI.SetActive (true); 									// Turns on PreGame UI.
		yield return new WaitForSeconds (CountDownDelay); 
		BottomBarrier.GetComponent<BoxCollider>().enabled = true;
		PreGameUI.SetActive (false); 									// Turns off PreGame UI.
		timeScaleControllerScript.enabled = true;
		PlayerAnim.enabled = false;
		yield return new WaitForSeconds (2);
	}

	public IEnumerator BrickSpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true) {
			int randomColumn;
			for (int i = 0; i < hazardCount; i++) 
			{
				if (wave >= 0 && wave <= 1) 
				{
					if (GameObject.FindGameObjectWithTag ("Boss") == null) 
					{
						GameObject hazard = Hazards [Random.Range (0, 4)];
						randomColumn = Random.Range (0, columnLocations.Length);
						Vector3 spawnPosition = new Vector3 (columnLocations [randomColumn], spawnValues.y, 0);
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (hazard, spawnPosition, spawnRotation);
						yield return new WaitForSeconds (spawnWait);
					}

					if (GameObject.FindGameObjectWithTag ("Boss") != null) 
					{
						yield return new WaitForSeconds (spawnWait);
					}
				}
				
				if (wave > 1 && wave <= 2) 
				{
					if (GameObject.FindGameObjectWithTag ("Boss") == null) 
					{
						GameObject hazard = Hazards [Random.Range (0, 9)];
						randomColumn = Random.Range (0, columnLocations.Length);
						Vector3 spawnPosition = new Vector3 (columnLocations [randomColumn], spawnValues.y, 0);
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (hazard, spawnPosition, spawnRotation);
						yield return new WaitForSeconds (spawnWait);
					}

					if (GameObject.FindGameObjectWithTag ("Boss") != null) 
					{
						yield return new WaitForSeconds (spawnWait);
					}
				}
				
				if (wave > 2 && wave <= 3) 
				{
					if (GameObject.FindGameObjectWithTag ("Boss") == null) 
					{
						GameObject hazard = Hazards [Random.Range (0, 14)];
						randomColumn = Random.Range (0, columnLocations.Length);
						Vector3 spawnPosition = new Vector3 (columnLocations [randomColumn], spawnValues.y, 0);
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (hazard, spawnPosition, spawnRotation);
						yield return new WaitForSeconds (spawnWait);
					}

					if (GameObject.FindGameObjectWithTag ("Boss") != null) 
					{
						yield return new WaitForSeconds (spawnWait);
					}
				}

				if (wave > 3 && wave <= 4) 
				{
					if (GameObject.FindGameObjectWithTag ("Boss") == null) 
					{
						GameObject hazard = Hazards [Random.Range (0, 19)];
						randomColumn = Random.Range (0, columnLocations.Length);
						Vector3 spawnPosition = new Vector3 (columnLocations [randomColumn], spawnValues.y, 0);
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (hazard, spawnPosition, spawnRotation);
						yield return new WaitForSeconds (spawnWait);
					}

					if (GameObject.FindGameObjectWithTag ("Boss") != null) 
					{
						yield return new WaitForSeconds (spawnWait);
					}
				}

				if (wave > 4 && wave <= 5) 
				{
					if (GameObject.FindGameObjectWithTag ("Boss") == null) 
					{
						GameObject hazard = Hazards [Random.Range (0, 24)];
						randomColumn = Random.Range (0, columnLocations.Length);
						Vector3 spawnPosition = new Vector3 (columnLocations [randomColumn], spawnValues.y, 0);
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (hazard, spawnPosition, spawnRotation);
						yield return new WaitForSeconds (spawnWait);
					}

					if (GameObject.FindGameObjectWithTag ("Boss") != null) 
					{
						yield return new WaitForSeconds (spawnWait);
					}
				}

				// Can spawn multi-column brick groups
				if (wave > 5) 
				{
					if (GameObject.FindGameObjectWithTag ("Boss") == null) 
					{
						GameObject hazard = Hazards [Random.Range (0, Hazards.Length)];
						randomColumn = GetRandomForBrickGroup (hazard);
						Vector3 spawnPosition = new Vector3 (columnLocations [randomColumn], spawnValues.y, 0);
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (hazard, spawnPosition, spawnRotation);
						yield return new WaitForSeconds (spawnWait);
					}

					if (GameObject.FindGameObjectWithTag ("Boss") != null) 
					{
						yield return new WaitForSeconds (spawnWait);
					}
				}
			}

			yield return new WaitForSeconds (waveWait / 2);
			GameObject rareitem = Rareitems [Random.Range (7, Rareitems.Length)];

			if (GameObject.FindGameObjectWithTag ("Boss") == null) 
			{
				Instantiate (rareitem, new Vector3 (0, 40, 0), Quaternion.Euler (0, 0, 0));
			}
			yield return new WaitForSeconds (waveWait / 2);
		}
	}

	private int GetRandomForBrickGroup (GameObject hazard)
	{
		if(transform.childCount > 0) // if hazard is a brick group
		{
			Transform firstChild = hazard.transform.GetChild(0);
			BrickMovement sourceBrickMove = firstChild.gameObject.GetComponent <BrickMovement>();

			if (sourceBrickMove.isSourceBrick) // if hazard is a multi-column brick group
			{
				int groupWidth = sourceBrickMove.groupWidth;
				int stackWidth = brickStackControllerScript.GetTotalColumns;

				return Random.Range (0, stackWidth - groupWidth);
			}
		}

		return Random.Range (0, columnLocations.Length);
	}

	IEnumerator PowerupSpawnWaves ()
	{
		yield return new WaitForSeconds (powerupStartWait);
		while (true)
		{
			for (int i = 0; i < powerupCount; i++) 
			{
				GameObject powerup = Powerups [Random.Range (0, Powerups.Length)];
				Vector3 powerupSpawnPos = new Vector3 (Mathf.RoundToInt (Random.Range (-powerupSpawnValues.x, powerupSpawnValues.x)), powerupSpawnValues.y, powerupSpawnValues.z);
				Instantiate (powerup, powerupSpawnPos, Quaternion.Euler(0, 0, 180));
				yield return new WaitForSeconds (powerupSpawnWait);
			}
		}
	}

	// For Health
	IEnumerator RareitemSpawnWaves ()
	{
		yield return new WaitForSeconds (rareitemStartWait);
		while (true) {
			for (int i = 0; i < rareitemCount; i++) {
				GameObject rareitem = Rareitems [Random.Range (0, 4)];
				Vector3 rareitemSpawnPos = new Vector3 (Mathf.RoundToInt (Random.Range (-rareitemSpawnValues.x, rareitemSpawnValues.x)), rareitemSpawnValues.y, rareitemSpawnValues.z);
				Instantiate (rareitem, rareitemSpawnPos, Quaternion.identity);
				yield return new WaitForSeconds (rareitemSpawnWait);
			}
		}
	}
}
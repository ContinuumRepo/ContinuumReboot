using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour 
{
	private Transform Player;
	private PlayerController playerControllerScript;
	private TimescaleController timeScaleControllerScript; // Timescale script component.

	[Header ("MouseStates")]
	public GlobalMouseVisibility MouseScript;

	[Header ("Pre-game")]
	public bool isPreGame;
	public GameObject PreGameUI;
	public float CountDownDelay = 5.0f;
	public Animator PlayerAnim;
	public GameObject BottomBarrier;

	[Header ("Spawning Objects")]
	public GameObject[] Hazards;
	public Vector3 spawnValues;
	public float startWait;
	public float spawnWait;
	public float waveWait;
	public float hazardCount;

	[Header ("Pausing")]
	public bool isPaused;
	public GameObject PauseUI;
	public AudioSource PauseSound;
	public AudioSource MainSound;
	public AudioSource ResumeSound;
	public AudioSourcePitchByTimescale AudioPitchScript;

	[Header ("Scoring")]
	public float CurrentScore;
	public int LevelOneScore = 1000;
	public int LevelTwoScore = 10000;
	public int LevelThreeScore = 20000;
	public int LevelFourScore = 50000;
	public int LevelFiveScore = 100000;
	public float ScoreSpeed; // How fast the score increases per frame.
	public GameObject PanelL;
	public GameObject PanelR;
	public Text ScoreL;
	public Text ScoreR;
	public Text TimeScaleTextL;
	public Text TimeScaleTextR;
	public Text GameOverScoreText;
	public Text HighestTimeScaleText;

	void Start () 
	{
		// Starts coroutines.
		StartCoroutine (BrickSpawnWaves ());
		StartCoroutine (CountDown ());

		// Start cursor lock state.
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Confined;
		MouseScript = GameObject.FindGameObjectWithTag ("GlobalMouseController").GetComponent<GlobalMouseVisibility>();

		isPreGame = true;

		// turns off bottom barrier so the player can safely translate into the play space.
		BottomBarrier.GetComponent<BoxCollider>().enabled = false;

		PanelL.SetActive (false); // Turns off left panel.
		PanelR.SetActive (true); // Turns on right panel.
		PauseUI.SetActive (false); // Disables Pause UI.

		// Finds TimeScale Controller Component.
		timeScaleControllerScript = GameObject.FindGameObjectWithTag ("TimeScaleController").GetComponent<TimescaleController> ();

		playerControllerScript = GameObject.Find ("Player").GetComponent<PlayerController> ();

		if (timeScaleControllerScript == null) 
		{
			Debug.Log ("Cannot find Timescale Controller script.");
		}

		// Finds Player transform.
		Player = GameObject.FindGameObjectWithTag ("Player").transform;

		if (Player == null) 
		{
			Debug.Log ("Cannot find Player.");
		}

		// Start score.
		CurrentScore = 0;
		ScoreL.text = "" + 0 + "";
		ScoreR.text = "" + 0 + "";
	}

	void Update () 
	{
		if (isPreGame) 
		{
			timeScaleControllerScript.enabled = false;
			isPaused = false;
		}

		if (!isPreGame && playerControllerScript.Health > playerControllerScript.minHealth) 
		{
			// Sets UI for time multipler and converts to string format.
			TimeScaleTextL.text = "" + string.Format ("{0:0}", Mathf.Round (timeScaleControllerScript.timeScaleReadOnly * 100f)) + "%";
			TimeScaleTextR.text = "" + string.Format ("{0:0}", Mathf.Round (timeScaleControllerScript.timeScaleReadOnly * 100f)) + "%";

			// Sets score and shows on UI.
			CurrentScore += Time.deltaTime * ScoreSpeed;
			ScoreL.text = "" + Mathf.Round (CurrentScore) + "";
			ScoreR.text = "" + Mathf.Round (CurrentScore) + "";

			// When the player starts to approach the bottom of the screen
			if (Player.transform.position.y < -14.0f) 
			{
				// When the Player is positioned on the left hand side of the screen, (or exactly in the middle (when transform.position.x = 0)). 
				if (Player.transform.position.x <= 0) 
				{
					PanelL.SetActive (false); // Turns off left panel.
					PanelR.SetActive (true); // Turns on right panel.
				}

				// When the Player is positioned on the right hand side of the screen. 
				if (Player.transform.position.x > 0) 
				{
					PanelL.SetActive (true); // Turns on left panel.
					PanelR.SetActive (false); // Turns on right panel.
				}
			}

			if (Player.transform.position.y >= -14.0f)
			{
				PanelL.SetActive (false); // Turns off left panel.
				PanelR.SetActive (true); // Turns on right panel.
			}
		}

		if (!isPreGame && playerControllerScript.Health <= playerControllerScript.minHealth) 
		{
			GameOverScoreText.text = "" + Mathf.Round(CurrentScore) + "";
			HighestTimeScaleText.text = "" + string.Format ("{0:0}", Mathf.Round (timeScaleControllerScript.highestTimeScale * 100f)) + "%";
		}

		if (CurrentScore < 0) 
		{
			CurrentScore = 0;
		}

		// HOTKEYS //

		// Pauses game and enables mouse pointer.
		if (Input.GetKeyDown (KeyCode.Escape) && isPaused == false) 
		{
			isPaused = true;
			PauseGame ();

			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;

			MouseScript.visibleTime = MouseScript.visibleDuration;
			MouseScript.enabled = false;
		}

		if (Input.GetKeyDown (KeyCode.P)) 
		{
			isPaused = false;
			UnPauseGame ();
		}

		if (Input.GetKeyDown (KeyCode.R)) 
		{
			// Restarts this scene.
			SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
		}

		if (Input.GetKeyDown (KeyCode.M)) 
		{
			// Loads back Menu.
			SceneManager.LoadScene ("menu");
		}

		if (Input.GetKeyDown (KeyCode.B)) 
		{
			// Loads game from the start
			SceneManager.LoadScene ("disclaimer");
		}

		// Right click to enable mouse pointer
		if (Input.GetMouseButtonDown (1)) 
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			MouseScript.visibleTime = MouseScript.visibleDuration;
		}
	}

	public void StartSpawningBricks ()
	{
		StartCoroutine (BrickSpawnWaves ());
	}

	public void PauseGame ()
	{
		timeScaleControllerScript.enabled = false; // Turns off Timescale controller script.
		Time.timeScale = 0; // Sets timescale to 0.
		PauseUI.SetActive (true); // Activates pause UI.
		Debug.Log ("Paused game.");
		AudioPitchScript.enabled = false; // Turns off audio pitch by timescale script on the audio.
		MainSound.pitch = 1; // Sets custom pitch.
		MainSound.volume = 0.25f; // Sets custom volume.
		MainSound.GetComponent<AudioLowPassFilter> ().enabled = true; // Turns on low pass audio filter.
		Cursor.lockState = CursorLockMode.None; // Unlocks the cursor.
		Cursor.visible = true; // Makes cursor visible.
	}

	public void UnPauseGame ()
	{
		timeScaleControllerScript.enabled = true; // Turns on Timescale controller script.
		PauseUI.SetActive (false); // Deactivates Pause UI.
		Time.timeScale = 1; // Sets timescale to 1.
		Debug.Log ("Unpaused game.");
		AudioPitchScript.enabled = true; // Enables audio pich script.
		MainSound.pitch = 1; // Sets custom pitch.
		MainSound.volume = 0.7f; // Sets custom volume.
		MainSound.GetComponent<AudioLowPassFilter> ().enabled = false; // Turns off low pass filter.
		//Cursor.lockState = CursorLockMode.Locked; // Locks the cursor.
		Cursor.visible = false; // Makes cursor invisible.
		isPaused = false;

	}

	IEnumerator CountDown ()
	{
		//timeScaleControllerScript.enabled = false;
		PreGameUI.SetActive (true); // Turns on PreGame UI.
		yield return new WaitForSeconds (CountDownDelay); 
		BottomBarrier.GetComponent<BoxCollider>().enabled = true;
		isPreGame = false; // Takes out of pre game mode.
		PreGameUI.SetActive (false); // Turns off PreGame UI.
		timeScaleControllerScript.enabled = true;
		PlayerAnim.enabled = false;
	}

	IEnumerator BrickSpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true) 
		{
			for (int i = 0; i < hazardCount; i++) 
			{
				// Spawn element 0 if less than level one score
				if (CurrentScore > 0 && CurrentScore < LevelOneScore)
				{
					GameObject hazard = Hazards [Random.Range (0, 1)];
					Vector3 spawnPosition = new Vector3 (Mathf.RoundToInt(Random.Range(-spawnValues.x, spawnValues.x)), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;
					Instantiate (hazard, spawnPosition, spawnRotation);
					yield return new WaitForSeconds (spawnWait);
				}

				// Spawn element 0 and 1 if less than level 2 score.
				if (CurrentScore > LevelOneScore && CurrentScore < LevelTwoScore)
				{
					GameObject hazard = Hazards [Random.Range (0, 2)];
					Vector3 spawnPosition = new Vector3 (Mathf.RoundToInt(Random.Range(-spawnValues.x, spawnValues.x)), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;
					Instantiate (hazard, spawnPosition, spawnRotation);
					yield return new WaitForSeconds (spawnWait);
				}


				if (CurrentScore > LevelTwoScore && CurrentScore < LevelThreeScore)
				{
					GameObject hazard = Hazards [Random.Range (0, 3)];
					Vector3 spawnPosition = new Vector3 (Mathf.RoundToInt(Random.Range(-spawnValues.x, spawnValues.x)), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;
					Instantiate (hazard, spawnPosition, spawnRotation);
					yield return new WaitForSeconds (spawnWait);
				}

				if (CurrentScore > LevelThreeScore && CurrentScore < LevelFourScore)
				{
					GameObject hazard = Hazards [Random.Range (0, 4)];
					Vector3 spawnPosition = new Vector3 (Mathf.RoundToInt(Random.Range(-spawnValues.x, spawnValues.x)), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;
					Instantiate (hazard, spawnPosition, spawnRotation);
					yield return new WaitForSeconds (spawnWait);
				}

				if (CurrentScore > LevelFourScore && CurrentScore < LevelFiveScore)
				{
					GameObject hazard = Hazards [Random.Range (0, 5)];
					Vector3 spawnPosition = new Vector3 (Mathf.RoundToInt(Random.Range(-spawnValues.x, spawnValues.x)), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;
					Instantiate (hazard, spawnPosition, spawnRotation);
					yield return new WaitForSeconds (spawnWait);
				}

				if (CurrentScore > LevelFiveScore)
				{
					GameObject hazard = Hazards [Random.Range (0, Hazards.Length)];
					Vector3 spawnPosition = new Vector3 (Mathf.RoundToInt(Random.Range(-spawnValues.x, spawnValues.x)), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;
					Instantiate (hazard, spawnPosition, spawnRotation);
					yield return new WaitForSeconds (spawnWait);
				}
			}
			yield return new WaitForSeconds (waveWait);
		}
	}
}

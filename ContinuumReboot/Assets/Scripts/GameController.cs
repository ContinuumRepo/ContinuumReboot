using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour 
{
	private Transform Player;
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
	public float ScoreSpeed; // How fast the score increases per frame.
	public GameObject PanelL;
	public GameObject PanelR;
	public Text ScoreL;
	public Text ScoreR;
	public Text TimeScaleTextL;
	public Text TimeScaleTextR;

	void Start () 
	{
		StartCoroutine (BrickSpawnWaves ());
		// Disables and hides mouse movement.
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Confined;

		MouseScript = GameObject.FindGameObjectWithTag ("GlobalMouseController").GetComponent<GlobalMouseVisibility>();

		isPreGame = true;
		BottomBarrier.GetComponent<BoxCollider>().enabled = false;
		StartCoroutine (CountDown ());

		PanelL.SetActive (false); // Turns off left panel.
		PanelR.SetActive (true); // Turns on right panel.
		PauseUI.SetActive (false); // Disables Pause UI.

		// Finds TimeScale Controller Component.
		timeScaleControllerScript = GameObject.FindGameObjectWithTag ("TimeScaleController").GetComponent<TimescaleController> ();

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
	}

	void Update () 
	{
		if (isPreGame) 
		{
			timeScaleControllerScript.enabled = false;
			isPaused = false;
		}

		if (!isPreGame) 
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
				GameObject hazard = Hazards [Random.Range (0, Hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
		}
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour 
{
	private Transform Player;
	private TimescaleController timeScaleControllerScript; // Timescale script component.

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

	[Header ("UI Elements")]
	public GameObject PanelL;
	public GameObject PanelR;
	public Text ScoreL;
	public Text ScoreR;
	public Text TimeScaleTextL;
	public Text TimeScaleTextR;

	void Start () 
	{
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

		// HOTKEYS //
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			isPaused = true;
			PauseGame ();
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
	}

	void PauseGame ()
	{
		timeScaleControllerScript.enabled = false;
		Time.timeScale = 0;
		PauseUI.SetActive (true);
		Debug.Log ("Paused game.");
		AudioPitchScript.enabled = false;
		MainSound.pitch = 1;
		MainSound.volume = 0.25f;
		MainSound.GetComponent<AudioLowPassFilter> ().enabled = true;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	void UnPauseGame ()
	{
		timeScaleControllerScript.enabled = true;
		PauseUI.SetActive (false);
		Time.timeScale = 1;
		Debug.Log ("Unpaused game.");
		AudioPitchScript.enabled = true;
		MainSound.pitch = 1;
		MainSound.volume = 0.7f;
		MainSound.GetComponent<AudioLowPassFilter> ().enabled = false;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
}

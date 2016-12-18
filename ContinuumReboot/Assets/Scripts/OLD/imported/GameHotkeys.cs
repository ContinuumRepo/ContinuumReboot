using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHotkeys : MonoBehaviour 
{
	public TimeController timeControllerScript;
	public Game gameControllerScript;
	public Text PauseText;
	public GameObject PausePanel;
	public AudioSource BackgroundMusic;
	public AudioSource WarpStarsAudio;
	public bool wipeStats;

	void Start () 
	{
		PauseText.text = "";
		PausePanel.SetActive (false);
		wipeStats = false;
	}

	void Update () 
	{
		// Restarting the scene
		if (Input.GetKey (KeyCode.R)) 
		{
			// Application.LoadLevel (Application.loadedLevel); (Obsolete)
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}

		// Pausing the game
		if (Input.GetMouseButtonUp (0)) 
		{
			if (gameControllerScript.isPreGame == false) 
			{
				timeControllerScript.enabled = false;
				Time.timeScale = 0;
				PauseText.text = "PAUSED";
				PausePanel.SetActive (true);
				WarpStarsAudio.Pause ();
				BackgroundMusic.Pause ();
				gameControllerScript.isPaused = true;
			}
		}

		// Resuming the game
		if (Input.GetMouseButtonDown (0)) 
		{
			BackgroundMusic.pitch = 1;
			Time.timeScale = 1.0f;
			timeControllerScript.enabled = true;
			PauseText.text = "";
			PausePanel.SetActive (false);
			//WarpStarsAudio.UnPause ();
			gameControllerScript.isPaused = false;
			BackgroundMusic.UnPause ();
		}

		// Wipes PlayerPrefs stuff
		if (Input.GetKeyDown (KeyCode.L) && wipeStats == false) 
		{
			PlayerPrefs.SetInt ("High Score", 0);
			PlayerPrefs.SetInt ("Total Points", 0);
			Debug.Log ("Deleted stats.");
			wipeStats = true;
		}
	}
}

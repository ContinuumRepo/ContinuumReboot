using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour 
{
	private Transform Player;
	private TimescaleController timeScaleControllerScript; // Timescale script component.

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
		GameObject timeScaleObject = GameObject.FindGameObjectWithTag ("TimeScaleController");
		timeScaleControllerScript = timeScaleObject.GetComponent<TimescaleController> ();

		PanelL.SetActive (false); // Turns off left panel.
		PanelR.SetActive (true); // Turns on right panel.

		GameObject PlayerObject = GameObject.FindGameObjectWithTag ("Player");
		Player = PlayerObject.transform;
	}

	void Update () 
	{
		// Sets UI for time multipler and converts to string format.
		TimeScaleTextL.text = "" + string.Format ("{0:0}", Mathf.Round (timeScaleControllerScript.TimeScaleReadOnly * 100f)) + "%";
		TimeScaleTextR.text = "" + string.Format ("{0:0}", Mathf.Round (timeScaleControllerScript.TimeScaleReadOnly * 100f)) + "%";

		CurrentScore += Time.deltaTime * ScoreSpeed;
		ScoreL.text = "" + Mathf.Round (CurrentScore) + "";
		ScoreR.text = "" + Mathf.Round (CurrentScore) + "";

		// When the player starts to approach the bottom of the screen
		if (Player.transform.position.y < -14.0f) 
		{
			// When the Player is positioned on the left hand side of the screen, (or exactly in the middle (when transform.position.x = 0)). 
			if (Player.transform.position.x <= 0) {
				PanelL.SetActive (false); // Turns off left panel.
				PanelR.SetActive (true); // Turns on right panel.
			}

			// When the Player is positioned on the right hand side of the screen. 
			if (Player.transform.position.x > 0) {
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
		if (Input.GetKeyDown (KeyCode.R)) 
		{
			// Restarts this scene.
			SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
		}
			
	}
}

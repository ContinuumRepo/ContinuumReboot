using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour 
{
	public Transform Player;
	public TimescaleController timeScaleControllerScript;

	[Header ("UI Elements")]
	public GameObject PanelL;
	public GameObject PanelR;
	public Text TimeScaleTextL;
	public Text TimeScaleTextR;

	void Start () 
	{
		GameObject timeScaleObject = GameObject.FindGameObjectWithTag ("TimeScaleController");
		timeScaleControllerScript = timeScaleObject.GetComponent<TimescaleController> ();

		PanelL.SetActive (false); // Turns off left panel.
		PanelR.SetActive (true); // Turns on right panel.
	}

	void Update () 
	{
		// Sets UI for time multipler.
		TimeScaleTextL.text = "" + string.Format ("{0:0}", Mathf.Round (timeScaleControllerScript.TimeScaleReadOnly * 100f)) + "%";
		TimeScaleTextR.text = "" + string.Format ("{0:0}", Mathf.Round (timeScaleControllerScript.TimeScaleReadOnly * 100f)) + "%";


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

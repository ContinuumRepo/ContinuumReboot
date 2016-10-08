using UnityEngine;
using System.Collections;

public class QuitScript : MonoBehaviour 
{
	public ButtonEvents noButton;

	void Update ()
	{
		// If A, start or return are pressed
		if (Input.GetKeyDown ("joystick button 0") == true || Input.GetKeyDown ("joystick button 7") == true || Input.GetKeyDown ("return") == true)
		{
			QuitGame();
		}
		// If B or escape has been pressed
		else if (Input.GetKeyDown ("joystick button 1") == true || Input.GetKeyDown ("escape") == true)
		{
			noButton.OnClick();
		}
	}

	public void QuitGame ()
	{
		Application.Quit ();

		#if UNITY_EDITOR
			Debug.Log ("Tried to quit in editor.");
		#endif
	}
}

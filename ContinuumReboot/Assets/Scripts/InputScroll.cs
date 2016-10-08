using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputScroll : MonoBehaviour
{
	public ButtonEvents[] buttons;
	public float timeBuffer; // Time between button scroll
	public string inputLocPrefsValue;
	public bool startToSelect = true; // If true, start will behave like the A button
	public bool bToClose;
	public GameObject bDeactivate; // Will call if bBackButton is null
	public ButtonEvents bBackButton; // Calls OnClick for this button if set
	public string bPrefsValue;

	private int buttonIndex;
	private int indexLocation; // What button the player currently has selected
	private int lastIndex = 0;

	private bool waiting = false;
	private bool resetForMouse = false;

	// Use this for initialization
	void Start ()
	{
		buttonIndex = buttons.Length;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (PlayerPrefs.GetString ("InputMenu") == inputLocPrefsValue)
		{
			if (buttonIndex > 0 && !waiting)
			{
				float valueJoy = Input.GetAxis ("Vertical P1");
				float valueKey = Input.GetAxis ("Vertical");

				if (valueJoy != 0)
				{
					resetForMouse = false;
					CheckScroll (valueJoy);
				}
				else if (valueKey != 0)
				{
					resetForMouse = false;
					CheckScroll (valueKey);
				}
			}

			// When selecting a button run that button's OnClick method (A, Start, space, enter/return)
			if (Input.GetKeyDown ("joystick button 0") == true || (startToSelect && Input.GetKeyDown ("joystick button 7") == true) || Input.GetKeyDown ("space") == true || Input.GetKeyDown ("return") == true)
			{
				buttons[indexLocation].OnClick();
			}

			// If enabled, allow player to cancel to close the current menu (B, Start, escape)
			if (bToClose && (Input.GetKeyDown ("joystick button 1") == true || (!startToSelect && Input.GetKeyDown ("joystick button 7") == true) || Input.GetKeyDown ("escape") == true))
			{
				PlayerPrefs.SetString ("InputMenu", bPrefsValue);
				if (bBackButton != null)
				{
					bBackButton.OnClick();
				}
				else
				{
					bDeactivate.SetActive (false);
				}
			}

			// If mouse is moved, call onExit on last selected button
			if (resetForMouse == false)
			{
				if (Input.GetAxis ("Mouse X") != 0 || Input.GetAxis ("Mouse Y") != 0)
				{
					buttons[indexLocation].OnExit();
					resetForMouse = true;
					Debug.Log ("Resetting for mouse movement");
				}
			}
		}
	}

	void OnEnable ()
	{
		indexLocation = 0;
		SetHighlighted (0);
	}

	private void CheckScroll (float value)
	{
		waiting = true;

		if (value > 0) // Select button above
		{
			/*if (idxSet == false) // When the player first inputs, set highlighted to be first button
			{
				SetHighlighted (0);
			}
			else */if (indexLocation == 0) // If current highlighted button is last, set first button to be highlighted
			{
				SetHighlighted (buttonIndex - 1);
			}
			else
			{
				SetHighlighted (indexLocation - 1);
			}
		}
		else if (value < 0) // Select button below
		{
			/*if (idxSet == false) // When the player first inputs, set highlighted to be first button
			{
				SetHighlighted (0);
			}
			else */if (indexLocation == buttonIndex - 1) // If current highlighted button is last, set first button to be highlighted
			{
				SetHighlighted (0);
			}
			else
			{
				SetHighlighted (indexLocation + 1);
			}
		}

		StartCoroutine (ScrollWait());
	}

	IEnumerator ScrollWait()
	{
		yield return WaitForUnscaledSeconds (timeBuffer);
		waiting = false;
	}

	IEnumerator WaitForUnscaledSeconds (float time)
	{
		float ttl = 0;
		while(time > ttl)
		{
			ttl += Time.unscaledDeltaTime;
			yield return null;
		}
	}

	private void SetHighlighted (int newIndex)
	{
		indexLocation = newIndex;
		//idxSet = true;

		buttons[lastIndex].OnExit();
		buttons[newIndex].OnEnter();

		lastIndex = newIndex;
	}

	public int HighlightedButton
	{
		set {indexLocation = value;}
	}
}

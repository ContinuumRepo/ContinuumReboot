using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputScroll : MonoBehaviour
{
	public ButtonEvents[] buttons;
	public float dead = 0.1f;
	public float timeBuffer; // Time between button scroll
	public string locationPrefsValue;

	private bool idxSet = false;
	private int buttonIndex;
	[SerializeField]
	private int indexLocation; // What button the player currently has selected
	private int lastIndex;

	private bool waiting = false;
	private bool resetForMouse = false;

	// Use this for initialization
	void Start ()
	{
		PlayerPrefs.SetString ("Menu", locationPrefsValue);
		buttonIndex = buttons.Length;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (PlayerPrefs.GetString ("Menu") == locationPrefsValue)
		{
			if (buttonIndex > 0 && !waiting)
			{
				float valueJoy = Input.GetAxis ("Vertical P1");
				float valueKey = Input.GetAxis ("Vertical");

				if (valueJoy < -dead || valueJoy > dead)
				{
					resetForMouse = false;
					CheckScroll (valueJoy);
				}
				else if (valueKey < -dead || valueKey > dead)
				{
					resetForMouse = false;
					CheckScroll (valueKey);
				}
			}

			if (Input.GetAxis ("FireButton") > 0)
			{
				buttons[indexLocation].OnClick();
			}

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

	private void CheckScroll (float value)
	{
		waiting = true;

		if (value > dead) // Select button above
		{
			if (idxSet == false) // When the player first inputs, set highlighted to be first button
			{
				SetHighlighted (0);
			}
			else if (indexLocation == 0) // If current highlighted button is last, set first button to be highlighted
			{
				SetHighlighted (buttonIndex - 1);
			}
			else
			{
				SetHighlighted (indexLocation - 1);
			}
		}
		else if (value < -dead) // Select button below
		{
			if (idxSet == false) // When the player first inputs, set highlighted to be first button
			{
				SetHighlighted (0);
			}
			else if (indexLocation == buttonIndex - 1) // If current highlighted button is last, set first button to be highlighted
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
		yield return new WaitForSeconds (timeBuffer);
		waiting = false;
	}

	private void SetHighlighted (int newIndex)
	{
		indexLocation = newIndex;
		idxSet = true;

		buttons[lastIndex].OnExit();
		buttons[newIndex].OnEnter();

		lastIndex = newIndex;
	}

	public int HighlightedButton
	{
		set {indexLocation = value;}
	}

	/*
	private void RunOnClick (int index)
	{
		switch (index)
		{
		case 0: // 1 Player
			buttonScript.P1Click();
			break;
		case 1: // 2 Player
			buttonScript.P2Click();
			break;
		case 2: // Leaderboards
			buttonScript.LeaderboardsClick();
			break;
		case 3: // Settings
			buttonScript.SettingsClick();
			break;
		case 4: // Credits
			buttonScript.CreditsClick();
			break;
		case 5: // Quit
			buttonScript.QuitClick();
			break;
		}
	}

	private void RunOnEnter (int index)
	{
		switch (index)
		{
		case 0: // 1 Player
			buttonScript.P1Enter();
			break;
		case 1: // 2 Player
			buttonScript.P2Enter();
			break;
		case 2: // Leaderboards
			buttonScript.LeaderboardsEnter();
			break;
		case 3: // Settings
			buttonScript.SettingsEnter();
			break;
		case 4: // Credits
			buttonScript.CreditsEnter();
			break;
		case 5: // Quit
			buttonScript.QuitEnter();
			break;
		}
	}

	private void RunOnExit (int index)
	{
		switch (index)
		{
		case 0: // 1 Player
			buttonScript.P1Exit();
			break;
		case 1: // 2 Player
			buttonScript.P2Exit();
			break;
		case 2: // Leaderboards
			buttonScript.LeaderboardsExit();
			break;
		case 3: // Settings
			buttonScript.SettingsExit();
			break;
		case 4: // Credits
			buttonScript.CreditsExit();
			break;
		case 5: // Quit
			buttonScript.QuitExit();
			break;
		}
	}
	*/
}

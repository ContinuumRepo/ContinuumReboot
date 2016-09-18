using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputScrollMenu : MonoBehaviour
{
	public MenuButtons buttonScript;
	public GameObject[] buttons;
	public float dead = 0.1f;
	public float timeBuffer;

	private bool idxSet = false;
	private int buttonIndex;
	[SerializeField]
	private int indexLocation; //What button the player currently has selected
	private int lastIndex;

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
		if (buttonIndex > 0 && !waiting)
		{
			float value = Input.GetAxis ("Vertical");

			if (value < -dead || value > dead)
			{
				resetForMouse = false;
				StartCoroutine (CheckScroll (value));
			}
		}
		/*
		if (resetForMouse == false)
		{
			if (Input.GetAxis ("Mouse X") > dead || Input.GetAxis ("Mouse X") > -dead || Input.GetAxis ("Mouse Y") > dead || Input.GetAxis ("Mouse Y") > -dead)
			{
				RunOnExit (indexLocation);
				resetForMouse = true;
			}
		}*/
	}

	IEnumerator CheckScroll (float value)
	{
		waiting = true;

		if (value < -dead) // Select button above
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
		else if (value > dead) // Select button below
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
		yield return StartCoroutine (ScrollWait());
		waiting = false;
	}

	IEnumerator ScrollWait()
	{
		yield return new WaitForSeconds (timeBuffer);
	}

	private void SetHighlighted (int newIndex)
	{
		indexLocation = newIndex;
		idxSet = true;

		RunOnExit (lastIndex);
		RunOnEnter (newIndex);

		lastIndex = newIndex;
	}

	public int HighlightedButton
	{
		set {indexLocation = value;}
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
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameOverController : MonoBehaviour
{
	public ButtonEvents[] buttons;
	public float timeBuffer; // Time between button scroll
	public string inputLocPrefsValue;

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

			if (Input.GetKeyDown ("space") == true ||Input.GetKeyDown ("joystick button 0") == true || Input.GetKeyDown ("return") == true)
			{
				Debug.Log ("Click");
				buttons[indexLocation].OnClick();
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

	private void CheckScroll (float value)
	{
		waiting = true;

		if (value > 0) // Select button above
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
		else if (value < 0) // Select button below
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
		idxSet = true;

		buttons[lastIndex].OnExit();
		buttons[newIndex].OnEnter();

		lastIndex = newIndex;
	}

	public int HighlightedButton
	{
		set {indexLocation = value;}
	}
}

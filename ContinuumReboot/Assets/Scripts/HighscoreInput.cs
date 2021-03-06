﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighscoreInput : MonoBehaviour
{
	public string[] characters;
	public Text[] nameInputs;
	public GameObject[] underlines;
	public float timeBuffer; // Time between button scroll
	public AudioSource oneShotInit;
	public AudioSource oneShotChar;
	public GameOverController gameOverCont;

	private bool waiting = false;
	private int nameIdxLoc = 0;
	private int[] charIdxLoc = new int[3] {0, 0, 0};
	private int charLenth;
	private int nameLength;

	private float scrollWait;
	private int scrollNo = 0;

	void Start ()
	{
		PlayerPrefs.SetString ("InputMenu", "highscoreinput");
		charLenth = characters.Length;
		nameLength = nameInputs.Length;
		scrollWait = timeBuffer;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!waiting)
		{
			if (scrollNo == 2)
			{
				scrollWait = timeBuffer / 2;
			}

			// Check for character change
			float vertJoy = Input.GetAxis ("Vertical P1");
			float vertKey = Input.GetAxis ("Vertical");

			if (vertJoy > 0.5 || vertJoy < -0.5)
			{
				ScrollChar (vertJoy);
			}
			else if (vertKey > 0.5 || vertKey < -0.5)
			{
				ScrollChar (vertKey);
			}
			else
			{
				scrollNo = 0;
				scrollWait = timeBuffer;
			}
		}

		// Confirm to move to next initial input or confirm submit (A, space, enter/return)
		if (Input.GetKeyDown ("joystick button 0") == true || Input.GetKeyDown ("space") == true || Input.GetKeyDown ("return") == true)
		{
			if (nameIdxLoc != nameLength - 1)
			{
				underlines [nameIdxLoc].SetActive (false);
				nameIdxLoc++;
				underlines [nameIdxLoc].SetActive (true);
				oneShotInit.Play();
			}
			else
			{
				string name = characters [charIdxLoc[0]] + characters [charIdxLoc[1]] + characters [charIdxLoc[2]];
				underlines[2].SetActive (false);
				gameOverCont.SubmitHighscore (name);
			}
		}

		// Cancel to move back an initial input (B, escape, backspace)
		if (Input.GetKeyDown ("joystick button 1") == true || Input.GetKeyDown ("escape") == true || Input.GetKeyDown (KeyCode.Backspace))
		{
			if (nameIdxLoc != 0)
			{
				underlines [nameIdxLoc].SetActive (false);
				nameIdxLoc--;
				underlines [nameIdxLoc].SetActive (true);
				oneShotInit.Play();
			}
		}
	}

	// Vertical scroll
	private void ScrollChar (float value)
	{
		waiting = true;

		if (value > 0) // Select char above
		{
			if (charIdxLoc [nameIdxLoc] == 0) // If current char is first, set last char to be highlighted
			{
				charIdxLoc [nameIdxLoc] = charLenth - 1;
				nameInputs [nameIdxLoc].text = characters [charLenth - 1];
			}
			else
			{
				charIdxLoc [nameIdxLoc] = charIdxLoc [nameIdxLoc] - 1;
				nameInputs [nameIdxLoc].text = characters [charIdxLoc [nameIdxLoc]];
			}
		}
		else if (value < 0) // Select button below
		{
			if (charIdxLoc [nameIdxLoc] == charLenth - 1) // If current char is last, set first char to be highlighted
			{
				charIdxLoc [nameIdxLoc] = 0;
				nameInputs [nameIdxLoc].text = characters [0];
			}
			else
			{
				charIdxLoc [nameIdxLoc] = charIdxLoc [nameIdxLoc] + 1;
				nameInputs [nameIdxLoc].text = characters [charIdxLoc [nameIdxLoc]];
			}
		}

		if (oneShotChar != null)
			oneShotChar.Play();
		scrollNo++;
		StartCoroutine (ScrollWait());
	}

	IEnumerator ScrollWait()
	{
		yield return WaitForUnscaledSeconds (scrollWait);
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
}

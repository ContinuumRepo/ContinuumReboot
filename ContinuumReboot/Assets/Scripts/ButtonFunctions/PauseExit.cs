﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[AddComponentMenu("ButtonEvents/PauseExit")]
public class PauseExit : ButtonEvents
{
	public AudioSource clickSound;
	public AudioSource hoverSound;

	public InputScroll confirmExitScroll;
	public Animator exitAnim;

	public Button thisButton;
	public InputScroll scrollScript;
	public int buttonIndex;

	public override void OnClick()
	{
		if (PlayerPrefs.GetString ("InputMenu") == "exitconfirm")
		{
			PlayerPrefs.SetString ("InputMenu", "gamepause");
			confirmExitScroll.enabled = false;
			scrollScript.enabled = true;
		}
		else
		{
			scrollScript.WaitToRenameInputMenu (0.1f, "exitconfirm");
			confirmExitScroll.enabled = true;
			scrollScript.enabled = false;
		}
		clickSound.Play();
	}

	public override void OnEnter()
	{
		if (scrollScript != null)
			scrollScript.HighlightedButton = buttonIndex;
		exitAnim.enabled = true;
		exitAnim.Play ("PauseHover");
		thisButton.Select();
		hoverSound.Play();
	}

	public override void OnExit()
	{
		exitAnim.Play ("PauseNormal");
	}
}

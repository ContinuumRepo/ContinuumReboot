﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[AddComponentMenu("ButtonEvents/ButtonTemplate")]
public class ButtonTemplate : ButtonEvents
{


	public Button thisButton;
	public InputScroll scrollScript;
	public int buttonIndex;

	public override void OnClick()
	{

	}

	public override void OnEnter()
	{
		if (scrollScript != null)
			scrollScript.HighlightedButton = buttonIndex;

		thisButton.Select();
	}

	public override void OnExit()
	{

	}
}

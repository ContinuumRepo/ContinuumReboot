using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[AddComponentMenu("ButtonEvents/MenuQuit")]
public class MenuQuit : ButtonEvents
{
	public Button thisButton;
	public InputScroll scrollScript;
	public int buttonIndex;
	public MenuButtons menuButtonsScript;

	public override void OnClick()
	{
		PlayerPrefs.SetString ("InputMenu", "quit");
		menuButtonsScript.QuitClick ();
	}

	public override void OnEnter()
	{
		if (scrollScript != null) 
		{
			scrollScript.HighlightedButton = buttonIndex;
			thisButton.Select ();
		}
		menuButtonsScript.QuitEnter ();
	}

	public override void OnExit()
	{
		menuButtonsScript.QuitExit ();
	}
}
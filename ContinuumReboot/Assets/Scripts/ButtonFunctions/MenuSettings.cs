using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[AddComponentMenu("ButtonEvents/MenuSettings")]
public class MenuSettings : ButtonEvents
{
	public Button thisButton;
	public InputScroll scrollScript;
	public int buttonIndex;
	public MenuButtons menuButtonsScript;

	public override void OnClick()
	{
		//PlayerPrefs.SetString ("InputMenu", "settings");
		//menuButtonsScript.SettingsClick ();
	}

	public override void OnEnter()
	{
		if (scrollScript != null) 
		{
			scrollScript.HighlightedButton = buttonIndex;
			thisButton.Select ();
		}
		menuButtonsScript.SettingsEnter ();
	}

	public override void OnExit()
	{
		menuButtonsScript.SettingsExit ();
	}
}
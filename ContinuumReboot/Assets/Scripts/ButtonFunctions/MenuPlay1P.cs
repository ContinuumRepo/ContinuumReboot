using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[AddComponentMenu("ButtonEvents/MenuPlay1P")]
public class MenuPlay1P : ButtonEvents
{
	public Button thisButton;
	public InputScroll scrollScript;
	public int buttonIndex;
	public MenuButtons menuButtonsScript;

	public override void OnClick()
	{
		PlayerPrefs.SetString ("InputMenu", "controls");
		menuButtonsScript.P1Click();
	}

	public override void OnEnter()
	{
		if (scrollScript != null) 
		{
			scrollScript.HighlightedButton = buttonIndex;
			thisButton.Select ();
		}

		menuButtonsScript.P1Enter ();
	}

	public override void OnExit()
	{
		menuButtonsScript.P1Exit ();
	}
}

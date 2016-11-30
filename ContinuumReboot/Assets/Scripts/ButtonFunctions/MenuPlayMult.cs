using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[AddComponentMenu("ButtonEvents/MenuPlayMult")]
public class MenuPlayMult : ButtonEvents
{
	public Button thisButton;
	public InputScroll scrollScript;
	public int buttonIndex;
	public MenuButtons menuButtonsScript;

	public override void OnClick()
	{
		PlayerPrefs.SetString ("InputMenu", "multiplayer");
		menuButtonsScript.P2Click ();
	}

	public override void OnEnter()
	{
		if (scrollScript != null) 
		{
			scrollScript.HighlightedButton = buttonIndex;
			thisButton.Select ();
		}

		menuButtonsScript.P2Enter ();
	}

	public override void OnExit()
	{
		menuButtonsScript.P2Exit ();
	}
}
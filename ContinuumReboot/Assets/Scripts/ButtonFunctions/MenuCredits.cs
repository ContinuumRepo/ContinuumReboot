using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[AddComponentMenu("ButtonEvents/MenuCredits")]
public class MenuCredits : ButtonEvents
{
	public Button thisButton;
	public InputScroll scrollScript;
	public int buttonIndex;
	public MenuButtons menuButtonsScript;

	public override void OnClick()
	{
		//PlayerPrefs.SetString ("InputMenu", "credits");
		//menuButtonsScript.CreditsClick ();
	}

	public override void OnEnter()
	{
		if (scrollScript != null)
		{
			scrollScript.HighlightedButton = buttonIndex;
			thisButton.Select ();
		}

		menuButtonsScript.CreditsEnter ();
	}

	public override void OnExit()
	{
		menuButtonsScript.CreditsExit ();
	}
}
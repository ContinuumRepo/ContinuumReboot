using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[AddComponentMenu("ButtonEvents/MenuLeaderboards")]
public class MenuLeaderboards : ButtonEvents
{
	public Button thisButton;
	public InputScroll scrollScript;
	public int buttonIndex;
	public MenuButtons menuButtonsScript;

	public override void OnClick()
	{
		//PlayerPrefs.SetString ("InputMenu", "leaderboards");
		//menuButtonsScript.LeaderboardsClick ();
	}

	public override void OnEnter()
	{
		if (scrollScript != null)
		{
			scrollScript.HighlightedButton = buttonIndex;
			thisButton.Select ();
		}

		menuButtonsScript.LeaderboardsEnter ();
	}

	public override void OnExit()
	{
		menuButtonsScript.LeaderboardsExit ();
	}
}
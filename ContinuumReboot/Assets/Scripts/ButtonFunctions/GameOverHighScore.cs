using UnityEngine;
using System.Collections;

[AddComponentMenu("ButtonEvents/GameOverHighScore")]
public class GameOverHighScore : ButtonEvents
{


	public InputScroll scrollScript;
	public int buttonIndex;

	public override void OnClick()
	{

	}

	public override void OnEnter()
	{
		if (scrollScript != null)
			scrollScript.HighlightedButton = buttonIndex;

	}

	public override void OnExit()
	{

	}
}

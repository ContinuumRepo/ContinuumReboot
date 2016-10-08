using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[AddComponentMenu("ButtonEvents/PauseExitMenu")]
public class PauseExitMenu : ButtonEvents
{
	public AudioSource clickSound;
	public AudioSource hoverSound;

	public MenuScript menuScript;

	public Button thisButton;
	public InputScroll scrollScript;
	public int buttonIndex;

	public override void OnClick()
	{
		menuScript.LoadScene ("menu");
		clickSound.Play();
	}

	public override void OnEnter()
	{
		if (scrollScript != null)
			scrollScript.HighlightedButton = buttonIndex;
		thisButton.Select();
		hoverSound.Play();
	}

	public override void OnExit()
	{

	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[AddComponentMenu("ButtonEvents/MenuControlsPlay")]
public class MenuControlsPlay : ButtonEvents
{
	public MenuScript menuScript;
	public AudioSource oneShot;

	public Button thisButton;
	public InputScroll scrollScript;
	public int buttonIndex;

	public override void OnClick()
	{
		menuScript.LoadScene ("main");
	}

	public override void OnEnter()
	{
		if (scrollScript != null)
			scrollScript.HighlightedButton = buttonIndex;
		thisButton.Select();

		oneShot.Play();
	}

	public override void OnExit()
	{

	}
}

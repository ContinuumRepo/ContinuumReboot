using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[AddComponentMenu("ButtonEvents/GameOverMenu")]
public class GameOverMenu : ButtonEvents
{
	public AudioSource hoverSound;

	public MenuScript menuScript;
	public Animator menuAnim;

	public Button thisButton;
	public InputScroll scrollScript;
	public int buttonIndex;

	public override void OnClick()
	{
		menuScript.LoadScene ("menu");
	}

	public override void OnEnter()
	{
		if (scrollScript != null)
			scrollScript.HighlightedButton = buttonIndex;
		thisButton.Select();

		menuAnim.Play ("Play2PPointerEnter");
		hoverSound.Play();
	}

	public override void OnExit()
	{
		menuAnim.Play ("Play2PPointerExit");
	}
}

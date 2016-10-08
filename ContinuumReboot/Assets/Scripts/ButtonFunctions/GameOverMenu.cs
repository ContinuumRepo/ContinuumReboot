using UnityEngine;
using System.Collections;

[AddComponentMenu("ButtonEvents/GameOverMenu")]
public class GameOverMenu : ButtonEvents
{
	public AudioSource hoverSound;

	public MenuScript menuScript;
	public Animator menuAnim;

	public GameOverController scrollScript;
	public int buttonIndex;

	public override void OnClick()
	{
		menuScript.LoadScene ("menu");
	}

	public override void OnEnter()
	{
		if (scrollScript != null)
			scrollScript.HighlightedButton = buttonIndex;
		menuAnim.Play ("Play2PPointerEnter");
		hoverSound.Play();
	}

	public override void OnExit()
	{
		menuAnim.Play ("Play2PPointerExit");
	}
}

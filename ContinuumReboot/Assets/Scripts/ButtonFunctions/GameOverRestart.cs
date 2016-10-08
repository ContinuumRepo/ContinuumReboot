using UnityEngine;
using System.Collections;

[AddComponentMenu("ButtonEvents/GameOverRestart")]
public class GameOverRestart : ButtonEvents
{
	public AudioSource hoverSound;

	public MenuScript menuScript;
	public Animator restartAnim;

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
		restartAnim.Play ("Play1PPointerEnter");
		hoverSound.Play();
	}

	public override void OnExit()
	{
		restartAnim.Play ("Play1PPointerExit");
	}
}

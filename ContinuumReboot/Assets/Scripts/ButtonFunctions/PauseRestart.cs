using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[AddComponentMenu("ButtonEvents/PauseRestart")]
public class PauseRestart : ButtonEvents
{
	public AudioSource clickSound;
	public AudioSource hoverSound;

	public MenuScript menuScript;
	public Animator restartAnim;

	public Button thisButton;
	public InputScroll scrollScript;
	public int buttonIndex;

	public override void OnClick()
	{
		PlayerPrefs.SetString ("InputMenu", "");
		menuScript.LoadScene ("main");
		clickSound.Play();
	}

	public override void OnEnter()
	{
		if (scrollScript != null)
			scrollScript.HighlightedButton = buttonIndex;
		restartAnim.enabled = true;
		restartAnim.Play ("PauseHover");
		thisButton.Select();
		hoverSound.Play();
	}

	public override void OnExit()
	{
		restartAnim.Play ("PauseNormal");
	}
}

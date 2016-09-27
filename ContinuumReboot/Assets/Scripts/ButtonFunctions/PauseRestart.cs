using UnityEngine;
using System.Collections;

[AddComponentMenu("ButtonEvents/PauseRestart")]
public class PauseRestart : ButtonEvents
{
	public AudioSource clickSound;
	public AudioSource hoverSound;

	public MenuScript menuScript;
	public Animator restartAnim;

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
		restartAnim.Play ("Play2PPointerEnter");
		hoverSound.Play();
	}

	public override void OnExit()
	{
		restartAnim.Play ("Play2PPointerExit");
	}
}

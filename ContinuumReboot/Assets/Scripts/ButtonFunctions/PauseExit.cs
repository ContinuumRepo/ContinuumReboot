using UnityEngine;
using System.Collections;

[AddComponentMenu("ButtonEvents/PauseExit")]
public class PauseExit : ButtonEvents
{
	public AudioSource clickSound;
	public AudioSource hoverSound;

	public GameObject confirmExit;
	public Animator exitAnim;

	public InputScroll scrollScript;
	public int buttonIndex;

	public override void OnClick()
	{
		PlayerPrefs.SetString ("InputMenu", "exitConfirm");
		confirmExit.SetActive (true);
		clickSound.Play();
	}

	public override void OnEnter()
	{
		if (scrollScript != null)
			scrollScript.HighlightedButton = buttonIndex;
		exitAnim.enabled = true;
		exitAnim.Play ("Play2PPointerEnter");
		hoverSound.Play();
	}

	public override void OnExit()
	{
		exitAnim.Play ("Play2PPointerExit");
	}
}

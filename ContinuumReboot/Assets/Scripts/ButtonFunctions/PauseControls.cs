using UnityEngine;
using System.Collections;

[AddComponentMenu("ButtonEvents/PauseControls")]
public class PauseControls : ButtonEvents
{
	public AudioSource clickSound;
	public AudioSource hoverSound;

	public GameObject controlsPage;
	public Animator controlsAnim;

	public InputScroll scrollScript;
	public int buttonIndex;

	public override void OnClick()
	{
		PlayerPrefs.SetString ("InputMenu", "controls");
		controlsPage.SetActive (true);
		clickSound.Play();
	}

	public override void OnEnter()
	{
		if (scrollScript != null)
			scrollScript.HighlightedButton = buttonIndex;
		controlsAnim.enabled = true;
		controlsAnim.Play ("Play2PPointerEnter");
		hoverSound.Play();
	}

	public override void OnExit()
	{
		controlsAnim.Play ("Play2PPointerExit");
	}
}

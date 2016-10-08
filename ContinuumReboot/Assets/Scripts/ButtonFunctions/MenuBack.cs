using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[AddComponentMenu("ButtonEvents/MenuBack")]
public class MenuBack : ButtonEvents
{
	public GameObject currentMenu;
	public AudioLowPassFilter bgMenuMusicLow;
	public AudioHighPassFilter bgMenuMusicHigh;
	public GameObject title;
	public AudioSource backAudio;

	public AudioSource oneShot;
	public Animator backAnimator;
	public float renameWait;
	public InputScroll backToScrollScript;

	public Button thisButton;
	public InputScroll scrollScript;
	public int buttonIndex;

	public override void OnClick()
	{
		backToScrollScript.WaitToRenameInputMenu (renameWait, "mainmenu");
		bgMenuMusicLow.enabled = false;
		bgMenuMusicHigh.enabled = false;
		title.SetActive (true);
		backAudio.Play();
		currentMenu.SetActive (false);
	}

	public override void OnEnter()
	{
		if (scrollScript != null)
			scrollScript.HighlightedButton = buttonIndex;
		thisButton.Select();

		oneShot.Play();
		backAnimator.enabled = true;
		backAnimator.Play ("BackButtonOnPointerEnter");
	}

	public override void OnExit()
	{
		backAnimator.Play ("BackButtonOnPointerExit");
	}
}

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

	public GameObject Menus;
	public AudioSource MenuMusic;
	public AudioSource CreditsMusic;

	public override void OnClick()
	{
		backToScrollScript.WaitToRenameInputMenu (renameWait, "mainmenu");
		bgMenuMusicLow.enabled = false;
		bgMenuMusicHigh.enabled = false;
		title.SetActive (true);
		backAudio.Play();
		//GameObject.Find ("UI").GetComponent<InputScroll> ().enabled = false;
		Menus.SetActive (false);
		currentMenu.SetActive (false);
		backAnimator.Play ("MainToStart");
		MenuMusic.UnPause ();
		CreditsMusic.Pause ();
	}

	public override void OnEnter()
	{
		if (scrollScript != null)
			scrollScript.HighlightedButton = buttonIndex;
		thisButton.Select();

		oneShot.Play();
		backAnimator.enabled = true;
		//backAnimator.Play ("BackButtonOnPointerEnter");
	}

	public override void OnExit()
	{
		//backAnimator.Play ("BackButtonOnPointerExit");
	}
}

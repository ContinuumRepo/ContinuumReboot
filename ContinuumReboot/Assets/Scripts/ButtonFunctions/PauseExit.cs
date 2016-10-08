using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[AddComponentMenu("ButtonEvents/PauseExit")]
public class PauseExit : ButtonEvents
{
	public AudioSource clickSound;
	public AudioSource hoverSound;

	public GameObject confirmExit;
	public Animator exitAnim;

	public Button thisButton;
	public InputScroll scrollScript;
	public int buttonIndex;

	public override void OnClick()
	{
		if (PlayerPrefs.GetString ("InputMenu") == "exitconfirm")
		{
			PlayerPrefs.SetString ("InputMenu", "gamepause");
			confirmExit.SetActive (false);
		}
		else
		{
			PlayerPrefs.SetString ("InputMenu", "exitconfirm");
			confirmExit.SetActive (true);
		}
		clickSound.Play();
	}

	public override void OnEnter()
	{
		if (scrollScript != null)
			scrollScript.HighlightedButton = buttonIndex;
		exitAnim.enabled = true;
		exitAnim.Play ("Play2PPointerEnter");
		thisButton.Select();
		hoverSound.Play();
	}

	public override void OnExit()
	{
		exitAnim.Play ("Play2PPointerExit");
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[AddComponentMenu("ButtonEvents/PauseExitExit")]
public class PauseExitExit : ButtonEvents
{
	public AudioSource clickSound;
	public AudioSource hoverSound;

	public Button thisButton;
	public InputScroll scrollScript;
	public int buttonIndex;

	void Start ()
	{
		thisButton = this.gameObject.GetComponent <Button>();
	}

	public override void OnClick()
	{
		clickSound.Play();
		Application.Quit ();
	}

	public override void OnEnter()
	{
		if (scrollScript != null)
			scrollScript.HighlightedButton = buttonIndex;
		thisButton.Select();
		hoverSound.Play();
	}

	public override void OnExit()
	{
		
	}
}

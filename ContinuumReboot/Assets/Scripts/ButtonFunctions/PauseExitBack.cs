using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[AddComponentMenu("ButtonEvents/PauseExitBack")]
public class PauseExitBack : ButtonEvents
{
	public AudioSource clickSound;

	public Button exitButton;
	public InputScroll confirmExitScroll;
	public InputScroll backScrollScript;

	public override void OnClick()
	{
		backScrollScript.WaitToRenameInputMenu (0.1f, "gamepause");
		exitButton.Select();
		confirmExitScroll.enabled = false;
		clickSound.Play();
	}

	public override void OnEnter()
	{
		
	}

	public override void OnExit()
	{

	}
}

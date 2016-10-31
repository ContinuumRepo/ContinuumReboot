using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[AddComponentMenu("ButtonEvents/PauseExitBack")]
public class PauseExitBack : ButtonEvents
{
	public AudioSource clickSound;

	public ButtonEvents exitButton;
	public InputScroll confirmExitScroll;
	public InputScroll backScrollScript;

	public override void OnClick()
	{
		confirmExitScroll.enabled = false;
		backScrollScript.enabled = true;
		backScrollScript.WaitToRenameInputMenu (0.1f, "gamepause");
		exitButton.OnEnter();
		clickSound.Play();
	}

	public override void OnEnter()
	{
		
	}

	public override void OnExit()
	{

	}
}

using UnityEngine;
using System.Collections;

[AddComponentMenu("ButtonEvents/PauseExitBack")]
public class PauseExitBack : ButtonEvents
{
	public AudioSource clickSound;

	public GameObject confirmExit;

	public override void OnClick()
	{
		PlayerPrefs.SetString ("InputMenu", "gamepause");
		confirmExit.SetActive (false);
		clickSound.Play();
	}

	public override void OnEnter()
	{
		
	}

	public override void OnExit()
	{

	}
}

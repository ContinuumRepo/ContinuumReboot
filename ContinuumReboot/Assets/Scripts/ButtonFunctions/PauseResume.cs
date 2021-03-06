﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[AddComponentMenu("ButtonEvents/PauseResume")]
public class PauseResume : ButtonEvents
{
	public AudioSource clickSound;
	public AudioSource hoverSound;
	public AudioSource bgmSource;
	public float bgmVolume;
	public AudioSourcePitchByTimescale bgmPitch;

	public GameObject pauseUI;
	public TimescaleController timeCont;
	public GlobalMouseVisibility mouseVis;
	public GameController gameCont;
	public Animator resumeAnim;

	public Button thisButton;
	public InputScroll scrollScript;
	public int buttonIndex;

	public override void OnClick()
	{
		PlayerPrefs.SetString ("InputMenu", "");
		pauseUI.SetActive (false);
		clickSound.Play();
		//bgmPitch.enabled = true;
		bgmSource.volume = bgmVolume;
		timeCont.enabled = true;
		timeCont.Start();
		mouseVis.enabled = true;
		gameCont.UnPauseGame();
		resumeAnim.enabled = false;
	}

	public override void OnEnter()
	{
		if (scrollScript != null)
			scrollScript.HighlightedButton = buttonIndex;
		thisButton.Select();
		resumeAnim.enabled = true;
		resumeAnim.Play ("PauseHover");
		thisButton.Select();
		hoverSound.Play();
	}

	public override void OnExit()
	{
		resumeAnim.Play ("PauseNormal");
	}
}

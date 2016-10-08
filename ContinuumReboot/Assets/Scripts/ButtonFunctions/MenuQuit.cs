﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[AddComponentMenu("ButtonEvents/MenuQuit")]
public class MenuQuit : ButtonEvents
{
	public AudioSource oneShot;
	public AudioLowPassFilter bgMenuMusicLow;
	public AudioHighPassFilter bgMenuMusicHigh;

	public GameObject controls;
	public Material playerMaterial;

	public ParticleSystem menuParticleShort;

	public Button thisButton;
	public InputScroll scrollScript;
	public int buttonIndex;

	[Header("Menu Player 1")]
	public MeshRenderer menuPlayer1;
	public ParticleSystem engineLnone;
	public ParticleSystem engineRnone;
	public ParticleSystem engineLred;
	public ParticleSystem engineRred;

	[Header("Quit Button")]
	public GameObject quit;
	public Animator quitAnimator;
	public GameObject quitMesh;
	public Material quitMaterial;

	public override void OnClick()
	{
		PlayerPrefs.SetString ("InputMenu", "quit");
		quit.SetActive (true);
		bgMenuMusicLow.enabled = true;
		bgMenuMusicHigh.enabled = true;
		oneShot.Play();
	}

	public override void OnEnter()
	{
		if (scrollScript != null)
			scrollScript.HighlightedButton = buttonIndex;
		thisButton.Select();
		quitAnimator.enabled = true;
		quitAnimator.Play ("QuitPointerEnter");
		oneShot.Play();
		quitMesh.SetActive (true);
		menuPlayer1.material = quitMaterial;
		engineLred.Play();
		engineRred.Play();
		engineLnone.Stop();
		engineRnone.Stop();
	}

	public override void OnExit()
	{
		quitAnimator.Play ("QuitPointerExit");
		quitMesh.SetActive (false);
		menuPlayer1.material = playerMaterial;
		engineLred.Stop();
		engineRred.Stop();
	}
}
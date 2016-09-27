using UnityEngine;
using System.Collections;

[AddComponentMenu("ButtonEvents/MenuSettings")]
public class MenuSettings : ButtonEvents
{
	public AudioSource oneShot;
	public AudioLowPassFilter bgMenuMusicLow;

	public GameObject controls;
	public Material playerMaterial;

	public ParticleSystem menuParticleShort;

	private InputScrollMenu scrollScript;

	[Header("Menu Player 1")]
	public MeshRenderer menuPlayer1;
	public ParticleSystem engineLnone;
	public ParticleSystem engineRnone;
	public ParticleSystem engineLpurple;
	public ParticleSystem engineRpurple;

	[Header("Settings Button")]
	public GameObject settings;
	public Animator settingsAnimator;
	public GameObject settingsMesh;
	public Material settingsMaterial;

	public override void OnClick()
	{
		PlayerPrefs.SetString ("Menu", "settings");
		settings.SetActive (true);
		bgMenuMusicLow.enabled = true;
		oneShot.Play();
	}

	public override void OnEnter()
	{
		if (scrollScript != null)
			scrollScript.HighlightedButton = 3;
		settingsAnimator.enabled = true;
		settingsAnimator.Play ("CreditsPointerEnter");
		oneShot.Play();
		settingsMesh.SetActive (true);
		menuPlayer1.material = settingsMaterial;
		engineLpurple.Play();
		engineRpurple.Play();
		engineLnone.Stop();
		engineRnone.Stop();
	}

	public override void OnExit()
	{
		settingsAnimator.Play ("CreditsPointerExit");
		settingsMesh.SetActive (false);
		menuPlayer1.material = playerMaterial;
		engineLpurple.Stop();
		engineRpurple.Stop();
	}
}
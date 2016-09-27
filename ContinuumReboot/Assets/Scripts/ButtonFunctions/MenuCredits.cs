using UnityEngine;
using System.Collections;

[AddComponentMenu("ButtonEvents/MenuCredits")]
public class MenuCredits : ButtonEvents
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
	public ParticleSystem engineLpink;
	public ParticleSystem engineRpink;

	[Header("Credits Button")]
	public GameObject credits;
	public Animator creditsAnimator;
	public GameObject creditsMesh;
	public Material creditsMaterial;
	public AudioSource creditsAudio;
	public AudioSource backgroundMenuAudio;

	public override void OnClick()
	{
		PlayerPrefs.SetString ("Menu", "credits");
		credits.SetActive (true);
		bgMenuMusicLow.enabled = true;
		oneShot.Play();
		backgroundMenuAudio.Pause();
		creditsAudio.Play();
	}

	public override void OnEnter()
	{
		if (scrollScript != null)
			scrollScript.HighlightedButton = 4;
		creditsAnimator.enabled = true;
		creditsAnimator.Play ("CreditsPointerEnter");
		oneShot.Play();
		creditsMesh.SetActive (true);
		menuPlayer1.material = creditsMaterial;
		engineLpink.Play();
		engineRpink.Play();
		engineLnone.Stop();
		engineRnone.Stop();
	}

	public override void OnExit()
	{
		creditsAnimator.Play ("CreditsPointerExit");
		creditsMesh.SetActive (false);
		menuPlayer1.material = playerMaterial;
		engineLpink.Stop();
		engineRpink.Stop();
	}
}
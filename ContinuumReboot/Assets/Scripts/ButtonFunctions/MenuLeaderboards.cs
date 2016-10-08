using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[AddComponentMenu("ButtonEvents/MenuLeaderboards")]
public class MenuLeaderboards : ButtonEvents
{
	public AudioSource oneShot;
	public AudioLowPassFilter bgMenuMusicLow;

	public Material playerMaterial;
	public ParticleSystem menuParticleShort;

	public Button thisButton;
	public InputScroll scrollScript;
	public int buttonIndex;

	[Header("Menu Player 1")]
	public MeshRenderer menuPlayer1;
	public ParticleSystem engineLnone;
	public ParticleSystem engineRnone;
	public ParticleSystem engineLblue;
	public ParticleSystem engineRblue;

	[Header("View Leaderboards Button")]
	public GameObject leaderboards;
	public Animator leadAnimator;
	public GameObject leadMesh;
	public Material leadMaterial;

	public override void OnClick()
	{
		PlayerPrefs.SetString ("InputMenu", "leaderboards");
		leaderboards.SetActive (true);
		bgMenuMusicLow.enabled = true;
		oneShot.Play();
	}

	public override void OnEnter()
	{
		if (scrollScript != null)
			scrollScript.HighlightedButton = buttonIndex;
		thisButton.Select();
		leadAnimator.enabled = true;
		leadAnimator.Play ("LeaderboardsPointerEnter");
		oneShot.Play();
		leadMesh.SetActive (true);
		menuPlayer1.material = leadMaterial;
		engineLblue.Play();
		engineRblue.Play();
		engineLnone.Stop();
		engineRnone.Stop();
	}

	public override void OnExit()
	{
		leadAnimator.Play ("LeaderboardsPointerExit");
		leadMesh.SetActive (false);
		menuPlayer1.material = playerMaterial;
		engineLblue.Stop();
		engineRblue.Stop();
	}
}
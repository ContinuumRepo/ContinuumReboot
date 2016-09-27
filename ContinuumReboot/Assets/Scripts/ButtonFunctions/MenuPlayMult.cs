using UnityEngine;
using System.Collections;

[AddComponentMenu("ButtonEvents/MenuPlayMult")]
public class MenuPlayMult : ButtonEvents
{
	public AudioSource oneShot;
	public AudioLowPassFilter bgMenuMusicLow;

	public GameObject controls;
	public Material playerMaterial;

	public ParticleSystem menuParticleShort;

	private InputScroll scrollScript;

	[Header("Menu Player 1")]
	public MeshRenderer menuPlayer1;
	public ParticleSystem engineLnone;
	public ParticleSystem engineRnone;
	public ParticleSystem engineLorange;
	public ParticleSystem engineRorange;

	[Header("Play 2P Button")]
	public GameObject ComingSoonText;
	public GameObject multiplayer;
	public Animator p2Animator;
	public GameObject p2Mesh;
	public Material p2Material;
	public float p2ParticleShortSpeedEnter;
	public GameObject menuPlayer2;
	public SmoothFollowOrig smoothFollowOrig2;
	public SmoothFollowClone smoothFollowClone2;
	public AudioSource p2AudioIn;
	public AudioSource p2AudioOut;

	public float p3ParticleShortSpeedEnter;
	public GameObject menuPlayer3;
	public SmoothFollowOrig smoothFollowOrig3;
	public SmoothFollowClone smoothFollowClone3;

	public float p4ParticleShortSpeedEnter;
	public GameObject menuPlayer4;
	public SmoothFollowOrig smoothFollowOrig4;
	public SmoothFollowClone smoothFollowClone4;

	public override void OnClick()
	{
		PlayerPrefs.SetString ("Menu", "multiplayer");
		multiplayer.SetActive (true);
		bgMenuMusicLow.enabled = true;
		oneShot.Play();
	}

	public override void OnEnter()
	{
		if (scrollScript != null)
			scrollScript.HighlightedButton = 1;
		ComingSoonText.SetActive (true);
		p2Animator.enabled = true;
		p2Animator.Play ("Play2PPointerEnter");
		oneShot.Play();
		p2Mesh.SetActive (true);
		menuPlayer1.material = p2Material;
		engineLorange.Play();
		engineRorange.Play();
		engineLnone.Stop();
		engineRnone.Stop();
		smoothFollowOrig2.enabled = true;
		smoothFollowClone2.enabled = false;
		menuPlayer2.SetActive (true);
		p2AudioIn.Play();
		menuParticleShort.startSpeed = p2ParticleShortSpeedEnter;

		smoothFollowOrig3.enabled = true;
		smoothFollowClone3.enabled = false;
		menuPlayer3.SetActive (true);
		menuParticleShort.startSpeed = p3ParticleShortSpeedEnter;

		smoothFollowOrig4.enabled = true;
		smoothFollowClone4.enabled = false;
		menuPlayer4.SetActive (true);
		menuParticleShort.startSpeed = p4ParticleShortSpeedEnter;
	}

	public override void OnExit()
	{
		p2Animator.Play ("Play2PPointerExit");
		p2Mesh.SetActive (false);
		menuPlayer1.material = playerMaterial;
		engineLorange.Stop();
		engineRorange.Stop();
		smoothFollowClone2.enabled = true;
		smoothFollowClone3.enabled = true;
		smoothFollowClone4.enabled = true;
		smoothFollowOrig2.enabled = false;
		smoothFollowOrig3.enabled = false;
		smoothFollowOrig4.enabled = false;
		ComingSoonText.SetActive (false);
		p2AudioOut.Play();
	}
}
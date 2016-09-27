using UnityEngine;
using System.Collections;

[AddComponentMenu("ButtonEvents/MenuPlay1P")]
public class MenuPlay1P : ButtonEvents
{
	public AudioSource oneShot;
	public AudioLowPassFilter bgMenuMusicLow;

	public GameObject controls;
	public Material playerMaterial;
	public ParticleSystem menuParticleShort;

	public InputScroll scrollScript;
	public int buttonIndex;

	[Header("Menu Player 1")]
	public MeshRenderer menuPlayer1;
	public ParticleSystem engineLnone;
	public ParticleSystem engineRnone;
	public ParticleSystem engineLcyan;
	public ParticleSystem engineRcyan;

	[Header("Play 1P Button")]
	public Animator p1Animator;
	public GameObject p1Mesh;
	public Material p1Material;
	public float p1ParticleShortSpeedEnter;
	public float p1ParticleShortSpeedExit;

	public override void OnClick()
	{
		PlayerPrefs.SetString ("InputMenu", "controls");
		controls.SetActive (true);
		bgMenuMusicLow.enabled = true;
		oneShot.Play();
	}

	public override void OnEnter()
	{
		if (scrollScript != null)
			scrollScript.HighlightedButton = buttonIndex;
		p1Animator.enabled = true;
		p1Animator.Play ("Play1PPointerEnter");
		oneShot.Play();
		p1Mesh.SetActive (true);
		menuPlayer1.material = p1Material;
		engineLcyan.Play();
		engineRcyan.Play();
		engineLnone.Stop();
		engineRnone.Stop();
		menuParticleShort.startSpeed = p1ParticleShortSpeedEnter;
		//general brick prefab change material? what does this do?
	}

	public override void OnExit()
	{
		p1Animator.Play ("Play1PPointerExit");
		p1Mesh.SetActive (false);
		menuPlayer1.material = playerMaterial;
		engineLcyan.Stop();
		engineRcyan.Stop();
		menuParticleShort.startSpeed = p1ParticleShortSpeedExit;
		//general brick prefab change material? what does this do?
	}
}

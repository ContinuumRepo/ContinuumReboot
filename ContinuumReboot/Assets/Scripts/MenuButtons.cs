using UnityEngine;
using System.Collections;

public class MenuButtons : MonoBehaviour
{
	public AudioSource oneShot;
	public AudioLowPassFilter bgMenuMusicLow;
	public AudioHighPassFilter bgMenuMusicHigh;

	public GameObject controls;
	public Material playerMaterial;

	public ParticleSystem menuParticleShort;
	public ParticleSystem menuParticleLong;

	[Header("Menu Player 1")]
	public MeshRenderer menuPlayer1;
	public ParticleSystem engineLnone;
	public ParticleSystem engineRnone;
	public ParticleSystem engineLorange;
	public ParticleSystem engineRorange;
	public ParticleSystem engineLcyan;
	public ParticleSystem engineRcyan;
	public ParticleSystem engineLpurple;
	public ParticleSystem engineRpurple;
	public ParticleSystem engineLred;
	public ParticleSystem engineRred;
	public ParticleSystem engineLblue;
	public ParticleSystem engineRblue;
	public ParticleSystem engineLpink;
	public ParticleSystem engineRpink;

	[Header("Play 1P Button")]
	public Animator p1Animator;
	public GameObject p1Mesh;
	public Material p1Material;
	public float p1ParticleShortSpeedEnter;
	public float p1ParticleShortSpeedExit;

	[Header("Play 2P Button")]
	public Animator p2Animator;
	public GameObject p2Mesh;
	public Material p2Material;
	public float p2ParticleShortSpeedEnter;
	public float p2ParticleShortSpeedExit;
	public GameObject menuPlayer2;
	public SmoothFollowOrig smoothFollowOrig;
	public SmoothFollowClone smoothFollowClone;
	public AudioSource p2AudioIn;
	public AudioSource p2AudioOut;

	[Header("View Leaderboards Button")]
	public GameObject leaderboards;
	public Animator leadAnimator;
	public GameObject leadMesh;
	public Material leadMaterial;

	[Header("Settings Button")]
	public GameObject settings;
	public Animator settingsAnimator;
	public GameObject settingsMesh;
	public Material settingsMaterial;

	[Header("Credits Button")]
	public GameObject credits;
	public Animator creditsAnimator;
	public GameObject creditsMesh;
	public Material creditsMaterial;
	public AudioSource creditsAudio;
	public AudioSource backgroundMenuAudio;

	[Header("Quit Button")]
	public GameObject quit;
	public Animator quitAnimator;
	public GameObject quitMesh;
	public Material quitMaterial;

	#region Play 1P Button Functions
	public void P1Click()
	{
		controls.SetActive (true);
		bgMenuMusicLow.enabled = true;
		oneShot.Play();
	}

	public void P1Enter()
	{
		p1Animator.enabled = true;
		p1Animator.Play ("Play1PPointerEnter");
		p1Mesh.SetActive (true);
		menuPlayer1.material = p1Material;
		engineLcyan.Play();
		engineRcyan.Play();
		engineLnone.Stop();
		engineRnone.Stop();
		menuParticleShort.startSpeed = p1ParticleShortSpeedEnter;
		//general brick prefab change material? what does this do?
	}

	public void P1Exit()
	{
		p1Animator.Play ("Play1PPointerExit");
		p1Mesh.SetActive (false);
		menuPlayer1.material = playerMaterial;
		engineLcyan.Stop();
		engineRcyan.Stop();
		menuParticleShort.startSpeed = p1ParticleShortSpeedExit;
		//general brick prefab change material? what does this do?
	}
	#endregion

	#region Play 2P Button Functions
	public void P2Click()
	{
		controls.SetActive (true);
		bgMenuMusicLow.enabled = true;
		oneShot.Play();
	}

	public void P2Enter()
	{
		p2Animator.enabled = true;
		p2Animator.Play ("Play2PPointerEnter");
		oneShot.Play();
		p2Mesh.SetActive (true);
		menuPlayer1.material = p2Material;
		engineLorange.Play();
		engineRorange.Play();
		engineLnone.Stop();
		engineRnone.Stop();
		smoothFollowOrig.enabled = true;
		smoothFollowClone.enabled = false;
		menuPlayer2.SetActive (true);
		p2AudioIn.Play();
		menuParticleShort.startSpeed = p2ParticleShortSpeedEnter;
	}

	public void P2Exit()
	{
		p2Animator.Play ("Play2PPointerExit");
		p2Mesh.SetActive (false);
		menuPlayer1.material = playerMaterial;
		engineLorange.Stop();
		engineRorange.Stop();
		smoothFollowClone.enabled = true;
		p2AudioOut.Play();
	}
	#endregion

	#region View Leaderboards Button Functions
	public void LeaderboardsClick()
	{
		leaderboards.SetActive (true);
		bgMenuMusicLow.enabled = true;
		oneShot.Play();
	}

	public void LeaderboardsEnter()
	{
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

	public void LeaderboardsExit()
	{
		leadAnimator.Play ("LeaderboardsPointerExit");
		leadMesh.SetActive (false);
		menuPlayer1.material = playerMaterial;
		engineLblue.Stop();
		engineRblue.Stop();
	}
	#endregion

	#region Settings Button Functions
	public void SettingsClick()
	{
		settings.SetActive (true);
		bgMenuMusicLow.enabled = true;
		oneShot.Play();
	}

	public void SettingsEnter()
	{
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

	public void SettingsExit()
	{
		settingsAnimator.Play ("CreditsPointerExit");
		settingsMesh.SetActive (false);
		menuPlayer1.material = playerMaterial;
		engineLpurple.Stop();
		engineRpurple.Stop();
	}
	#endregion

	#region Credits Button Functions
	public void CreditsClick()
	{
		credits.SetActive (true);
		bgMenuMusicLow.enabled = true;
		oneShot.Play();
		backgroundMenuAudio.Pause();
		creditsAudio.Play();
	}

	public void CreditsEnter()
	{
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

	public void CreditsExit()
	{
		creditsAnimator.Play ("CreditsPointerExit");
		creditsMesh.SetActive (false);
		menuPlayer1.material = playerMaterial;
		engineLpink.Stop();
		engineRpink.Stop();
	}
	#endregion

	#region Quit Button Functions
	public void QuitClick()
	{
		quit.SetActive (true);
		bgMenuMusicLow.enabled = true;
		bgMenuMusicHigh.enabled = true;
		oneShot.Play();
	}

	public void QuitEnter()
	{
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

	public void QuitExit()
	{
		quitAnimator.Play ("CreditsPointerExit");
		quitMesh.SetActive (false);
		menuPlayer1.material = playerMaterial;
		engineLred.Stop();
		engineRred.Stop();
	}
	#endregion
}

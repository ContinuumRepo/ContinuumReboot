/// <summary>
/// 
/// CURRENTLY UNUSED
/// 
/// To find the event handlers for the menu buttons,
/// the ButtonFunctions folder holds each individual
/// script for each button.
/// 
/// </summary>

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
	public AudioSource uiConfirm;
	public AudioSource uiHover;
	public AudioLowPassFilter bgMenuMusicLow;
	public AudioHighPassFilter bgMenuMusicHigh;

	public GameObject ParentMenu;

	[Header("Play 1P Button")]
	public GameObject controls;
	public Animator p1Animator;
	public GameObject LoadingArcadeMode;

	[Header("Play 2P Button")]
	public GameObject multiplayer;
	public Animator p2Animator;
	public GameObject menuPlayer2;
	public SmoothFollowOrig smoothFollowOrig2;
	public SmoothFollowClone smoothFollowClone2;
	public AudioSource p2AudioIn;
	public AudioSource p2AudioOut;

	[Header("View Leaderboards Button")]
	public GameObject leaderboards;
	public Animator leadAnimator;

	[Header("Settings Button")]
	public GameObject settings;
	public Animator settingsAnimator;

	[Header("Credits Button")]
	public GameObject credits;
	public Animator creditsAnimator;
	public AudioSource creditsAudio;
	public AudioSource backgroundMenuAudio;

	[Header("Quit Button")]
	public GameObject quit;
	public Animator quitAnimator;

	private InputScroll scrollScript;

	void Start()
	{
		scrollScript = GetComponent<InputScroll> ();
	}

	#region Play 1P Button Functions
	public void P1Click()
	{
		uiConfirm.Play();
		LoadingArcadeMode.SetActive (true);
		ParentMenu.SetActive (false);
		Camera.main.GetComponent<Animator> ().Play ("MainToSelect");
		DisableAllMenus ();
	}

	public void P1Enter()
	{
		if (scrollScript != null) 
		{
			scrollScript.HighlightedButton = 0;
		}

		p1Animator.enabled = true;
		p1Animator.Play ("MainButtonHoverEnter");
		uiHover.Play();

		PlayerPrefs.SetString ("Menu", "controls");
		controls.SetActive (true);
		bgMenuMusicLow.enabled = true;

		backgroundMenuAudio.UnPause();
		creditsAudio.Pause();

		multiplayer.SetActive (false);
		leaderboards.SetActive (false);
		settings.SetActive (false);
		credits.SetActive (false);
		quit.SetActive (false);
	}

	public void P1Exit()
	{
		p1Animator.Play ("MainButtonHoverExit");

		// Disables all other menus.
		controls.SetActive (false);
		multiplayer.SetActive (false);
		leaderboards.SetActive (false);
		settings.SetActive (false);
		credits.SetActive (false);
		quit.SetActive (false);
	}
	#endregion

	#region Play 2P Button Functions
	public void P2Click()
	{
		uiConfirm.Play();
		ParentMenu.SetActive (false);
		Camera.main.GetComponent<Animator> ().Play ("MainToSelect");
		DisableAllMenus ();
	}

	public void P2Enter()
	{
		if (scrollScript != null) 
		{
			scrollScript.HighlightedButton = 1;
		}
			
		p2Animator.enabled = true;
		p2Animator.Play ("MainButtonHoverEnter");
		uiHover.Play();
		smoothFollowOrig2.enabled = true;
		smoothFollowClone2.enabled = false;
		menuPlayer2.SetActive (true);
		p2AudioIn.Play();

		PlayerPrefs.SetString ("Menu", "multiplayer");
		multiplayer.SetActive (true);
		bgMenuMusicLow.enabled = true;

		backgroundMenuAudio.UnPause();
		creditsAudio.Pause();

		controls.SetActive (false);
		leaderboards.SetActive (false);
		settings.SetActive (false);
		credits.SetActive (false);
		quit.SetActive (false);
	}

	public void P2Exit()
	{
		p2Animator.Play ("MainButtonHoverExit");
		smoothFollowClone2.enabled = true;
		smoothFollowOrig2.enabled = false;
		p2AudioOut.Play();

		// Disables all other menus.
		controls.SetActive (false);
		multiplayer.SetActive (false);
		leaderboards.SetActive (false);
		settings.SetActive (false);
		credits.SetActive (false);
		quit.SetActive (false);
	}
	#endregion

	#region View Leaderboards Button Functions
	public void LeaderboardsClick()
	{
		uiConfirm.Play();
		//DisableAllMenus ();
	}

	public void LeaderboardsEnter()
	{
		if (scrollScript != null) 
		{
			scrollScript.HighlightedButton = 2;
		}

		leadAnimator.enabled = true;
		leadAnimator.Play ("MainButtonHoverEnter");
		uiHover.Play();

		PlayerPrefs.SetString ("Menu", "leaderboards");
		leaderboards.SetActive (true);
		bgMenuMusicLow.enabled = true;

		backgroundMenuAudio.UnPause();
		creditsAudio.Pause();

		controls.SetActive (false);
		multiplayer.SetActive (false);
		settings.SetActive (false);
		credits.SetActive (false);
		quit.SetActive (false);
	}

	public void LeaderboardsExit()
	{
		leadAnimator.Play ("MainButtonHoverExit");

		// Disables all other menus.
		controls.SetActive (false);
		multiplayer.SetActive (false);
		leaderboards.SetActive (false);
		settings.SetActive (false);
		credits.SetActive (false);
		quit.SetActive (false);
	}
	#endregion

	#region Settings Button Functions
	public void SettingsClick()
	{
		uiConfirm.Play();
		//DisableAllMenus ();
	}

	public void SettingsEnter()
	{
		if (scrollScript != null) 
		{
			scrollScript.HighlightedButton = 3;
		}

		settingsAnimator.enabled = true;
		settingsAnimator.Play ("MainButtonHoverEnter");
		uiHover.Play();

		PlayerPrefs.SetString ("Menu", "settings");
		settings.SetActive (true);
		bgMenuMusicLow.enabled = true;

		backgroundMenuAudio.UnPause();
		creditsAudio.Pause();

		controls.SetActive (false);
		multiplayer.SetActive (false);
		leaderboards.SetActive (false);
		credits.SetActive (false);
		quit.SetActive (false);
	}

	public void SettingsExit()
	{
		settingsAnimator.Play ("MainButtonHoverExit");

		// Disables all other menus.
		controls.SetActive (false);
		multiplayer.SetActive (false);
		leaderboards.SetActive (false);
		settings.SetActive (false);
		credits.SetActive (false);
		quit.SetActive (false);
	}
	#endregion

	#region Credits Button Functions
	public void CreditsClick()
	{
		uiConfirm.Play();
		//DisableAllMenus ();
	}

	public void CreditsEnter()
	{
		if (scrollScript != null) 
		{
			scrollScript.HighlightedButton = 4;
		}

		creditsAnimator.enabled = true;
		creditsAnimator.Play ("MainButtonHoverEnter");
		uiHover.Play();

		PlayerPrefs.SetString ("Menu", "credits");
		credits.SetActive (true);
		bgMenuMusicLow.enabled = true;

		backgroundMenuAudio.Pause();
		creditsAudio.Play();

		controls.SetActive (false);
		multiplayer.SetActive (false);
		settings.SetActive (false);
		leaderboards.SetActive (false);
		quit.SetActive (false);
	}

	public void CreditsExit()
	{
		creditsAnimator.Play ("MainButtonHoverExit");

		// Disables all other menus.
		controls.SetActive (false);
		multiplayer.SetActive (false);
		leaderboards.SetActive (false);
		settings.SetActive (false);
		credits.SetActive (false);
		quit.SetActive (false);
	}
	#endregion

	#region Quit Button Functions
	public void QuitClick()
	{
		uiConfirm.Play();
		Debug.Log ("Tried to quit.");
		Application.Quit ();
		//DisableAllMenus ();
	}

	public void QuitEnter()
	{
		if (scrollScript != null) 
		{
			scrollScript.HighlightedButton = 5;
		}

		quitAnimator.enabled = true;
		quitAnimator.Play ("MainButtonHoverEnter");
		uiHover.Play();

		PlayerPrefs.SetString ("Menu", "quit");
		quit.SetActive (true);
		bgMenuMusicLow.enabled = true;
		bgMenuMusicHigh.enabled = true;

		backgroundMenuAudio.UnPause();
		creditsAudio.Pause();

		controls.SetActive (false);
		multiplayer.SetActive (false);
		leaderboards.SetActive (false);
		settings.SetActive (false);
		credits.SetActive (false);
	}

	public void QuitExit()
	{
		quitAnimator.Play ("MainButtonHoverExit");
		DisableAllMenus ();
	}
	#endregion

	void DisableAllMenus ()
	{
		// Disables all other menus.
		controls.SetActive (false);
		multiplayer.SetActive (false);
		leaderboards.SetActive (false);
		settings.SetActive (false);
		credits.SetActive (false);
		quit.SetActive (false);
	}
}
	

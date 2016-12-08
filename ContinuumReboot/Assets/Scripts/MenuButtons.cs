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
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
	public AudioSource uiConfirm;
	public AudioSource uiHover;
	public AudioLowPassFilter bgMenuMusicLow;
	public AudioHighPassFilter bgMenuMusicHigh;
	public Text DescriptionText;
	public GameObject ParentMenu;
	public GlobalMouseVisibility mouseScript;

	[Header ("Press Start Button")]
	public GameObject TitleUI;
	public GameObject Menus;
	public GameObject AllUI;

	[Header("Play 1P Button")]
	public GameObject controls;
	public Animator p1Animator;
	public GameObject LoadingArcadeMode;
	public MenuPlay1P menuPlay1PScript;

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
	public AudioSource backgroundMenuAudioDrums;
	public AudioSource backgroundMenuAudioSynths;

	[Header("Quit Button")]
	public GameObject quit;
	public Animator quitAnimator;

	private InputScroll scrollScript;

	void Start()
	{
		scrollScript = GetComponent<InputScroll> ();
		mouseScript.InvisibleCursor ();
	}

	public void Update ()
	{
		if (Input.GetKeyDown ("joystick button 7") == true) 
		{
			if (TitleUI.activeInHierarchy == true) 
			{
				PressStartClick ();
				mouseScript.InvisibleCursor ();
			}
		}
	}

	public void PressStartClick ()
	{
		mouseScript.InvisibleCursor ();
		TitleUI.SetActive (false);
		ParentMenu.SetActive (true);
		uiConfirm.Play ();
		Camera.main.GetComponent <SmoothFollowOrig> ().enabled = false;
		Camera.main.GetComponent <Animator> ().enabled = true;
		Camera.main.GetComponent <Animator> ().Play ("StartToMain");
		AllUI.GetComponent<InputScroll> ().enabled = true;
		Menus.SetActive (true);
		menuPlay1PScript.OnEnter ();
	}

	#region Play 1P Button Functions
	public void P1Click()
	{
		uiConfirm.Play();
		LoadingArcadeMode.SetActive (true);
		ParentMenu.SetActive (false);
		Camera.main.GetComponent<Animator> ().Play ("MainToSelect");
		backgroundMenuAudioDrums.GetComponent<DecreaseAudioVolumeOverTime> ().enabled = true;
		backgroundMenuAudioSynths.GetComponent<DecreaseAudioVolumeOverTime> ().enabled = true;
		DisableAllMenus ();
	}

	public void P1Enter()
	{
		if (scrollScript != null) 
		{
			scrollScript.HighlightedButton = 0;
			mouseScript.InvisibleCursor ();
		}

		p1Animator.enabled = true;
		p1Animator.Play ("MainButtonHoverEnter");
		uiHover.Play();

		DescriptionText.text = "You control the speed of time based on your vertical position on the screen. Shoot and dodge to stay alive in order to get more points" +
			"" +
			"\n\nTo help with this, there are powerups which can be shot or collected, boosting firepower, or activating defenses." +
			"" +
			"\n\nThe longer you stay alive, the more points you'll get. In this mode, try to beat your friends' scores!";

		PlayerPrefs.SetString ("Menu", "controls");
		controls.SetActive (true);
		bgMenuMusicLow.enabled = false;
		bgMenuMusicHigh.enabled = false;
		backgroundMenuAudioDrums.UnPause();
		backgroundMenuAudioSynths.UnPause();
		backgroundMenuAudioDrums.volume = 0.85f;
		backgroundMenuAudioSynths.volume = 0.85f;

		creditsAudio.Pause();

		multiplayer.SetActive (false);
		leaderboards.SetActive (false);
		settings.SetActive (false);
		credits.SetActive (false);
		quit.SetActive (false);
	}

	public void P1Exit()
	{
		bgMenuMusicLow.enabled = false;
		bgMenuMusicHigh.enabled = false;

		p1Animator.Play ("MainButtonHoverExit");
		DescriptionText.text = "" + "";

		DisableAllMenus ();
	}
	#endregion

	#region Play 2P Button Functions
	public void P2Click()
	{
		uiConfirm.Play();
		ParentMenu.SetActive (false);
		Camera.main.GetComponent<Animator> ().Play ("MainToSelect");
		backgroundMenuAudioDrums.GetComponent<DecreaseAudioVolumeOverTime> ().enabled = true;
		backgroundMenuAudioSynths.GetComponent<DecreaseAudioVolumeOverTime> ().enabled = true;
		DisableAllMenus ();
	}

	public void P2Enter()
	{
		if (scrollScript != null) 
		{
			mouseScript.InvisibleCursor ();
			scrollScript.HighlightedButton = 1;
		}
			
		DescriptionText.text = "BOTH players control the speed of time based on their average vertical position on the screen. Shoot and dodge to stay alive in order to get more points" +
			"" +
			"\n\nTo help with this, there are powerups which can be shot or collected, boosting firepower, or activating defenses." +
			"" +
			"\n\nThe longer you both stay alive, the more points you'll both get. In this mode, teamwork is key!";

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
		bgMenuMusicHigh.enabled = false;
		backgroundMenuAudioDrums.UnPause();
		backgroundMenuAudioSynths.UnPause();

		backgroundMenuAudioDrums.volume = 0.85f;
		backgroundMenuAudioSynths.volume = 0.85f;
		creditsAudio.Pause();

		controls.SetActive (false);
		leaderboards.SetActive (false);
		settings.SetActive (false);
		credits.SetActive (false);
		quit.SetActive (false);
	}

	public void P2Exit()
	{
		bgMenuMusicLow.enabled = false;
		bgMenuMusicHigh.enabled = false;

		p2Animator.Play ("MainButtonHoverExit");
		smoothFollowClone2.enabled = true;
		smoothFollowOrig2.enabled = false;
		p2AudioOut.Play();
		DescriptionText.text = "" + "";
		DisableAllMenus ();
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
			mouseScript.InvisibleCursor ();
			scrollScript.HighlightedButton = 2;
		}

		leadAnimator.enabled = true;
		leadAnimator.Play ("MainButtonHoverEnter");
		uiHover.Play();
		DescriptionText.text = "View the top scorers." + "";
		PlayerPrefs.SetString ("Menu", "leaderboards");
		leaderboards.SetActive (true);
		//bgMenuMusicLow.enabled = true;

		backgroundMenuAudioDrums.UnPause();
		backgroundMenuAudioSynths.UnPause();

		backgroundMenuAudioDrums.volume = 0.0f;
		backgroundMenuAudioSynths.volume = 0.85f;

		creditsAudio.Pause();

		controls.SetActive (false);
		multiplayer.SetActive (false);
		settings.SetActive (false);
		credits.SetActive (false);
		quit.SetActive (false);
	}

	public void LeaderboardsExit()
	{
		bgMenuMusicLow.enabled = false;
		bgMenuMusicHigh.enabled = false;
		leadAnimator.Play ("MainButtonHoverExit");
		DescriptionText.text = "" + "";
		DisableAllMenus ();
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
		mouseScript.InvisibleCursor ();
		settingsAnimator.enabled = true;
		settingsAnimator.Play ("MainButtonHoverEnter");
		uiHover.Play();
		DescriptionText.text = "Settings." + "";
		PlayerPrefs.SetString ("Menu", "settings");
		settings.SetActive (true);
		//bgMenuMusicLow.enabled = true;

		backgroundMenuAudioDrums.UnPause();
		backgroundMenuAudioSynths.UnPause();
		backgroundMenuAudioDrums.volume = 0.85f;
		backgroundMenuAudioSynths.volume = 0.0f;

		creditsAudio.Pause();

		controls.SetActive (false);
		multiplayer.SetActive (false);
		leaderboards.SetActive (false);
		credits.SetActive (false);
		quit.SetActive (false);
	}

	public void SettingsExit()
	{
		bgMenuMusicLow.enabled = false;
		bgMenuMusicHigh.enabled = false;
		settingsAnimator.Play ("MainButtonHoverExit");
		DescriptionText.text = "" + "";
		DisableAllMenus ();
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
		mouseScript.InvisibleCursor ();
		creditsAnimator.enabled = true;
		creditsAnimator.Play ("MainButtonHoverEnter");
		uiHover.Play();
		DescriptionText.text = "Credits." + "";
		PlayerPrefs.SetString ("Menu", "credits");
		credits.SetActive (true);
		//bgMenuMusicLow.enabled = true;

		backgroundMenuAudioDrums.Pause();
		backgroundMenuAudioSynths.Pause();
		creditsAudio.Play();

		controls.SetActive (false);
		multiplayer.SetActive (false);
		settings.SetActive (false);
		leaderboards.SetActive (false);
		quit.SetActive (false);
	}

	public void CreditsExit()
	{
		bgMenuMusicLow.enabled = false;
		bgMenuMusicHigh.enabled = false;
		creditsAnimator.Play ("MainButtonHoverExit");
		DescriptionText.text = "" + "";
		DisableAllMenus ();
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
		mouseScript.InvisibleCursor ();
		quitAnimator.enabled = true;
		quitAnimator.Play ("MainButtonHoverEnter");
		uiHover.Play();
		DescriptionText.text = "Quit to desktop?" + "";
		PlayerPrefs.SetString ("Menu", "quit");
		quit.SetActive (true);
		bgMenuMusicLow.enabled = false;
		bgMenuMusicHigh.enabled = true;

		backgroundMenuAudioDrums.UnPause();
		backgroundMenuAudioSynths.UnPause();
		creditsAudio.Pause();

		controls.SetActive (false);
		multiplayer.SetActive (false);
		leaderboards.SetActive (false);
		settings.SetActive (false);
		credits.SetActive (false);
	}

	public void QuitExit()
	{
		bgMenuMusicLow.enabled = false;
		bgMenuMusicHigh.enabled = false;
		quitAnimator.Play ("MainButtonHoverExit");
		DescriptionText.text = "" + "";
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
		mouseScript.InvisibleCursor ();
	}
}
	

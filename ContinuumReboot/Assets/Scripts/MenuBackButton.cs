using UnityEngine;
using System.Collections;

public class MenuBackButton : MonoBehaviour
{
	public GameObject currentMenu;
	public AudioLowPassFilter bgMenuMusicLow;
	public AudioHighPassFilter bgMenuMusicHigh;
	public GameObject title;
	public AudioSource backAudio;
	public AudioSource bgMenuAudio;
	public AudioSource creditsAudio;

	public void BackToMainButton()
	{
		PlayerPrefs.SetString ("Menu", "main");
		bgMenuMusicLow.enabled = false;
		bgMenuMusicHigh.enabled = false;
		title.SetActive (true);
		backAudio.Play();
		currentMenu.SetActive (false);
	}

	public void BackFromCreditsButton()
	{
		PlayerPrefs.SetString ("Menu", "main");
		bgMenuMusicLow.enabled = false;
		bgMenuMusicHigh.enabled = false;
		title.SetActive (true);
		backAudio.Play();
		bgMenuAudio.UnPause();
		creditsAudio.Pause();
		currentMenu.SetActive (false);
	}
}

using UnityEngine;
using System.Collections;

public class MenuBackButton : MonoBehaviour
{
	public GameObject currentMenu;
	public GameObject[] OtherMenus;
	public AudioLowPassFilter bgMenuMusicLow;
	public AudioHighPassFilter bgMenuMusicHigh;
	public GameObject title;
	public AudioSource backAudio;
	public AudioSource bgMenuAudio;
	public AudioSource creditsAudio;
	public Animator CameraAnim;

	public void BackToMainButton()
	{
		PlayerPrefs.SetString ("InputMenu", "mainmenu");
		bgMenuMusicLow.enabled = false;
		bgMenuMusicHigh.enabled = false;
		CameraAnim.Play ("MainToStart");
		title.SetActive (true);
		backAudio.Play();
		currentMenu.SetActive (false);

		foreach (GameObject otherMenu in OtherMenus) 
		{
			otherMenu.SetActive (false);
		}
	}

	public void BackFromCreditsButton()
	{
		PlayerPrefs.SetString ("InputMenu", "mainmenu");
		bgMenuMusicLow.enabled = false;
		bgMenuMusicHigh.enabled = false;
		title.SetActive (true);
		backAudio.Play();
		bgMenuAudio.UnPause();
		creditsAudio.Pause();
		currentMenu.SetActive (false);
	}
}

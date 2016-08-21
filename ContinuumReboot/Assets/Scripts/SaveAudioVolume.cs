using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SaveAudioVolume : MonoBehaviour 
{
	public Slider BGMVolSlider;
	public AudioSource BGMAudio;

	void Start () 
	{
		BGMAudio.volume =  PlayerPrefs.GetFloat ("BGMVol", 1);
		BGMVolSlider.value = PlayerPrefs.GetFloat ("BGMVol", 1);
	}
	
	void Update () 
	{
	
	}

	public void OnValueChanged () 
	{
		PlayerPrefs.SetFloat ("BGMVol", BGMVolSlider.value);
	}
}

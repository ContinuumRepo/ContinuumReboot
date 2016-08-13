using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneWhenFaded : MonoBehaviour 
{
	public enum fadeType {FadeIn, FadeOut}
	public fadeType FadeType;
	public string SceneName = "main";

	void Start () 
	{
	}
	
	void Update () 
	{
		if (FadeType == fadeType.FadeIn) 
		{
			if (GetComponent<Image> ().color.a == 1) 
			{
				SceneManager.LoadScene (SceneName);
			}
		}	

		if (FadeType == fadeType.FadeOut) 
		{
			if (GetComponent<Image> ().color.a == 0) 
			{
				SceneManager.LoadScene (SceneName);
			}
		}	
	}
}

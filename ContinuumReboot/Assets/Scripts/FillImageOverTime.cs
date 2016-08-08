using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FillImageOverTime : MonoBehaviour 
{
	private Image im;
	public float time;
	public string sceneName;
	public GameObject LoadingText;

	void Start () 
	{
		GetComponent<Image> ().fillAmount = 0;
		LoadingText.SetActive (false);
	}

	void Update () 
	{
		GetComponent<Image> ().fillAmount += Time.unscaledDeltaTime / time;

		if (GetComponent<Image> ().fillAmount >= 1) 
		{
			LoadingText.SetActive (true);
			SceneManager.LoadScene (sceneName);
		}
	}
}

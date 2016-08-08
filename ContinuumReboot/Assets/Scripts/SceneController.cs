using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour 
{
	public float delay = 5.0f;
	public string sceneName = "menu";

	void Start () 
	{
		StartCoroutine (LoadMenu ());
	}

	IEnumerator LoadMenu ()
	{
		yield return new WaitForSeconds (delay);
		SceneManager.LoadScene (sceneName);
	}
}

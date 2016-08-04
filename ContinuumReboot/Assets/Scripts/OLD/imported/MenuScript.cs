using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour 
{
	public void LoadScene(string sceneName)
	{
		// Application.LoadLevel(sceneName); (Deprecated)
		SceneManager.LoadScene (sceneName);
	}
}
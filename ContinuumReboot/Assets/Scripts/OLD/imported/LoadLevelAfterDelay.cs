using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevelAfterDelay : MonoBehaviour 
{
	public float LoadTime = 3.0f;
	public string Level = "Menu";
	
	void Awake(){
		StartCoroutine(LoadAfterDelay(Level));
	}
	
	public IEnumerator LoadAfterDelay(string levelName)
	{
		yield return new WaitForSeconds (LoadTime); // wait LoadTime seconds
		// Application.LoadLevel (Level); (Deprecated)
		SceneManager.LoadScene (Level);
	}

}
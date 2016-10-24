using UnityEngine;
using System.Collections;

public class OpenUrlOnPress : MonoBehaviour 
{
	public string URL;

	void Start() 
	{
		//Application.OpenURL(URL);
	}

	public void OpenURL (string URL)
	{
		Application.OpenURL(URL);

		// Deprecated:
		//Application.ExternalEval("window.open('http://www.google.com','_blank')");
		//Application.ExternalEval("window.open('" + URL + "','_blank')");
	}
}
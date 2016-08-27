using UnityEngine;
using System.Collections;

public class QuitScript : MonoBehaviour 
{
	public void QuitGame ()
	{
		Application.Quit ();

		#if UNITY_EDITOR
			Debug.Log ("Tried to quit in editor.");
		#endif
	}
}

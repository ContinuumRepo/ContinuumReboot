using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour, IPointerClickHandler 
{
	public GameController GameControllerScript;
	
	void Start () 
	{
	
	}

	void Update () 
	{
	
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		if (GameControllerScript.isPaused == false) 
		{
			GameControllerScript.isPaused = true;
			Debug.Log ("Paused");
		}
	}
}

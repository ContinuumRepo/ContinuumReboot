using UnityEngine;
using System.Collections;

public class TurnOffPreGameBool : MonoBehaviour 
{
	public Game gameControllerScript;

	void FixedUpdate () 
	{
		gameControllerScript.isPreGame = false;
		Destroy (gameObject);
	}
}

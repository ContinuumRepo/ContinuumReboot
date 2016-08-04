using UnityEngine;
using System.Collections;

public class PlayButtonScript : MonoBehaviour 
{
	public GameController GameController;
	public TimescaleCalculator Tscalc;
	public bool PreGame;
	
	void Start () 
	{
		PreGame = true;
		//GameObject GameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		GameController.PreGame = PreGame;
	}

	void Update () 
	{

	}

	public void inPreGame (bool PreGame)
	{
		//GameController.PreGame = false;
		GameController.PreGame = (PreGame);
		Tscalc.isLevelOne = true;
		gameObject.GetComponent<PlayButtonScript> ().enabled = false;

	}
}

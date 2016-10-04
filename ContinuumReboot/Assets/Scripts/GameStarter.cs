using UnityEngine;
using System.Collections;

public class GameStarter : MonoBehaviour 
{
	public GameObject PowerupOne;
	public GameObject PowerupTwo;
	public GameObject OrangeBox, YellowBox, GreenBox, BlueBox, PurpleBox;
	public TimescaleController tsControl;

	void Start () 
	{
		GetComponent<GameController> ().startWait = 3;
	}

	void Update () 
	{
		if (PowerupOne == null && PowerupTwo == null && OrangeBox == null && YellowBox == null && GreenBox == null && BlueBox == null && PurpleBox == null) 
		{
			GetComponent<GameController> ().isPreGame = false;
			//GetComponent<GameController> ().startWait = 1;
			StartGame ();
			GetComponent<GameController> ().WaveLabel.SetActive (true);
			GetComponent<GameController> ().WaveLabel.GetComponent<DestroyOrDeactivateByTime> ().enabled = true;
			GetComponent<GameController> ().WaveLabel.GetComponent<Animator> ().Play ("WaveLabel");
			tsControl.enabled = true;
			//Time.timeScale = 0.1f;
			GetComponent<GameStarter>().enabled = false;
		}
	}

	void StartGame ()
	{
		StartCoroutine (GetComponent<GameController> ().BrickSpawnWaves ());

	}
}

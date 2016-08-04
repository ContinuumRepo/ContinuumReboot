using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelMod : MonoBehaviour 
{
	public GameObject Explosion;
	public int levelModValue = 1;
	private GameController GameControllerScript;
	public GameObject LevelModTextParticles;
	public TimescaleCalculator TSCalc;
	public float addTime = 5.0f;
	private Animator LevelModText;
	
	void Start () 
	{
		GameObject LevelModObject = GameObject.FindGameObjectWithTag ("LevelModText");
		LevelModText = LevelModObject.GetComponent<Animator> ();
	}
	
	void Update () 
	{
		GameObject TSCalcObject = GameObject.FindGameObjectWithTag ("TSCalc");
		TSCalc = TSCalcObject.GetComponent<TimescaleCalculator>();
		
		// Checks if there is a GameController script attached to GameObject with "GameController" as its tag
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		
		if (gameControllerObject != null)
		{
			GameControllerScript = gameControllerObject.GetComponent <GameController>();
		}
		
		if (GameControllerScript == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}
	
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "PlayerGeom" || other.tag == "LevelMod") 
		{
			// Increments points by point value
			GameControllerScript.currentlevelModPoints += levelModValue;
			Instantiate (LevelModTextParticles, LevelModTextParticles.transform.position, LevelModTextParticles.transform.rotation);
			//Debug.Log ("Collected a point");

			// UI animation
			LevelModText.Play(0);

			// Makes nice explosion
			Instantiate (Explosion, transform.position, Explosion.transform.rotation);
			Destroy (gameObject);

			if (TSCalc.isLevelOne == true)
			{
				TSCalc.levelOneTimeRemaining += addTime;
			}

			if (TSCalc.isLevelTwo == true)
			{
				TSCalc.levelTwoTimeRemaining += addTime;
			}

			if (TSCalc.isLevelThree == true)
			{
				TSCalc.levelThreeTimeRemaining += addTime;
			}

			if (TSCalc.isLevelFour == true)
			{
				TSCalc.levelFourTimeRemaining += addTime;
			}
		}
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeController : MonoBehaviour 
{
	private Game gameControllerScript;
	public Player playerControllerScript;
	public float distance;
	public float dampener = 1.0f;
	public float timeAdded;
	public float dampenIncreaser = 100.0f;
	public float timeScalenow;
	public Text TimeScaleText;
	public float addTime;
	public Transform TimescaleTransform;
	public Transform PlayerTransform;
	public bool methodOne;
	public bool methodTwo;

	void Start () 
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		gameControllerScript = gameControllerObject.GetComponent<Game> ();
	}

	void Update () 
	{
		timeScalenow = Time.timeScale;
		//TimeScaleText.text = "x " + string.Format ("{0:0.00}", Mathf.Round (timeScalenow * 100f) / 100f) + "";
		TimeScaleText.text = "" + string.Format ("{0:0}", Mathf.Round (timeScalenow * 100f)) + "%";

		if (methodOne == true)
		{

			distance = PlayerTransform.transform.position.z - TimescaleTransform.transform.position.z;

			if (gameControllerScript.isGameOver == false) 
			{
				Time.timeScale = (distance / dampener) + addTime + timeAdded;
				timeAdded += Time.unscaledDeltaTime / dampenIncreaser;	

				if (Time.timeScale > 5) {
					Time.timeScale = 5;
				}
			}
		}

		if (methodTwo == true) 
		{
			distance = Vector3.Distance (PlayerTransform.transform.position, TimescaleTransform.transform.position);

			if (gameControllerScript.isGameOver == false) 
			{
				Time.timeScale = (distance / dampener) + addTime + timeAdded;
				timeAdded += Time.unscaledDeltaTime / dampenIncreaser;

				if (Time.timeScale > 5) 
				{
					Time.timeScale = 5;
				}
			}
		}

		if (methodOne == true && methodTwo == true) 
		{
			Debug.LogWarning ("You have method one and method two enabled, only one is supposed to be enabled.");
		}

		if (gameControllerScript.isGameOver == true) 
		{
			Time.timeScale -= Time.deltaTime;

			if (Time.timeScale < 0) 
			{
				Time.timeScale = 0;
			}
		}
		
	}
}

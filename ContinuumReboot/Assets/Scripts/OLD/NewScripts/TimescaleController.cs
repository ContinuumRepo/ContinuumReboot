using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimescaleController : MonoBehaviour 
{
	public GameObject player;
	public PlayerController playerControllerScript;
	public NewGameController gameControllerScript;
	public ClickAndDragCS clickDragScript;
	public float distance;
	public float timeScale;
	public float timeScaleRO;
	public float timeScaleIncreased;
	public float dampener;
	public float dampenIncreaser;
	public float YTime;
	public Transform ObjectRef;

	public int level = 1;

	void Start () 
	{
		level = 1;
	}

	void Update () 
	{
		if (gameControllerScript.isPaused == true) 
		{
			Time.timeScale = 0;
			clickDragScript.enabled = false;
			//playerControllerScript.enabled = false;
		}

		if (gameControllerScript.isPaused == false) 
		{
			clickDragScript.enabled = true;
			//playerControllerScript.enabled = true;
			timeScaleRO = Time.timeScale;

			//Time.timeScale = 0+ player.GetComponent<Rigidbody> ().velocity.x;


			// Calculates distance
			distance = player.transform.position.z - ObjectRef.transform.position.z;

			// Calculates timescale
			timeScale = 0.3f + (distance + timeScaleIncreased) / 8.0f;
			Time.timeScale = timeScale - YTime;

			// Calculates distance between player and reference point
			//distance = Vector3.Distance (player.transform.position, gameObject.transform.position);
			//Time.timeScale = timeScale + Vector3.Distance (ObjectRef.transform.position, (Player.transform.position) / dampener) + YTime;

			// Sets timescale value to increase continuously
			timeScaleIncreased += Time.unscaledDeltaTime / dampenIncreaser;

			//Time.timeScale = timeScale;
		
			// Calculates final Time.timeScale
			//Time.timeScale = (1 + timeScale + (distance / (dampener))) - YTime;
		}
	}
}

using UnityEngine;
using System.Collections;

public class TimescaleController : MonoBehaviour 
{
	public float highestTimeScale; // For stats screen.
	public float timeScaleReadOnly; // The actual value of time.timeScale.
	public float startTimeScale = 1.0f; // So there is no runtime error that timescale is below 0 at the start of the game.
	public float timeSpeedIncreaseSens; // Multipler to minimum time increase per frame.
	public float addMinTime = 0.1f; // Minimum time scale.
	public float timeSpeedSens; // Dampens change in timescale as the distance between player and reference point increase.
	public float distance; // y-distance from Reference point to player.

	private float currentTimeScale; // Stores calculation of timescale here.
	private Transform player;
	private Transform referencePoint;

	public void Start () 
	{
		Time.timeScale = startTimeScale; // Sets timescale to 1
		player = GameObject.Find ("Player").transform; // Finds Player GameObject.transform.
		referencePoint = GameObject.FindGameObjectWithTag ("ReferencePoint").transform; // Finds reference point gameObject.transform.
	}

	public void Update () 
	{
		timeScaleReadOnly = Time.timeScale; // See actual time.TimeScale in inspector so you dont have to always check in edit > Project Settings > Time.
		Time.timeScale = ((distance + currentTimeScale) * timeSpeedSens) + addMinTime; // Stores values into time.TimeScale.
		currentTimeScale += Time.unscaledDeltaTime * timeSpeedIncreaseSens; // Increases minimum timescale.
		distance = player.transform.position.y - referencePoint.transform.position.y; // Calculates distance.

		// Stores the highest timescale value for stats.
		if (Time.timeScale > highestTimeScale) 
		{
			highestTimeScale = Time.timeScale;
		}

	}
}

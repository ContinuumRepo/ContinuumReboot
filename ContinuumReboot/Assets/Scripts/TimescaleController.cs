using UnityEngine;
using System.Collections;

public class TimescaleController : MonoBehaviour 
{
	public float CurrentTimeScale; // Stores calculation of timescale here.
	public float highestTimeScale;
	public float TimeScaleReadOnly; // The actual value of time.timeScale.
	public float StartTimeScale = 1.0f;
	public float TimeSpeedIncreaseSens; // Multipler to minimum time increase per frame.
	public float AddMinTime = 0.1f; // Minimum time scale.
	public float TimeSpeedSens; // Dampens change in timescale as the distance between player and reference point increase.

	public Transform Player;
	public Transform ReferencePoint;
	public float distance; // y-distance from Reference point to player.

	void Start () 
	{
		Time.timeScale = StartTimeScale; // Sets timescale to 1
	}
	

	void Update () 
	{
		TimeScaleReadOnly = Time.timeScale; // See actual time.TimeScale in inspector so you dont have to always check in edit > Project Settings > Time.
		Time.timeScale = ((distance + CurrentTimeScale) * TimeSpeedSens) + AddMinTime; // Stores values into time.TimeScale.
		CurrentTimeScale += Time.unscaledDeltaTime * TimeSpeedIncreaseSens; // Increases minimum timescale.
		distance = Player.transform.position.y - ReferencePoint.transform.position.y; // Calculates distance.

		if (Time.timeScale > highestTimeScale) 
		{
			highestTimeScale = Time.timeScale;
		}

	}
}

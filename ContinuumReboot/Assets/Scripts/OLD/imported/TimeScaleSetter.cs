using UnityEngine;
using System.Collections;

public class TimeScaleSetter : MonoBehaviour 
{
	public float TimeScale;

	void Start () 
	{
		Time.timeScale = TimeScale;
	}

	public void TimeScaler (float TimeScale) 
	{
		Time.timeScale = TimeScale;
	}
}
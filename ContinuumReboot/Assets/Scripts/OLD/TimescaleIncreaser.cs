using UnityEngine;
using System.Collections;

public class TimescaleIncreaser : MonoBehaviour 
{
	public float Dampener;
	
	void Start () 
	{
	
	}

	void Update () 
	{
		Time.timeScale += Time.unscaledDeltaTime / Dampener;

		if (Time.timeScale > 98)
		{
			Time.timeScale = 98;
		}
	
	}
}

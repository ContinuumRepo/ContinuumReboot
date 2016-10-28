using UnityEngine;
using System.Collections;

public class BoxArtController : MonoBehaviour 
{
	public float ts;

	void Start () 
	{
		Time.timeScale = 1;
	}

	void FixedUpdate () 
	{
		ts = Time.timeScale;

		if (Time.timeScale >= 0.01f) 
		{
			if (Input.GetAxis ("MouseScrollwheel") > 0) 
			{
				Time.timeScale += 0.02f;
			}

			if (Input.GetAxis ("MouseScrollwheel") < 0) 
			{
				Time.timeScale -= 0.02f;
			}
		}

		if (Time.timeScale < 0.01f) 
		{
			Time.timeScale = 0.01f;
		}
	}
}

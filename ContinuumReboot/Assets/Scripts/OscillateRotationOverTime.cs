using UnityEngine;
using System.Collections;

public class OscillateRotationOverTime : MonoBehaviour 
{
	public float amount = 1;
	public float frequency = 1;
	public float time;
	public float offset;

	public bool changePos;

	void Update () 
	{
		time += Time.deltaTime;

		if (changePos == false)
		{
			transform.rotation = Quaternion.Euler (0, amount * Mathf.Sin (time * frequency) + offset, 0);
		}

		if (changePos == true) 
		{
			transform.localPosition = new Vector3 (0, amount * Mathf.Sin (time * frequency) + offset, 0);
		}
	}
}

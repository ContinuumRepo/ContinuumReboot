using UnityEngine;
using System.Collections;

public class OscillateRotationOverTime : MonoBehaviour 
{
	public float amount = 1;
	public float frequency = 1;
	public float time;
	public float offset;

	void Start () 
	{
	
	}

	void Update () 
	{
		time += Time.deltaTime;

		transform.rotation = Quaternion.Euler(0, amount * Mathf.Sin(time * frequency) + offset, 0);
	}
}

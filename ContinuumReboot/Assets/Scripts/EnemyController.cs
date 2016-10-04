using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour 
{
	public GameObject Brain;

	void Start ()
	{
		Brain = GameObject.Find ("Brain");
	}

	void Update () 
	{
		if (Brain == null) 
		{
			// Do something.
		}
	}
}

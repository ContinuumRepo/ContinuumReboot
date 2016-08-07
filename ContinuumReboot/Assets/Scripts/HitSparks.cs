using UnityEngine;
using System.Collections;

public class HitSparks : MonoBehaviour 
{
	public ParticleSystem HitSpark;

	void Start () 
	{
		// Gets particle system component.
		HitSpark = GetComponent<ParticleSystem> ();
	}

	void Update () 
	{
	
	}

	void OnCollisionEnter (Collision collision)
	{
		if (collision.collider.tag == "Barrier" && HitSpark.isPlaying == false) 
		{
			// Play hit spark particle system.
			HitSpark.Play ();
		}
	}

	void OnCollisionExit (Collision collision)
	{
		if (collision.collider.tag == "Barrier" && HitSpark.isPlaying == false) 
		{
			// Stop hit spark particle system.
			HitSpark.Stop ();
		}
	}
}

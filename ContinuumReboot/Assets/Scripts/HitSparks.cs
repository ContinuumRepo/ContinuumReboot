using UnityEngine;
using System.Collections;

public class HitSparks : MonoBehaviour 
{
	public ParticleSystem HitSpark;

	void Start () 
	{
		HitSpark = GetComponent<ParticleSystem> ();
	}

	void Update () 
	{
	
	}

	void OnCollisionEnter (Collision collision)
	{
		if (collision.collider.tag == "Barrier" && HitSpark.isPlaying == false) 
		{
			HitSpark.Play ();
			//Debug.Log ("Collided.");
		}
	}

	void OnCollisionExit (Collision collision)
	{
		if (collision.collider.tag == "Barrier" && HitSpark.isPlaying == false) 
		{
			HitSpark.Stop ();
			//Debug.Log ("Collided.");
		}
	}
}

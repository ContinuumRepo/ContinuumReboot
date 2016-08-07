using UnityEngine;
using System.Collections;

public class PointObject : MonoBehaviour 
{
	public GameObject Explosion; // Instantiated explosion.
	public enum type {Orange, Yellow, Green, Cyan, Purple}; // Cube types.
	public type PointType;
	public ParticleSystem MainEngineParticles; // Engine Particle system.

	void Start ()
	{
		// Finding the main engine particle system GameObject.
		GameObject MainEngine = GameObject.FindGameObjectWithTag ("MainEngine");
		MainEngineParticles = MainEngine.GetComponent<ParticleSystem> ();
	}

	void Update () 
	{
	
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.collider.tag == "Player") 
		{
			// Creates explosion.
			Instantiate (Explosion, transform.position, transform.rotation);

			if (PointType == type.Orange) 
			{
				// Turns the engine color to orange.
				MainEngineParticles.startColor = new Color (0.78f, 0.33f, 0, 1);
			}

			if (PointType == type.Yellow) 
			{
				// Turns the engine color to yellow.
				MainEngineParticles.startColor = new Color (1, 1, 0, 1);
			}

			if (PointType == type.Green) 
			{
				// Turns the engine color to green.
				MainEngineParticles.startColor = new Color (0, 1, 0, 1);
			}

			if (PointType == type.Cyan) 
			{
				// Turns the engine color to cyan.
				MainEngineParticles.startColor = new Color (0, 1, 1, 1);
			}

			if (PointType == type.Purple) 
			{
				// Turns the engine color to purple.
				MainEngineParticles.startColor = new Color (0.39f, 0, 1, 1);
			}

			Destroy (gameObject); // Destroys the gameObject.
		}
	}
}

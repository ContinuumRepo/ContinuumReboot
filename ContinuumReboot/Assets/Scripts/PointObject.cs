using UnityEngine;
using System.Collections;

public class PointObject : MonoBehaviour 
{
	public GameObject Explosion; // Instantiated explosion.
	public enum type {Orange, Yellow, Green, Cyan, Purple}; // Cube types.
	public type PointType;
	public float PointReward = 150;
	public ParticleSystem MainEngineParticles; // Engine Particle system.
	public GameObject PlayerExplosion;
	private PlayerController PlayerControllerScript;
	private GameController gameControllerScript;
	public float Damage = 25.0f;

	void Start ()
	{
		// Finding the main engine particle system GameObject.
		GameObject MainEngine = GameObject.FindGameObjectWithTag ("MainEngine");
		MainEngineParticles = MainEngine.GetComponent<ParticleSystem> ();

		PlayerControllerScript = GameObject.Find ("Player").GetComponent<PlayerController>();

		gameControllerScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
	}

	void Update () 
	{
	
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Bullet") 
		{
			// Creates explosion.
			Instantiate (Explosion, transform.position, transform.rotation);

			if (PointType == type.Orange) 
			{
				// Turns the engine color to orange.
				MainEngineParticles.startColor = new Color (0.78f, 0.33f, 0, 1);
				gameControllerScript.CurrentScore += PointReward;
			}

			if (PointType == type.Yellow) 
			{
				// Turns the engine color to yellow.
				MainEngineParticles.startColor = new Color (1, 1, 0, 1);
				gameControllerScript.CurrentScore += PointReward * 2;
			}

			if (PointType == type.Green) 
			{
				// Turns the engine color to green.
				MainEngineParticles.startColor = new Color (0, 1, 0, 1);
				gameControllerScript.CurrentScore += PointReward * 3;
			}

			if (PointType == type.Cyan) 
			{
				// Turns the engine color to cyan.
				MainEngineParticles.startColor = new Color (0, 1, 1, 1);
				gameControllerScript.CurrentScore += PointReward * 4;
			}

			if (PointType == type.Purple) 
			{
				// Turns the engine color to purple.
				MainEngineParticles.startColor = new Color (0.39f, 0, 1, 1);
				gameControllerScript.CurrentScore += PointReward * 5;
			}

			Destroy (gameObject); // Destroys the gameObject.
		}

		if (other.tag == "Player")
		{
			GameObject[] Destroyers = GameObject.FindGameObjectsWithTag ("Cube");

			for (int i = Destroyers.Length-1; i > 0; i--)
			{
				Destroy (Destroyers [i].gameObject);
			}

			Instantiate (PlayerExplosion, transform.position, transform.rotation);
			PlayerControllerScript.Health -= Damage;
		}
	}
}

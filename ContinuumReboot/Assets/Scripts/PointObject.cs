using UnityEngine;
using System.Collections;

public class PointObject : MonoBehaviour 
{
	public GameObject Explosion;
	public enum type {Orange, Yellow, Green, Cyan, Purple};
	public type PointType;
	public ParticleSystem MainEngineParticles;

	void Start ()
	{
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
			Instantiate (Explosion, transform.position, transform.rotation);
			Destroy (gameObject);


			if (PointType == type.Orange) 
			{
				MainEngineParticles.startColor = new Color (0.78f, 0.33f, 0, 1);
			}

			if (PointType == type.Yellow) 
			{
				MainEngineParticles.startColor = new Color (1, 1, 0, 1);
			}

			if (PointType == type.Green) 
			{
				MainEngineParticles.startColor = new Color (0, 1, 0, 1);
			}

			if (PointType == type.Cyan) 
			{
				MainEngineParticles.startColor = new Color (0, 1, 1, 1);
			}

			if (PointType == type.Purple) 
			{
				MainEngineParticles.startColor = new Color (0.39f, 0, 1, 1);
			}
		}
	}
}

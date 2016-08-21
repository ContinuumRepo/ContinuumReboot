using UnityEngine;
using System.Collections;

public class DestroyIfNoChild : MonoBehaviour 
{
	public enum type {Points, Bullets}
	public type Type;

	void Start ()
	{
	}
	
	void Update () 
	{
		if (Type == type.Points) {
			GetComponentInChildren<PointObject> ();

			if (gameObject.GetComponentInChildren<PointObject> () == null) {
				Destroy (gameObject);
			}	
		}

		if (Type == type.Bullets) 
		{
			GetComponentInChildren<BulletScript> ();
			if (gameObject.GetComponentInChildren<BulletScript> () == null) 
			{
				Destroy (gameObject);
			}
		}
	}
}

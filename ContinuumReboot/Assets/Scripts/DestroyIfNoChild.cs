using UnityEngine;
using System.Collections;

public class DestroyIfNoChild : MonoBehaviour 
{
	public enum type {Points, Bullets}
	public type Type;
	public bool activateThings;
	public GameObject Activator;

	void Start ()
	{
	}
	
	void Update () 
	{
		if (Type == type.Points) {
			GetComponentInChildren<PointObject> ();

			if (gameObject.GetComponentInChildren<PointObject> () == null) 
			{
				if (activateThings == true) 
				{
					Activator.SetActive (true);
				}

				Destroy (gameObject);
			}	
		}

		if (Type == type.Bullets) 
		{
			GetComponentInChildren<BulletScript> ();
			if (gameObject.GetComponentInChildren<BulletScript> () == null) 
			{
				if (activateThings == true) 
				{
					Activator.SetActive (true);
				}

				Destroy (gameObject);
			}
		}
	}
}

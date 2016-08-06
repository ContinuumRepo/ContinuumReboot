using UnityEngine;
using System.Collections;

public class PointObject : MonoBehaviour 
{
	public GameObject Explosion;
	public enum type {Orange, Yellow, Green, Cyan, Purple};
	public type PointType;
	public GameObject MainEngine;

	void Start ()
	{
		if (PointType == type.Orange) 
		{
			MainEngine = GameObject.FindGameObjectWithTag ("MainEngineOrange");
		}
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

			MainEngine.SetActive (true);
		}
	}
}

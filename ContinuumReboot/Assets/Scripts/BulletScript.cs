using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour 
{
	public GameObject[] ObjectsFound;

	void Start () 
	{
		ObjectsFound = GameObject.FindGameObjectsWithTag ("Brick");
	}

	void Update () 
	{
		Vector3.MoveTowards (gameObject.transform.position, ObjectsFound [Random.Range (0, ObjectsFound.Length)].transform.position, 0.5f);
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Brick" || other.tag == "Barrier") 
		{
			//transform.LookAt(ObjectsFound[Random.Range(0, ObjectsFound.Length)].transform.position);
			gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(-180, 180));

			Debug.Log ("Ricoshet!");
		}
	}
}

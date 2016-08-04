using UnityEngine;
using System.Collections;

public class TeleporterScript : MonoBehaviour {

	public Transform TeleporterOut;
	public GameObject TeleporterEffect;
	public Transform Player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player")
		{
			Player.transform.position = TeleporterOut.transform.position;
			Instantiate (TeleporterEffect, TeleporterOut.transform.position, TeleporterOut.transform.rotation);
		}

		if (other.tag == "PlasmaBullet")
		{
			other.transform.position = TeleporterOut.transform.position;
			//Instantiate (TeleporterEffect, TeleporterOut.transform.position, TeleporterOut.transform.rotation);
		}

		if (other.tag == "Enemy1" || other.tag == "Enemy2") 
		{
			other.transform.position = TeleporterOut.transform.position;
			Instantiate (TeleporterEffect, TeleporterOut.transform.position, TeleporterOut.transform.rotation);
		}
	}
}

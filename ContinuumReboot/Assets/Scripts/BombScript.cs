using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;

public class BombScript : MonoBehaviour 
{
	public int Damage = 25;
	public Vector2 MoveRange;
	private PlayerController PlayerControllerScript;

	public GameObject PlayerExplosion;
	public GameObject BombExplosion;

	private AutoMoveAndRotate moveScript;

	void Start () 
	{
		PlayerControllerScript = GameObject.Find ("Player").GetComponent<PlayerController>();
		moveScript = GetComponent<AutoMoveAndRotate> ();

		moveScript.moveUnitsPerSecond.value = new Vector3 (Random.Range (-MoveRange.x, MoveRange.x), Random.Range (-MoveRange.y, MoveRange.y), 0); 
	}

	void OnTriggerEnter (Collider other)
	{
		Instantiate (BombExplosion, gameObject.transform.position, gameObject.transform.rotation);

		if (other.tag == "Cube") 
		{
			Instantiate (other.gameObject.GetComponent<PointObject>().OrangeExplosion, transform.position, Quaternion.identity);
			Destroy (other.gameObject);
		}

		if (other.tag == "Bullet" || other.name == "HorizontalBeam" || other.name == "Shield" || other.name == "GreenBeam" || 
			other.name == "AltFire (no cost)" || other.name == "HelixLeft" || other.name == "HelixRight") 
		{
			Destroy (gameObject);
		}
			
		if (other.tag == "Player") 
		{	
			if (PlayerControllerScript.Health > 25) 
			{
				// Finds Points to destroy.
				GameObject[] Destroyers = GameObject.FindGameObjectsWithTag ("Cube");
				for (int i = Destroyers.Length - 1; i > 0; i--) 
				{
					Destroy (Destroyers [i].gameObject);
				}
			}

			if (PlayerControllerScript.Health > 10) 
			{
				// Instantiates a larger explosion.
				Instantiate (PlayerExplosion, transform.position, transform.rotation);
			}

			// Gives damage to player.
			PlayerControllerScript.Health -= Damage;

			PlayerControllerScript.OverlayTime = 2;

			// If player health is less than 10.
			if (PlayerControllerScript.Health <= 10) 
			{
				// Instantiate huge explosion.
				Instantiate (PlayerControllerScript.gameOverExplosion, gameObject.transform.position, Quaternion.identity);
			}
		}
	}
}
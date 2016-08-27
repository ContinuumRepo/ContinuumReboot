using UnityEngine;
using System.Collections;

public class PowerupScript : MonoBehaviour 
{
	private PlayerController playerControllerScript;
	//private GameController gameControllerScript;

	public enum poweruptype {doubleShot, triShot, beamShot, shield, horizontalBeam, clone} // Add more later.
	public poweruptype PowerupType;

	public GameObject Explosion;

	void Start () 
	{
		playerControllerScript = GameObject.Find ("Player").GetComponent<PlayerController>();
		//gameControllerScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent <GameController> ();
	}

	void Update () 
	{
	
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player" || other.tag == "Bullet") 
		{
			if (PowerupType == poweruptype.doubleShot) 
			{
				playerControllerScript.powerupTime = 15.0f;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.DoubleShot;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScript.ActivePowerupParticles.Play ();
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.triShot) 
			{
				playerControllerScript.powerupTime = 20.0f;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.TriShot;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScript.ActivePowerupParticles.Play ();
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.beamShot) 
			{
				playerControllerScript.powerupTime = 15.0f;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.BeamShot;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScript.ActivePowerupParticles.Play ();
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.shield) 
			{
				playerControllerScript.powerupTime = 12.5f;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.shield;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.horizontalBeam) 
			{
				playerControllerScript.powerupTime = 10.0f;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.horizontalBeam;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScript.ActivePowerupParticles.Play ();
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.clone) 
			{
				playerControllerScript.powerupTime = 30.0f;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.Clone;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScript.ActivePowerupParticles.Play ();
				Destroy (gameObject);
			}
		}
	}
}

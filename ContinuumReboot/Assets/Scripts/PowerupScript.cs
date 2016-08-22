using UnityEngine;
using System.Collections;

public class PowerupScript : MonoBehaviour 
{
	private PlayerController playerControllerScript;
	private GameController gameControllerScript;

	public enum poweruptype {doubleShot, triShot, beamShot, shield, homingShot} // Add more later.
	public poweruptype PowerupType;

	public GameObject Explosion;

	void Start () 
	{
		playerControllerScript = GameObject.Find ("Player").GetComponent<PlayerController>();
		gameControllerScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent <GameController> ();
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
				playerControllerScript.powerupTime = playerControllerScript.powerupDuration;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.DoubleShot;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScript.ActivePowerupParticles.Play ();
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.triShot) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDuration;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.TriShot;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScript.ActivePowerupParticles.Play ();
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.beamShot) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDuration;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.BeamShot;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScript.ActivePowerupParticles.Play ();
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.shield) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDuration;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.shield;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.homingShot) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDuration;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.homingShot;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScript.ActivePowerupParticles.Play ();
				Destroy (gameObject);
			}
		}
	}
}

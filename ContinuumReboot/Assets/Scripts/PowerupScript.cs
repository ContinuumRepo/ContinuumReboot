using UnityEngine;
using System.Collections;

public class PowerupScript : MonoBehaviour 
{
	private PlayerController playerControllerScript; // The player controller script component.
	public enum poweruptype {doubleShot, triShot, beamShot, shield, horizontalBeam, clone} // Powerups list.
	public poweruptype PowerupType; // To show the above enum.
	public GameObject Explosion; // The explosion to play on trigger enter.

	void Start () 
	{
		// Finds player controller component.
		playerControllerScript = GameObject.Find ("Player").GetComponent<PlayerController>();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player" || other.tag == "Bullet") 
		{
			/// Summary ///
			/// if powerup type == X powerup
			/// set powerup time duration
			/// change powerup type in player script
			/// Instantiate explosion
			/// Turn on powerup particles on player
			/// Destroy this gameObject
			/// End Summary ///

			if (PowerupType == poweruptype.doubleShot) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationA;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.DoubleShot;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScript.ActivePowerupParticles.Play ();
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.triShot) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationB;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.TriShot;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScript.ActivePowerupParticles.Play ();
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.beamShot) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationC;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.BeamShot;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScript.ActivePowerupParticles.Play ();
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.shield) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationD;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.shield;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.horizontalBeam) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationE;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.horizontalBeam;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScript.ActivePowerupParticles.Play ();
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.clone) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationF;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.Clone;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScript.ActivePowerupParticles.Play ();
				Destroy (gameObject);
			}
		}
	}
}

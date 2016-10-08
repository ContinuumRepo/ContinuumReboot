using UnityEngine;
using System.Collections;

public class PowerupScript : MonoBehaviour 
{
	private PlayerController playerControllerScript; // The player controller script component.
	// Powerups list.
	public enum poweruptype 
	{
		doubleShot, 
		triShot, 
		beamShot, 
		shield, 
		horizontalBeam, 
		clone, 
		helix, 
		wifi,
		health
		//ThreeD
	} 
	public poweruptype PowerupType; // To show the above enum.
	public GameObject Explosion; // The explosion to play on trigger enter.
	public bool useRandomPower;

	void Start () 
	{
		// Finds player controller component.
		playerControllerScript = GameObject.Find ("Player").GetComponent<PlayerController>();

		if (useRandomPower == true) 
		{
			PowerupType = (poweruptype)Random.Range (0, 7);
		}
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

			if (PowerupType == poweruptype.health) 
			{
				playerControllerScript.Health += 25;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				//Debug.Log ("Collected rare health woo!");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.doubleShot) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationA;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.DoubleShot;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScript.ActivePowerupParticles.Play ();
				//Debug.Log ("Double shot powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.triShot) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationB;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.TriShot;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScript.ActivePowerupParticles.Play ();
				//Debug.Log ("Tri shot powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.beamShot) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationC;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.BeamShot;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScript.ActivePowerupParticles.Play ();
				//Debug.Log ("Beam shot powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.shield) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationD;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.shield;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				//Debug.Log ("Shield powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.horizontalBeam) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationE;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.horizontalBeam;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScript.ActivePowerupParticles.Play ();
				//Debug.Log ("Horizontal beam powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.clone) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationF;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.Clone;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScript.ActivePowerupParticles.Play ();
				//Debug.Log ("Clone powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.helix) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationD;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.helix;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				//Debug.Log ("Helix shot powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.wifi) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationA;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.wifi;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScript.ActivePowerupParticles.Play ();
				//Debug.Log ("Ping wifi shot powerup collected.");
				Destroy (gameObject);
			}
		}
	}
}

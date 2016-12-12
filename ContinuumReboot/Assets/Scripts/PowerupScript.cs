using UnityEngine;
using System.Collections;

public class PowerupScript : MonoBehaviour 
{
	public poweruptype PowerupType; 

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
	} 

	public GameObject Explosion;
	public GameObject CloneObject;
	public bool useRandomPower;

	private PlayerController playerControllerScript;

	void Start () 
	{
		FindComponents ();

		if (useRandomPower == true) 
		{
			PowerupType = (poweruptype)Random.Range (0, 7);
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player" || other.tag == "Bullet") 
		{
			if (PowerupType == poweruptype.health) 
			{
				playerControllerScript.Health += 25;
				Instantiate (Explosion, gameObject.transform.position, Quaternion.Euler (0, 0, 45));
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.doubleShot) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationA;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.DoubleShot;
				Instantiate (Explosion, gameObject.transform.position, Quaternion.Euler (0, 0, 45));
				playerControllerScript.ActivePowerupParticles.Play ();
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.triShot) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationB;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.TriShot;
				Instantiate (Explosion, gameObject.transform.position, Quaternion.Euler (0, 0, 45));
				playerControllerScript.ActivePowerupParticles.Play ();
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.beamShot) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationC;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.BeamShot;
				Instantiate (Explosion, gameObject.transform.position, Quaternion.Euler (0, 0, 45));
				playerControllerScript.ActivePowerupParticles.Play ();
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.shield) 
			{
				GameObject.Find ("Shield").GetComponent<Animator> ().Play ("ShieldEntry");
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationD;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.shield;
				Instantiate (Explosion, gameObject.transform.position, Quaternion.Euler (0, 0, 45));
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.horizontalBeam) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationE;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.horizontalBeam;
				Instantiate (Explosion, gameObject.transform.position, Quaternion.Euler (0, 0, 45));
				playerControllerScript.ActivePowerupParticles.Play ();
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.clone) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationF;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.Clone;
				Instantiate (Explosion, gameObject.transform.position, Quaternion.Euler (0, 0, 45));
				playerControllerScript.ActivePowerupParticles.Play ();
				Instantiate (CloneObject, new Vector3 (0, -25, 0), Quaternion.Euler (90, 180, 0));
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.helix) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationD;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.helix;
				Instantiate (Explosion, gameObject.transform.position, Quaternion.Euler (0, 0, 45));
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.wifi) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationA;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.wifi;
				Instantiate (Explosion, gameObject.transform.position, Quaternion.Euler (0, 0, 45));
				playerControllerScript.ActivePowerupParticles.Play ();
				Destroy (gameObject);
			}
		}
	}

	void FindComponents ()
	{
		// Finds player controller component.
		playerControllerScript = GameObject.Find ("Player").GetComponent<PlayerController>();
	}
}
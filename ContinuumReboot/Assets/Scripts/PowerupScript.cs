using UnityEngine;
using System.Collections;

public class PowerupScript : MonoBehaviour 
{
	private PlayerController playerControllerScript; // The player controller script component.
	private PlayerController playerControllerScriptTwo; // The player controller script component.
	private PlayerController playerControllerScriptThree; // The player controller script component.
	private PlayerController playerControllerScriptFour; // The player controller script component.

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
		wifi 
	} 

	public poweruptype PowerupType; // To show the above enum.
	public GameObject Explosion; // The explosion to play on trigger enter.

	void Start () 
	{
		// Finds player controller component.
		playerControllerScript = GameObject.Find ("Player").GetComponent<PlayerController>();
		playerControllerScriptTwo = GameObject.Find ("PlayerTwo").GetComponent<PlayerController>();
		playerControllerScriptThree = GameObject.Find ("PlayerThree").GetComponent<PlayerController>();
		playerControllerScriptFour = GameObject.Find ("PlayerFour").GetComponent<PlayerController>();
	}

	void OnTriggerEnter (Collider other)
	{
		/// Summary ///
		/// if powerup type == X powerup
		/// set powerup time duration
		/// change powerup type in player script
		/// Instantiate explosion
		/// Turn on powerup particles on player
		/// Destroy this gameObject
		/// End Summary ///

		if (other.name == "PlayerFour") 
		{
			if (PowerupType == poweruptype.doubleShot) 
			{
				playerControllerScriptFour.powerupTime = playerControllerScriptFour.powerupDurationA;
				playerControllerScriptFour.CurrentPowerup = PlayerController.powerup.DoubleShot;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScript.ActivePowerupParticles.Play ();
				Debug.Log ("Double shot powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.triShot) 
			{
				playerControllerScriptFour.powerupTime = playerControllerScriptFour.powerupDurationB;
				playerControllerScriptFour.CurrentPowerup = PlayerController.powerup.TriShot;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScriptFour.ActivePowerupParticles.Play ();
				Debug.Log ("Tri shot powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.beamShot) 
			{
				playerControllerScriptFour.powerupTime = playerControllerScriptFour.powerupDurationC;
				playerControllerScriptFour.CurrentPowerup = PlayerController.powerup.BeamShot;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScriptFour.ActivePowerupParticles.Play ();
				Debug.Log ("Beam shot powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.shield) 
			{
				playerControllerScriptFour.powerupTime = playerControllerScriptFour.powerupDurationD;
				playerControllerScriptFour.CurrentPowerup = PlayerController.powerup.shield;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				Debug.Log ("Shield powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.horizontalBeam) 
			{
				playerControllerScriptFour.powerupTime = playerControllerScriptFour.powerupDurationE;
				playerControllerScriptFour.CurrentPowerup = PlayerController.powerup.horizontalBeam;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScriptFour.ActivePowerupParticles.Play ();
				Debug.Log ("Horizontal beam powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.clone) 
			{
				playerControllerScriptFour.powerupTime = playerControllerScriptFour.powerupDurationF;
				playerControllerScriptFour.CurrentPowerup = PlayerController.powerup.Clone;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScriptFour.ActivePowerupParticles.Play ();
				Debug.Log ("Clone powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.helix) 
			{
				playerControllerScriptFour.powerupTime = playerControllerScriptFour.powerupDurationD;
				playerControllerScriptFour.CurrentPowerup = PlayerController.powerup.helix;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				Debug.Log ("Helix shot powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.wifi) 
			{
				playerControllerScriptFour.powerupTime = playerControllerScriptFour.powerupDurationA;
				playerControllerScriptFour.CurrentPowerup = PlayerController.powerup.wifi;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScriptFour.ActivePowerupParticles.Play ();
				Debug.Log ("Ping wifi shot powerup collected.");
				Destroy (gameObject);
			}
		}

		if (other.name == "PlayerThree") 
		{
			if (PowerupType == poweruptype.doubleShot) 
			{
				playerControllerScriptThree.powerupTime = playerControllerScriptThree.powerupDurationA;
				playerControllerScriptThree.CurrentPowerup = PlayerController.powerup.DoubleShot;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScript.ActivePowerupParticles.Play ();
				Debug.Log ("Double shot powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.triShot) 
			{
				playerControllerScriptThree.powerupTime = playerControllerScriptThree.powerupDurationB;
				playerControllerScriptThree.CurrentPowerup = PlayerController.powerup.TriShot;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScriptThree.ActivePowerupParticles.Play ();
				Debug.Log ("Tri shot powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.beamShot) 
			{
				playerControllerScriptThree.powerupTime = playerControllerScriptThree.powerupDurationC;
				playerControllerScriptThree.CurrentPowerup = PlayerController.powerup.BeamShot;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScriptThree.ActivePowerupParticles.Play ();
				Debug.Log ("Beam shot powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.shield) 
			{
				playerControllerScriptThree.powerupTime = playerControllerScriptThree.powerupDurationD;
				playerControllerScriptThree.CurrentPowerup = PlayerController.powerup.shield;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				Debug.Log ("Shield powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.horizontalBeam) 
			{
				playerControllerScriptThree.powerupTime = playerControllerScriptThree.powerupDurationE;
				playerControllerScriptThree.CurrentPowerup = PlayerController.powerup.horizontalBeam;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScriptThree.ActivePowerupParticles.Play ();
				Debug.Log ("Horizontal beam powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.clone) 
			{
				playerControllerScriptThree.powerupTime = playerControllerScriptThree.powerupDurationF;
				playerControllerScriptThree.CurrentPowerup = PlayerController.powerup.Clone;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScriptThree.ActivePowerupParticles.Play ();
				Debug.Log ("Clone powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.helix) 
			{
				playerControllerScriptThree.powerupTime = playerControllerScriptThree.powerupDurationD;
				playerControllerScriptThree.CurrentPowerup = PlayerController.powerup.helix;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				Debug.Log ("Helix shot powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.wifi) 
			{
				playerControllerScriptThree.powerupTime = playerControllerScriptThree.powerupDurationA;
				playerControllerScriptThree.CurrentPowerup = PlayerController.powerup.wifi;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScriptThree.ActivePowerupParticles.Play ();
				Debug.Log ("Ping wifi shot powerup collected.");
				Destroy (gameObject);
			}
		}

		if (other.name == "PlayerTwo") 
		{
			if (PowerupType == poweruptype.doubleShot) 
			{
				playerControllerScriptTwo.powerupTime = playerControllerScriptTwo.powerupDurationA;
				playerControllerScriptTwo.CurrentPowerup = PlayerController.powerup.DoubleShot;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScript.ActivePowerupParticles.Play ();
				Debug.Log ("Double shot powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.triShot) 
			{
				playerControllerScriptTwo.powerupTime = playerControllerScriptTwo.powerupDurationB;
				playerControllerScriptTwo.CurrentPowerup = PlayerController.powerup.TriShot;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScriptTwo.ActivePowerupParticles.Play ();
				Debug.Log ("Tri shot powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.beamShot) 
			{
				playerControllerScriptTwo.powerupTime = playerControllerScriptTwo.powerupDurationC;
				playerControllerScriptTwo.CurrentPowerup = PlayerController.powerup.BeamShot;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScriptTwo.ActivePowerupParticles.Play ();
				Debug.Log ("Beam shot powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.shield) 
			{
				playerControllerScriptTwo.powerupTime = playerControllerScriptTwo.powerupDurationD;
				playerControllerScriptTwo.CurrentPowerup = PlayerController.powerup.shield;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				Debug.Log ("Shield powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.horizontalBeam) 
			{
				playerControllerScriptTwo.powerupTime = playerControllerScriptTwo.powerupDurationE;
				playerControllerScriptTwo.CurrentPowerup = PlayerController.powerup.horizontalBeam;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScriptTwo.ActivePowerupParticles.Play ();
				Debug.Log ("Horizontal beam powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.clone) 
			{
				playerControllerScriptTwo.powerupTime = playerControllerScriptTwo.powerupDurationF;
				playerControllerScriptTwo.CurrentPowerup = PlayerController.powerup.Clone;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScriptTwo.ActivePowerupParticles.Play ();
				Debug.Log ("Clone powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.helix) 
			{
				playerControllerScriptTwo.powerupTime = playerControllerScriptTwo.powerupDurationD;
				playerControllerScriptTwo.CurrentPowerup = PlayerController.powerup.helix;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				Debug.Log ("Helix shot powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.wifi) 
			{
				playerControllerScriptTwo.powerupTime = playerControllerScriptTwo.powerupDurationA;
				playerControllerScriptTwo.CurrentPowerup = PlayerController.powerup.wifi;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScriptTwo.ActivePowerupParticles.Play ();
				Debug.Log ("Ping wifi shot powerup collected.");
				Destroy (gameObject);
			}
		}

		if (other.tag == "Bullet") 
		{
			if (PowerupType == poweruptype.doubleShot) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationA;
				playerControllerScriptTwo.powerupTime = playerControllerScriptTwo.powerupDurationA;
				playerControllerScriptThree.powerupTime = playerControllerScriptThree.powerupDurationA;
				playerControllerScriptFour.powerupTime = playerControllerScriptFour.powerupDurationA;

				playerControllerScript.CurrentPowerup = PlayerController.powerup.DoubleShot;
				playerControllerScriptTwo.CurrentPowerup = PlayerController.powerup.DoubleShot;
				playerControllerScriptThree.CurrentPowerup = PlayerController.powerup.DoubleShot;
				playerControllerScriptFour.CurrentPowerup = PlayerController.powerup.DoubleShot;

				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);

				playerControllerScript.ActivePowerupParticles.Play ();
				playerControllerScriptTwo.ActivePowerupParticles.Play ();
				playerControllerScriptThree.ActivePowerupParticles.Play ();
				playerControllerScriptFour.ActivePowerupParticles.Play ();

				Debug.Log ("Double shot powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.triShot) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationB;
				playerControllerScriptTwo.powerupTime = playerControllerScriptTwo.powerupDurationB;
				playerControllerScriptThree.powerupTime = playerControllerScriptThree.powerupDurationB;
				playerControllerScriptFour.powerupTime = playerControllerScriptFour.powerupDurationB;

				playerControllerScript.CurrentPowerup = PlayerController.powerup.TriShot;
				playerControllerScriptTwo.CurrentPowerup = PlayerController.powerup.TriShot;
				playerControllerScriptThree.CurrentPowerup = PlayerController.powerup.TriShot;
				playerControllerScriptFour.CurrentPowerup = PlayerController.powerup.TriShot;

				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);

				playerControllerScript.ActivePowerupParticles.Play ();
				playerControllerScriptTwo.ActivePowerupParticles.Play ();
				playerControllerScriptThree.ActivePowerupParticles.Play ();
				playerControllerScriptFour.ActivePowerupParticles.Play ();

				Debug.Log ("Tri shot powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.beamShot) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationC;
				playerControllerScriptTwo.powerupTime = playerControllerScriptTwo.powerupDurationC;
				playerControllerScriptThree.powerupTime = playerControllerScriptThree.powerupDurationC;
				playerControllerScriptFour.powerupTime = playerControllerScriptFour.powerupDurationC;

				playerControllerScript.CurrentPowerup = PlayerController.powerup.BeamShot;
				playerControllerScriptTwo.CurrentPowerup = PlayerController.powerup.BeamShot;
				playerControllerScriptThree.CurrentPowerup = PlayerController.powerup.BeamShot;
				playerControllerScriptFour.CurrentPowerup = PlayerController.powerup.BeamShot;

				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);

				playerControllerScript.ActivePowerupParticles.Play ();
				playerControllerScriptTwo.ActivePowerupParticles.Play ();
				playerControllerScriptThree.ActivePowerupParticles.Play ();
				playerControllerScriptFour.ActivePowerupParticles.Play ();

				Debug.Log ("Beam shot powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.shield) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationD;
				playerControllerScriptTwo.powerupTime = playerControllerScriptTwo.powerupDurationD;
				playerControllerScriptThree.powerupTime = playerControllerScriptThree.powerupDurationD;
				playerControllerScriptFour.powerupTime = playerControllerScriptFour.powerupDurationD;

				playerControllerScript.CurrentPowerup = PlayerController.powerup.shield;
				playerControllerScriptTwo.CurrentPowerup = PlayerController.powerup.shield;
				playerControllerScriptThree.CurrentPowerup = PlayerController.powerup.shield;
				playerControllerScriptFour.CurrentPowerup = PlayerController.powerup.shield;

				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);

				playerControllerScript.ActivePowerupParticles.Play ();
				playerControllerScriptTwo.ActivePowerupParticles.Play ();
				playerControllerScriptThree.ActivePowerupParticles.Play ();
				playerControllerScriptFour.ActivePowerupParticles.Play ();

				Debug.Log ("Shield powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.horizontalBeam) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationE;
				playerControllerScriptTwo.powerupTime = playerControllerScriptTwo.powerupDurationE;
				playerControllerScriptThree.powerupTime = playerControllerScriptThree.powerupDurationE;
				playerControllerScriptFour.powerupTime = playerControllerScriptFour.powerupDurationE;

				playerControllerScript.CurrentPowerup = PlayerController.powerup.horizontalBeam;
				playerControllerScriptTwo.CurrentPowerup = PlayerController.powerup.horizontalBeam;
				playerControllerScriptThree.CurrentPowerup = PlayerController.powerup.horizontalBeam;
				playerControllerScriptFour.CurrentPowerup = PlayerController.powerup.horizontalBeam;

				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);

				playerControllerScript.ActivePowerupParticles.Play ();
				playerControllerScriptTwo.ActivePowerupParticles.Play ();
				playerControllerScriptThree.ActivePowerupParticles.Play ();
				playerControllerScriptFour.ActivePowerupParticles.Play ();

				Debug.Log ("Horizontal beam powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.clone) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationF;
				playerControllerScriptTwo.powerupTime = playerControllerScriptTwo.powerupDurationF;
				playerControllerScriptThree.powerupTime = playerControllerScriptThree.powerupDurationF;
				playerControllerScriptFour.powerupTime = playerControllerScriptFour.powerupDurationF;

				playerControllerScript.CurrentPowerup = PlayerController.powerup.Clone;
				playerControllerScriptTwo.CurrentPowerup = PlayerController.powerup.Clone;
				playerControllerScriptThree.CurrentPowerup = PlayerController.powerup.Clone;
				playerControllerScriptFour.CurrentPowerup = PlayerController.powerup.Clone;

				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);

				playerControllerScript.ActivePowerupParticles.Play ();
				playerControllerScriptTwo.ActivePowerupParticles.Play ();
				playerControllerScriptThree.ActivePowerupParticles.Play ();
				playerControllerScriptFour.ActivePowerupParticles.Play ();

				Debug.Log ("Clone powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.helix) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationD;
				playerControllerScriptTwo.powerupTime = playerControllerScriptTwo.powerupDurationD;
				playerControllerScriptThree.powerupTime = playerControllerScriptThree.powerupDurationD;
				playerControllerScriptFour.powerupTime = playerControllerScriptFour.powerupDurationD;

				playerControllerScript.CurrentPowerup = PlayerController.powerup.helix;
				playerControllerScriptTwo.CurrentPowerup = PlayerController.powerup.helix;
				playerControllerScriptThree.CurrentPowerup = PlayerController.powerup.helix;
				playerControllerScriptFour.CurrentPowerup = PlayerController.powerup.helix;

				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);

				playerControllerScript.ActivePowerupParticles.Play ();
				playerControllerScriptTwo.ActivePowerupParticles.Play ();
				playerControllerScriptThree.ActivePowerupParticles.Play ();
				playerControllerScriptFour.ActivePowerupParticles.Play ();

				Debug.Log ("Helix shot powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.wifi) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationA;
				playerControllerScriptTwo.powerupTime = playerControllerScriptTwo.powerupDurationA;
				playerControllerScriptThree.powerupTime = playerControllerScriptThree.powerupDurationA;
				playerControllerScriptFour.powerupTime = playerControllerScriptFour.powerupDurationA;

				playerControllerScript.CurrentPowerup = PlayerController.powerup.wifi;
				playerControllerScriptTwo.CurrentPowerup = PlayerController.powerup.wifi;
				playerControllerScriptThree.CurrentPowerup = PlayerController.powerup.wifi;
				playerControllerScriptFour.CurrentPowerup = PlayerController.powerup.wifi;

				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);

				playerControllerScript.ActivePowerupParticles.Play ();
				playerControllerScriptTwo.ActivePowerupParticles.Play ();
				playerControllerScriptThree.ActivePowerupParticles.Play ();
				playerControllerScriptFour.ActivePowerupParticles.Play ();

				Debug.Log ("Ping wifi shot powerup collected.");
				Destroy (gameObject);
			}
		}

		if (other.name == "Player")
		{
			if (PowerupType == poweruptype.doubleShot) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationA;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.DoubleShot;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScript.ActivePowerupParticles.Play ();
				Debug.Log ("Double shot powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.triShot) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationB;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.TriShot;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScript.ActivePowerupParticles.Play ();
				Debug.Log ("Tri shot powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.beamShot) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationC;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.BeamShot;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScript.ActivePowerupParticles.Play ();
				Debug.Log ("Beam shot powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.shield) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationD;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.shield;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				Debug.Log ("Shield powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.horizontalBeam) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationE;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.horizontalBeam;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScript.ActivePowerupParticles.Play ();
				Debug.Log ("Horizontal beam powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.clone) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationF;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.Clone;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScript.ActivePowerupParticles.Play ();
				Debug.Log ("Clone powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.helix) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationD;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.helix;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				Debug.Log ("Helix shot powerup collected.");
				Destroy (gameObject);
			}

			if (PowerupType == poweruptype.wifi) 
			{
				playerControllerScript.powerupTime = playerControllerScript.powerupDurationA;
				playerControllerScript.CurrentPowerup = PlayerController.powerup.wifi;
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				playerControllerScript.ActivePowerupParticles.Play ();
				Debug.Log ("Ping wifi shot powerup collected.");
				Destroy (gameObject);
			}
		}
	}
}

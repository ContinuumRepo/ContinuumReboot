using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using UnityStandardAssets.ImageEffects;

public class PointObject : MonoBehaviour 
{
	private PlayerController PlayerControllerScript; // The Player Controller script.
	private GameController gameControllerScript; // The GameController script.

	public GameObject Explosion; // Instantiated explosion.
	public enum type {Orange, Yellow, Green, Cyan, Purple}; // Cube types.
	public type PointType;
	public float PointReward = 150; // Rewards the player this many points when a bullet hits it.
	public ParticleSystem MainEngineParticles; // Engine Particle system.
	public GameObject PlayerExplosion; // The explosion when the player hits it.
	public float Damage = 25.0f; // Damage amount to player.

	// Combo particle systems.
	public ParticleSystem ComboOne;
	public ParticleSystem ComboTwo;
	public ParticleSystem ComboThree;
	public ParticleSystem ComboFour;
	public ParticleSystem ComboFive;

	void Start ()
	{
		// Finding the main engine particle system GameObject.
		GameObject MainEngine = GameObject.FindGameObjectWithTag ("MainEngine");
		MainEngineParticles = MainEngine.GetComponent<ParticleSystem> ();

		// Finds player controller script and assigns to private variable.
		PlayerControllerScript = GameObject.Find ("Player").GetComponent<PlayerController>();

		// Finds GameController script and assigns to private variable.
		gameControllerScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();


		// Finds Combo Particle System game objects in Scene (Should be attached as a child of the "Player" GameObject).
		ComboOne = GameObject.FindGameObjectWithTag ("ComboOrangeParticles").GetComponent<ParticleSystem>();
		ComboTwo = GameObject.FindGameObjectWithTag ("ComboYellowParticles").GetComponent<ParticleSystem>();
		ComboThree = GameObject.FindGameObjectWithTag ("ComboGreenParticles").GetComponent<ParticleSystem>();
		ComboFour = GameObject.FindGameObjectWithTag ("ComboCyanParticles").GetComponent<ParticleSystem>();
		ComboFive = GameObject.FindGameObjectWithTag ("ComboPurpleParticles").GetComponent<ParticleSystem>();

	}

	void Update () 
	{
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Bullet") 
		{
			// Creates explosion.
			Instantiate (Explosion, transform.position, transform.rotation);

			if (PointType == type.Orange) 
			{
				// Turns the engine color to orange.
				MainEngineParticles.startColor = new Color (0.78f, 0.33f, 0, 1);
				gameControllerScript.CurrentScore += PointReward * Time.timeScale;
				//ComboOne.Play ();
				ComboOne.startColor = new Color (0.78f, 0.33f, 0, 1);
				ComboTwo.startColor = new Color (0.78f, 0.33f, 0, 1);
				ComboThree.startColor = new Color (0.78f, 0.33f, 0, 1);
				ComboFour.startColor = new Color (0.78f, 0.33f, 0, 1);
				ComboFive.startColor = new Color (0.78f, 0.33f, 0, 1);
			}

			if (PointType == type.Yellow) 
			{
				// Turns the engine color to yellow.
				MainEngineParticles.startColor = new Color (1, 1, 0, 1);
				gameControllerScript.CurrentScore += PointReward * 2 * Time.timeScale;
				//ComboTwo.Play ();
				ComboOne.startColor = new Color (1, 1, 0, 1);
				ComboTwo.startColor = new Color (1, 1, 0, 1);
				ComboThree.startColor = new Color (1, 1, 0, 1);
				ComboFour.startColor = new Color (1, 1, 0, 1);
				ComboFive.startColor = new Color (1, 1, 0, 1);
			}

			if (PointType == type.Green) 
			{
				// Turns the engine color to green.
				MainEngineParticles.startColor = new Color (0, 1, 0, 1);
				gameControllerScript.CurrentScore += PointReward * 3 * Time.timeScale;
				//ComboThree.Play ();
				ComboOne.startColor = new Color (0, 1, 0, 1);
				ComboTwo.startColor = new Color (0, 1, 0, 1);
				ComboThree.startColor = new Color (0, 1, 0, 1);
				ComboFour.startColor = new Color (0, 1, 0, 1);
				ComboFive.startColor = new Color (0, 1, 0, 1);
			}

			if (PointType == type.Cyan) 
			{
				// Turns the engine color to cyan.
				MainEngineParticles.startColor = new Color (0, 1, 1, 1);
				gameControllerScript.CurrentScore += PointReward * 4 * Time.timeScale;
				//ComboFour.Play ();
				ComboOne.startColor = new Color (0, 1, 1, 1);
				ComboTwo.startColor = new Color (0, 1, 1, 1);
				ComboThree.startColor = new Color (0, 1, 1, 1);
				ComboFour.startColor = new Color (0, 1, 1, 1);
				ComboFive.startColor = new Color (0, 1, 1, 1);
			}

			if (PointType == type.Purple) 
			{
				// Turns the engine color to purple.
				MainEngineParticles.startColor = new Color (0.39f, 0, 1, 1);
				gameControllerScript.CurrentScore += PointReward * 5 * Time.timeScale;
				//ComboFive.Play ();
				ComboOne.startColor = new Color (0.39f, 0, 1, 1);
				ComboTwo.startColor = new Color (0.39f, 0, 1, 1);
				ComboThree.startColor = new Color (0.39f, 0, 1, 1);
				ComboFour.startColor = new Color (0.39f, 0, 1, 1);
				ComboFive.startColor = new Color (0.39f, 0, 1, 1);
			}

			Destroy (gameObject); // Destroys the gameObject.
		}

		if (other.tag == "Player")
		{	
			if (PlayerControllerScript.Health > 25)
			{
				// Finds Points to destroy.
				GameObject[] Destroyers = GameObject.FindGameObjectsWithTag ("Cube");
				for (int i = Destroyers.Length - 1; i > 0; i--) {
					Destroy (Destroyers [i].gameObject);
				}
			}

			Instantiate (PlayerExplosion, transform.position, transform.rotation);
			PlayerControllerScript.Health -= Damage;

			PlayerControllerScript.ColorCorrectionCurvesScript.saturation = 0;

			if (PlayerControllerScript.Health <= 10) 
			{
				Instantiate (PlayerControllerScript.gameOverExplosion, gameObject.transform.position, Quaternion.identity);
			}
		}
	}
}

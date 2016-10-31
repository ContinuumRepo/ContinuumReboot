using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;

public class PointObject : MonoBehaviour 
{
	private PlayerController PlayerControllerScript; // The Player Controller script.
	private GameController gameControllerScript; // The GameController script.
	public MeshRenderer playerMesh;
	public GameObject Explosion; // Instantiated explosion.
	public enum type {Orange, Yellow, Green, Cyan, Purple}; // Cube types.
	public type PointType; // To show above enum in inspector.
	public float PointReward = 150; // Rewards the player this many points when a bullet hits it.
	public ParticleSystem MainEngineParticles; // Engine Particle system.
	public GameObject PlayerExplosion; // The explosion when the player hits it.
	public float Damage = 25.0f; // Damage amount to player.
	public Animator ScoreText;
	public Material normalMaterial;
	public Material orangeMaterial;
	public Material yellowMaterial;
	public Material greenMaterial;
	public Material cyanMaterial;
	public Material pinkMaterial;
	public Material redMaterial;
	public TimescaleController timeController;
	public Text ComboText;
	public bool isBossPart;
	//private SunShafts sunShaftsScript;

	void Start ()
	{
		ComboText = GameObject.Find ("ComboText").GetComponent<Text> ();
		ScoreText = GameObject.FindGameObjectWithTag ("ScoreText").GetComponent<Animator> ();
		playerMesh = GameObject.FindGameObjectWithTag ("PlayerMesh").GetComponent<MeshRenderer> ();
		//sunShaftsScript = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<SunShafts> ();

		// Finding the main engine particle system GameObject.
		GameObject MainEngine = GameObject.FindGameObjectWithTag ("MainEngine");
		MainEngineParticles = MainEngine.GetComponent<ParticleSystem> ();

		// Finds player controller script and assigns to private variable.
		PlayerControllerScript = GameObject.Find ("Player").GetComponent<PlayerController>();

		// Finds GameController script and assigns to private variable.
		gameControllerScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();

		timeController = GameObject.FindGameObjectWithTag ("TimeScaleController").GetComponent<TimescaleController> ();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Bullet") 
		{
			// Creates explosion.
			Instantiate (Explosion, transform.position, transform.rotation);
			ScoreText.Play ("Scoretext");

			// Orange brick.
			if (PointType == type.Orange) 
			{
				playerMesh.material = orangeMaterial;
				// Turns the engine color to orange.
				MainEngineParticles.startColor = new Color (0.78f, 0.33f, 0, 1);
				gameControllerScript.CurrentScore += PointReward * Time.timeScale * PlayerControllerScript.ComboN;
				PlayerControllerScript.ComboAnimation.Play (0);
				ComboText.color = new Color (1, 0.5f, 0);
				//sunShaftsScript.sunColor = new Color (0.78f, 0.33f, 0, 1);
			}

			// Yellow brick.
			if (PointType == type.Yellow) 
			{
				playerMesh.material = yellowMaterial;
				// Turns the engine color to yellow.
				MainEngineParticles.startColor = new Color (1, 1, 0, 1);
				gameControllerScript.CurrentScore += PointReward * 2 * Time.timeScale * PlayerControllerScript.ComboN;
				PlayerControllerScript.ComboAnimation.Play (0);
				ComboText.color = new Color (1, 1, 0);
				//sunShaftsScript.sunColor = new Color (1, 1, 0, 1);
			}

			// Green brick.
			if (PointType == type.Green) 
			{
				playerMesh.material = greenMaterial;
				// Turns the engine color to green.
				MainEngineParticles.startColor = new Color (0, 1, 0, 1);
				gameControllerScript.CurrentScore += PointReward * 3 * Time.timeScale * PlayerControllerScript.ComboN;
				PlayerControllerScript.ComboAnimation.Play (0);
				ComboText.color = new Color (0, 1, 0);
				//sunShaftsScript.sunColor = new Color (0, 1, 0, 1);
			}

			// Cyan brick.
			if (PointType == type.Cyan) 
			{
				playerMesh.material = cyanMaterial;
				// Turns the engine color to cyan.
				MainEngineParticles.startColor = new Color (0, 1, 1, 1);
				gameControllerScript.CurrentScore += PointReward * 4 * Time.timeScale * PlayerControllerScript.ComboN;
				PlayerControllerScript.ComboAnimation.Play (0);
				ComboText.color = new Color (0, 1, 1);
				//sunShaftsScript.sunColor = new Color (0, 1, 1, 1);
			}

			// Purple brick.
			if (PointType == type.Purple) 
			{
				playerMesh.material = pinkMaterial;
				// Turns the engine color to purple.
				MainEngineParticles.startColor = new Color (0.39f, 0, 1, 1);
				gameControllerScript.CurrentScore += PointReward * 5 * Time.timeScale * PlayerControllerScript.ComboN;
				PlayerControllerScript.ComboAnimation.Play (0);
				ComboText.color = new Color (0.9f, 0.2f, 1);
				//sunShaftsScript.sunColor = new Color (0.8f, 0.25f, 1, 1);
			}

			Destroy (gameObject); // Destroys the gameObject.
		}

		// When a brick hits the player.
		if (other.tag == "Player") 
		{		
			//sunShaftsScript.sunColor = new Color (0.5f, 1, 1, 1);
			playerMesh.material = normalMaterial;

			// If player health is greater than 25.
			if (gameObject.tag == "Cube") 
			{
				if (PlayerControllerScript.Health > 25) 
				{
					PlayerControllerScript.OverlayTime = 2;
					//PlayerControllerScript.Overlay.Play ("Overlay");
					// Finds Points to destroy.
					GameObject[] Destroyers = GameObject.FindGameObjectsWithTag ("Cube");
					for (int i = Destroyers.Length - 1; i > 0; i--) 
					{
						Destroy (Destroyers [i].gameObject);
					}
				}
			}

			PlayerControllerScript.collisionCooldown = 3;

			if (PlayerControllerScript.Health > 10) 
			{
				// Instantiates a larger explosion.
				Instantiate (PlayerExplosion, transform.position, transform.rotation);
			}

			// Gives damage to player.
			PlayerControllerScript.Health -= Damage;

			if (PlayerControllerScript.Health > 25)
			{
				/*
			// Desaturates screen.
				PlayerControllerScript.ColorCorrectionCurvesScript.saturation = 0;
				*/
			}

			// If player health is less than 10.
			if (PlayerControllerScript.Health <= 10) 
			{
				// Instantiate huge explosion.
				Instantiate (PlayerControllerScript.gameOverExplosion, gameObject.transform.position, Quaternion.identity);
			}
		}
	}
}
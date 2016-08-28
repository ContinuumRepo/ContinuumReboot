using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;
using XInputDotNetPure;

public class BulletScript : MonoBehaviour 
{
	[Header ("External")]
	private AutoMoveAndRotate MoveAndRotateScript; // Auto Move and Rotate component.
	public float newSpeed = -70.0f; // New speed when ricoshet.
	private CameraShake camShakeScript; // The camera shake component.
	public float InitialShakeDuration = 0.25f; // Shake time.
	public float InitialShakeStrength = 0.5f; // Shake strength.
	private GameController gameControllerScript; // Game Controller component.
	public TimescaleController timeScaleControllerScript; // Time scale controller component.

	[Header ("Costing")]
	public GameObject BulletNoCost; // The bullet prefab that doesn't cost points.
	public enum bulletcost {FixedRate, Percentage} // Bullet cost type.
	public bulletcost BulletCostType; // The above enum.
	public float DecrementPortion = 0.1f; // In percentage (0.1 means 10% cost, 0.9 means 90% cost, Citric Jungle isnt that harsh ;) ).
	public float DecrementAmount = 100.0f; // Decrement points constant.

	[Header ("Audio")]
	public AudioSource[] Oneshots; // Array of audio clips.
	public int PlayElement; // Play element for audio.
	public AudioSource BeamExplosion; // Explosion sound when a beam hits an object.

	[Header ("Ricoshet")]
	public int ricoshet; // Ricoshet number.
	public int ricoshetMax = 6; // The max amount of ricoshets before the gameObject is destroyed.
	public ParticleSystem[] RicoshetParticle; // Particle combos, should be children of the Player GameObject.
	public float VibrationTime = 0.04f; // How long should the vibrationh occur?
	public bool isShield; // Is this bullet a shield instead?
	public bool isHorizontal; // Is the beam a horizontal one?
	public bool useRandomRotation = true; // Does the bullet need a random rotation after a trigger enter?
	public bool goThroughAnything;
	public bool followers;
	public GameObject PrefabWifi;
	public AudioSource WifiAudio;
	public bool wifiAudio;

	void Start () 
	{
		// Finds camera shake component.
		camShakeScript = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraShake> ();

		// Sets shake duration and strength.
		camShakeScript.shakeDuration = InitialShakeDuration;
		camShakeScript.shakeAmount = InitialShakeStrength;

		// Sets vibration time
		VibrationTime = 0.04f;

		// Sets start play element and ricoshet number.
		PlayElement = 0;
		ricoshet = 0;

		// Finds auto move and rotate component.
		MoveAndRotateScript = GetComponent<AutoMoveAndRotate> ();

		// Finds Game Controller script component.
		gameControllerScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();

		// Finds Time scale controller component.
		timeScaleControllerScript = GameObject.FindGameObjectWithTag ("TimeScaleController").GetComponent<TimescaleController>();

		// Finds combo particle system GameObjects in Scene (Should be attached as a child of the "Player" GameObject).
		RicoshetParticle[0] = GameObject.FindGameObjectWithTag ("ComboOrangeParticles").GetComponent<ParticleSystem>();
		RicoshetParticle[1] = GameObject.FindGameObjectWithTag ("ComboYellowParticles").GetComponent<ParticleSystem>();
		RicoshetParticle[2] = GameObject.FindGameObjectWithTag ("ComboGreenParticles").GetComponent<ParticleSystem>();
		RicoshetParticle[3] = GameObject.FindGameObjectWithTag ("ComboCyanParticles").GetComponent<ParticleSystem>();
		RicoshetParticle[4] = GameObject.FindGameObjectWithTag ("ComboPurpleParticles").GetComponent<ParticleSystem>();

		WifiAudio = GameObject.FindGameObjectWithTag ("WifiAudio").GetComponent<AudioSource> ();

		// If the bullet cost is in percentage mode.
		if (BulletCostType == bulletcost.Percentage)
		{
			gameControllerScript.CurrentScore = gameControllerScript.CurrentScore - (DecrementPortion * gameControllerScript.CurrentScore);
		}

		// If the bullet cost is in constant mode.
		if (BulletCostType == bulletcost.FixedRate) 
		{
			gameControllerScript.CurrentScore -= DecrementAmount;
		}
	}

	void Update ()
	{
		// If remaining vibration time is greater than 0.
		if (VibrationTime > 0) 
		{
			GamePad.SetVibration (PlayerIndex.One, 0, 0.25f); // Sets vibration amount ot 25%.
			VibrationTime -= Time.fixedDeltaTime; // Continuously decreases vibration time left.
		}

		// If remaining vibration time is less than or equal to 0.
		if (VibrationTime <= 0) 
		{
			GamePad.SetVibration (PlayerIndex.One, 0, 0); // Sets controller to not vibrate.
			VibrationTime = 0; // Make it equal to 0.
		}
	}

	void OnTriggerEnter (Collider other)
	{
		// Triggers with Brick or Cube tags.
		if (other.tag == "Brick" || other.tag == "Cube") 
		{
			// Sets shake duration and strength.
			camShakeScript.shakeDuration = InitialShakeDuration;
			camShakeScript.shakeAmount = InitialShakeStrength;

			if (wifiAudio && ricoshetMax == 1) 
			{
				WifiAudio.Play ();
				ricoshet += 1;
				Instantiate (PrefabWifi, gameObject.transform.position, Quaternion.Euler (0, 0, Random.Range (-360, 360)));
				Destroy (gameObject);
			}

			if (wifiAudio && ricoshetMax == 0) 
			{
				WifiAudio.Play ();
				Destroy (gameObject);
			}

			// If the object has useRandomRotation boolean enabled.
			if (useRandomRotation == true) 
			{
				// If ricoshets are less than the max ricoshet amount.
				if (ricoshet < ricoshetMax) 
				{
					// Gives new random rotation.
					gameObject.transform.rotation = Quaternion.Euler (0.0f, 0.0f, Random.Range (-360, 360));

					// Gives new speed.
					MoveAndRotateScript.moveUnitsPerSecond.value = new Vector3 (0.0f, newSpeed, 0.0f);

					// Instantiates sound objects which detroy after a second or so.
					Instantiate (Oneshots [PlayElement], Vector3.zero, Quaternion.identity);

					// Increases audio play element number and ricoshet number.
					PlayElement += 1;
					ricoshet += 1;

					if (followers == false) 
					{
						// Instantiates a no cost bullet giving that a random rotation also.
						Instantiate (BulletNoCost, gameObject.transform.position, Quaternion.Euler (0, 0, Random.Range (-360, 360)));
					}

					// Plays previous particle element.
					RicoshetParticle [PlayElement - 1].Play ();

					// Sets vibration time.
					VibrationTime = 0.04f;

					//timeScaleControllerScript.enabled = true; // Don't know if I need this.
				}

				// If the gameObject reaches maximum ricoshet.
				if (ricoshet >= ricoshetMax && goThroughAnything == false) 
				{
					Destroy (gameObject); // Destroys it.
				}
		
				// If the gameObject reaches more audio elements than ricoshets
				if (PlayElement >= ricoshetMax && goThroughAnything == true) 
				{
					PlayElement -= 1;
					ricoshet -= 1;
				}
			}

			// If the object has useRandomRotation boolean disabled.
			if (useRandomRotation == false && goThroughAnything == false && followers == false) 
			{
				PlayElement = 0; // Resets play element to 0.
				//Instantiate (Oneshots [PlayElement], Vector3.zero, Quaternion.identity); // Instantiates the assigned audio source.
				BeamExplosion.Play (); // Plays explosion particle effect.
			}

			// If the object has useRandomRotation boolean disabled.
			if (useRandomRotation == false && goThroughAnything == true && followers == true) 
			{
				// If the gameObject reaches more audio elements than ricoshets
				if (PlayElement >= ricoshetMax) 
				{
					PlayElement -= 2;
					ricoshet -= 2;
				}

				// If the gameObject reaches more audio elements than ricoshets
				if (PlayElement < ricoshetMax) 
				{
					PlayElement += 1;
					ricoshet += 1;
				}
				//PlayElement += 1; // Resets play element to 0.
				Oneshots [PlayElement].Play ();
				//BeamExplosion.Play (); // Plays explosion particle effect.
				Debug.Log ("Played.");
			}
		}

		if (other.tag == "Brick" || other.tag == "Cube" && ricoshet >= ricoshetMax) 
		{
			// Nothing to do here. Carry on..
		}

		if (other.tag == "Barrier" && isHorizontal == false && isShield == false) 
		{
			// Gives object a new random rotation.
			gameObject.transform.rotation = Quaternion.Euler (0.0f, 0.0f, Random.Range (-360, 360));
		}
	}
}
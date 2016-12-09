using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class PlayerController : MonoBehaviour 
{
	public playerNumber PlayerNumber;

	public enum playerNumber 
	{
		PlayerOne, 
		PlayerTwo, 
		PlayerThree, 
		PlayerFour
	}

	[Header ("Movement")]
	public float speed = 10.0f; 								// The overall speed the player can move (sensitivity).
	public float tilt = 0.0f;  									// The amount the player can tilt (default = 0).
	public bool useKeyboardControls;
	private Rigidbody rb; 										// The attached rigidbody component.

	[Header ("Boundary")]
	public float xBoundLower = -20.0f;
	public float xBoundUpper = 20.0f;
	public float yBoundLower = -20.0f;
	public float yBoundUpper = 20.0f;
	public float zBound = 0;

	[Header ("Shooting")]
	public GameObject shot; 									// The main shot GameObject.
	public Transform shotSpawn; 								// Where the shot will be placed when instantiated.
	public float fireRate;	    								// Time between bullets before a new one is instantiated.
	private float nextFire; 
	public float altfireRate;
	private float altnextFire;

	public enum altmode
	{
		yes, no
	}

	public altmode AltFireMode;

	public GameObject AltFire;
	public Image AltFireImage;
	public GameObject AltFireIndicator;

	[Header ("Powerups")]
	public float powerupTime = 0; 								// The current powerup time left.

	public enum powerup 										// The different types of powerups.
	{
		RegularShot, 
		DoubleShot, 
		TriShot, 
		BeamShot, 
		shield, 
		horizontalBeam, 
		Clone, 
		helix, 
		wifi
	}

	public powerup CurrentPowerup;			    // The above enum values.
	public Text PowerupTimeText;

	[Header ("Powerup Prefabs")]
	public GameObject RegularShot; 				// The bullet the player shoots when there is no powerup.
	public GameObject RegularShotNoCost; 		// The bullet the player shoots when there is a powerup that doesnt change the regular shot.
	public GameObject MutedRegularShot; 		// Smae as above but has no volume.
	public GameObject DoubleShot; 				// Allows the player to shoot TWO bullets at a time without costing points.
	public GameObject TriShot; 					// Allows the player to shoot THREE bullets at a time without costing points.
	public GameObject BeamShot; 				// A solid beam of energy wiping out everything in its path vertically.
	public GameObject Shield; 					// An unbreakable shield whic wipes out everything in its path spherically.
	public GameObject HorizontalBeam; 			// An unbreakable laser which wipes out everything in its path horizontally.
	public GameObject ClonedPlayer; 			// The extra players which help the main player.
	public GameObject HelixObject;				// Helix powerup.
	public GameObject WifiShot;					// Wifi shot.

	public Canvas MainCanvas;					// Where most of the UI is (in camera space).
	public bool isClone; 						// Is this script attached to this gameObject a clone?

	// A GameObject when spawned will destroy itself immediately (To keep the console quiet of exceptions).
	public GameObject EmptyInstantdestroy; 

	[Header ("Misc powerup attributes")]
	// The powerup durations.
	public float powerupDurationA = 10.0f;
	public float powerupDurationB = 20.0f;
	public float powerupDurationC = 12.0f;
	public float powerupDurationD = 20.0f;
	public float powerupDurationE = 12.0f;
	public float powerupDurationF = 30.0f;

	public AudioSource powerupTimeRunningOut; 		// The audio source to play as there is a few seconds left of the powerup.
	public AudioSource powerupDeactivateAudio;	    // The audio source to play as the powerup time runs out.
	public ParticleSystem ActivePowerupParticles;   // Plays particle system if powerup is active.
	public ParticleSystem TimeRunningOutParticles;  // Plays particle system if powerup is running out.
	//public Image PowerupMeter; 						// The UI for the powerup bar.

	[Header ("Combos")]
	public float ComboTime;
	public int ComboN;
	public Text ComboText;
	public Animator ComboAnimation;
	public Image ComboImage;

	[Header ("Health")]
	public float Health; 			   	   			// Current health
	public float startingHealth = 100; 	   			// The initial starting health so the UI health bar appears filled 100%.
	public float minHealth = 0; 	   	   			// The health amount in which the player is defeated if this or below.

	public Image Health25;
	public Image Health50;
	public Image Health75;
	public Image Health100;
	public Image Health125;
	public Image Health150;
	public Image Health175;

	public float vibrationAmount = 1;  	   			// The vibration amount as the player loses some health.
	public float vibrationDuration = 0.4f; 			// The vibration duration as the player loses some health.
	public float vibrationTime; 		   			// The actual vibration time left.
	public float collisionCooldown;					// How long until the collider is active again?

	public Material HealthFull;
	public Material HealthThreeQuarters;
	public Material HealthHalf;
	public Material HealthQuarter;
	public Material YellowMaterial;

	public Animator Overlay;
	public ScreenOverlay ScreenOverlayScript;
	public float OverlayIntensity = 0.2f;
	public float OverlayTime;

	[Header ("Game Over")]
	public bool initialPart; 						// Is the GameOver state in its initial sequence.
	public float initialTimeScale = 0.1f; 			// The Time.timeScale in the GameOver initial sequence.
	public float slowTimeDuration = 3.0f; 			// How long does the initial sequence go for?
	public float slowTimeRemaining = 0.0f; 			// The actual time left of the initial GameOver sequence.
	public float TimeSlowingSpeed = 0.1f; 			// The rate which the Time.timeScale decreases.
	public float minTimeScale = 0.0f; 				// The minimum timescale which Time.timeScale should be greater.
	public AudioSource BGMMusic; 					// The main game music.
	public GameObject PressToContinue; 				// The initial UI "Press A to view stats".
	public GameObject DeactivatePlayerElements; 	// The GameObjects to deactivate once the player is defeated.
	//public GameObject GameOverUI; 					// The Game Over UI after the initial sequence.
	public AudioSource GameOverSound; 				// The GameOver Sound effect.
	public AudioSource GameOverLoop; 				// The defeated Game Over music loop.
	public GameObject gameOverExplosion; 			// The Awesome particle system for the Game Over.

	[Header ("Audio")]
	public AudioLowPassFilter BgmLowFilter;			// Low pass filter for the main music.
	public AudioHighPassFilter BgmHighFilter;		// High pass filter for the main music.

	[Header ("Main Camera attributes")]
	public GameObject MainCam;
	public float normalBloomAmount = 0.1f;
	public float powerupBloomAmount = 0.3f;
	private bool playedGameOverSound; 						 	// Has the game over sound been played?
	public float shakeTime = 0.5f;							 	// Time to shake the camera.
	public float shakeAmount = 1.0f; 						  	// How hard the shake is on the camera.
	public LayerMask layermask;
	public LayerMask allLayers;
	public GameObject Shafts;

	[HideInInspector]
	public ColorCorrectionCurves ColorCorrectionCurvesScript;	// Color Corrections image effect.
	private CameraShake camShakeScrpt; 						 	// Camera shake attached to the main camera.
	private Bloom bloomScript;
	public Lens LensScript; 	  								// The Lens script that is attached to the main camera.

	// MISC PRIVATES AND SCRIPTS //
	private GameController gameControllerScript; 			  	// GameController component.
	private TimescaleController timeScaleControllerScript;    	// TimeScale Controller component.
	private MeshCollider PlayerCollider; 					  	// Collider for the player.
	private MeshRenderer PlayerMesh; 						 	// MeshRenderer for the player.

	void Start () 
	{
		Camera.main.orthographicSize = 0.1f;
		ScreenOverlayScript = Camera.main.GetComponent<ScreenOverlay> ();
		OverlayTime = 0;
		OverlayIntensity = -0.15f;
		ComboTime = 0;
		collisionCooldown = 0;

		if (isClone == false) 
		{
			MainCam.transform.rotation = Quaternion.identity;
		}
			
		//bloomScript = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Bloom>();
		//bloomScript.bloomIntensity = normalBloomAmount;

		BgmHighFilter.enabled = false;
		BgmLowFilter.enabled = false;

		/*
		// Turns off powerup icons.
		DoubleShotIcon.SetActive (false);
		BeamShotIcon.SetActive (false);
		TriShotIcon.SetActive (false);
		ShieldIcon.SetActive (false);
		HorizontalBeamIcon.SetActive (false);
		CloneIcon.SetActive (false);
		HelixIcon.SetActive (false);
		WifiIcon.SetActive (false);
		ThreeDIcon.SetActive (false);
		*/
		MainCanvas.worldCamera = Camera.main;

		// Finds the rigidbody this script is attached to.
		rb = GetComponent<Rigidbody> ();

		// Finds Game Controller script.
		gameControllerScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();

		// Finds Player Mesh Collider (note collider and mesh comonents are two different GameObjects).
		PlayerCollider = GameObject.Find ("Collider").GetComponent<MeshCollider>();

		// Finds Player Mesh Renderer
		if (isClone == false) 
		{
			PlayerMesh = GameObject.FindGameObjectWithTag ("PlayerMesh").GetComponent<MeshRenderer> ();
		}

		// Gives health starting health amount.
		Health = startingHealth;
		Health25.enabled = true;
		Health50.enabled = true;
		Health75.enabled = true;
		Health100.enabled = true;
		Health125.enabled = false;
		Health150.enabled = false;
		Health175.enabled = false;

		// Timescale controller script.
		timeScaleControllerScript = GameObject.FindGameObjectWithTag ("TimeScaleController").GetComponent<TimescaleController> ();

		// Finds color correction curves script.
		ColorCorrectionCurvesScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ColorCorrectionCurves>();

		// Finds Camera Shake script.
		if (GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ().enabled == true && isClone == false) 
		{
			camShakeScrpt = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraShake> ();
			LensScript = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Lens> ();
		}

		// Start powerup conditions.
		CurrentPowerup = powerup.RegularShot;
		BeamShot.SetActive (false);
		Shield.SetActive (false);
		HorizontalBeam.SetActive (false);
		HelixObject.SetActive (false);

		// Start GameOver conditions.
		initialPart = false;
		PressToContinue.SetActive (false);
		slowTimeRemaining = slowTimeDuration;

		AltFire.SetActive (false);
		AltFireMode = altmode.no;
		AltFireImage.fillAmount = 1;

		Shafts = GameObject.FindGameObjectWithTag ("Shafts");
	}

	void Update () 
	{
		UpdateCamera();
		//UpdateHealth ();
		if (!gameControllerScript.isPaused)
		{
			/// Movement ///
			if (Time.timeScale > 0 && Health >= 25) 
			{
				if (PlayerNumber == playerNumber.PlayerOne && useKeyboardControls == true && Health >= 25) {
					float moveHorizontal;
					float moveVertical;

					// Keyboard input
					if (Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0) {
						moveHorizontal = Input.GetAxis ("Horizontal");
						moveVertical = Input.GetAxis ("Vertical");	
					} else { // Mouse input
						moveHorizontal = Input.GetAxis ("Mouse X");
						moveVertical = Input.GetAxis ("Mouse Y");					
					}

					Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0.0f);
					rb.velocity = movement * speed;
				}
			}

			if (Input.GetKey (KeyCode.LeftAlt) == true && AltFireImage.fillAmount >= 1) 
			{
				// When the player presses altfire while bar is greater than 0
				if (AltFireImage.fillAmount >= 1) 
				{
					AltFire.GetComponent<Animator> ().Play ("Wifi Enlarge");
					AltFire.GetComponent<AudioSource> ().Play ();
					AltFireMode = altmode.yes;
				}
			}

			if (AltFireImage.fillAmount <= 0) 
			{
				AltFireMode = altmode.no;
			}

			if (AltFireImage.fillAmount <= 0) 
			{
				AltFireImage.fillAmount += 0.1f * Time.deltaTime;
			}

			if (PlayerNumber == playerNumber.PlayerOne && useKeyboardControls == false && Health >= 25) 
			{
				float moveHorizontalA = Input.GetAxis ("Horizontal P1");
				float moveVerticalA = Input.GetAxis ("Vertical P1");
				Vector3 movementA = new Vector3 (moveHorizontalA, moveVerticalA, 0.0f);
				rb.velocity = movementA * speed;
			}

			if (PlayerNumber == playerNumber.PlayerTwo) 
			{
				float moveHorizontalB = Input.GetAxis ("Horizontal P2");
				float moveVerticalB = Input.GetAxis ("Vertical P2");
				Vector3 movementB = new Vector3 (moveHorizontalB, moveVerticalB, 0.0f);
				rb.velocity = movementB * speed;
			}

			rb.position = new Vector3 (rb.position.x, rb.position.y, 0);

			// Player boundaries
			GetComponent<Rigidbody> ().position = new Vector3 (
				Mathf.Clamp (rb.position.x, xBoundLower, xBoundUpper),
				Mathf.Clamp (rb.position.y, yBoundLower, yBoundUpper),
				zBound		
			);

			/// Shooting functionality ///

			// PC Controller Input.
			if ((Input.GetKey ("space") && Time.unscaledTime > nextFire && gameControllerScript.CurrentScore > -1 && Health > minHealth) ||
				(Input.GetKey (KeyCode.LeftControl) && Time.unscaledTime > nextFire && gameControllerScript.CurrentScore > -1 && Health > minHealth))
			{
				nextFire = Time.unscaledTime + fireRate * (1/Time.timeScale);
				Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			}

			if ((Input.GetKeyUp ("space") && gameControllerScript.CurrentScore > -1 && Health > minHealth) ||
				(Input.GetKeyUp (KeyCode.LeftControl) && gameControllerScript.CurrentScore > -1 && Health > minHealth))
			{
			}

			// Controller Input.
			if (PlayerNumber == playerNumber.PlayerOne && Health >= 25) 
			{
				if (Time.timeScale > 0) {
					if (((Input.GetAxisRaw ("Fire P1") > 0.1f || Input.GetMouseButton (0)) && Time.unscaledTime > nextFire && gameControllerScript.CurrentScore > -1 && Health > minHealth)) {
						nextFire = Time.unscaledTime + fireRate * (1 / Time.timeScale);
						Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
					}

					if (((Input.GetAxisRaw ("Alt Fire P1") > 0.3f || Input.GetMouseButton (1)) && gameControllerScript.CurrentScore > -1 && Health > minHealth && isClone == false) ||
					   (Input.GetKeyDown ("joystick 1 button 2") && gameControllerScript.CurrentScore > 0 && Health > minHealth && isClone == false)) {
						// When the player presses altfire while bar is greater than 0
						if (AltFireImage.fillAmount >= 1) {
							AltFire.GetComponent<Animator> ().Play ("Wifi Enlarge");
							AltFire.GetComponent<AudioSource> ().Play ();
							AltFireMode = altmode.yes;
						}
					}

					if (Input.GetAxisRaw ("Alt Fire P1") <= 0.3f) {
					}

					if (((Input.GetAxisRaw ("Alt Fire P1") <= 0 || Input.GetMouseButtonUp (1)) && gameControllerScript.CurrentScore > 0 && Health > minHealth && isClone == false) ||
					   (Input.GetKeyDown ("joystick 1 button 2") && gameControllerScript.CurrentScore > 0 && Health > minHealth && isClone == false)) {
					}
				}
			}

			if (powerupTime > 0) 
			{
				PowerupTimeText.text = "" + Mathf.RoundToInt(powerupTime) + "";
			}

			if (powerupTime <= 0) 
			{
				PowerupTimeText.text = " ";
			}

			if (OverlayTime <= 0.1f) 
			{
				OverlayIntensity = 0.1f;
			}

			if (OverlayTime > 0.1f) 
			{
				OverlayTime -= 2f * Time.unscaledDeltaTime;
				OverlayIntensity = -Mathf.Clamp(OverlayTime, 0, 1) + 0.15f;
				ScreenOverlayScript.intensity = OverlayIntensity;
			}

			if (AltFireMode == altmode.no) 
			{
				AltFire.SetActive (false);
				AltFireImage.fillAmount += 0.1f * Time.unscaledDeltaTime;
			}

			if (AltFireMode == altmode.yes) 
			{
				AltFire.SetActive (true);
				AltFireImage.fillAmount -= 0.2f * Time.unscaledDeltaTime;
			}

			if (AltFireMode == altmode.yes && AltFireImage.fillAmount <= 0) 
			{
				AltFireMode = altmode.no;
			}

			ComboN = Mathf.RoundToInt (ComboTime);
			ComboImage.fillAmount = (ComboTime / 10) - 0.1f;
			//ComboImage.color = new Color (0, 1, (ComboTime-1)/10, 1);

			if (ComboTime > 11) 
			{
				ComboTime = 11;
			}

			if (ComboTime < 0) 
			{
				ComboTime = 0;
			}
				
			if (ComboN > 10) 
			{
				ComboN = 10;
			}

			if (collisionCooldown > 0) 
			{
				collisionCooldown -= Time.unscaledDeltaTime;
				PlayerCollider.enabled = false;
			}

			if (collisionCooldown <= 0) 
			{
				collisionCooldown = 0;
				PlayerCollider.enabled = true;
			}

			if (isClone == false && Health >= 25) 
			{
			}

			if (Health < 25)
			{
				//MainCam.transform.rotation = Quaternion.Euler (0, 0, 0);
				Camera.main.transform.position = new Vector3 (PlayerMesh.transform.position.x, PlayerMesh.transform.position.y, -100);
			}

			if (isClone) 
			{
				shot = MutedRegularShot;
			}

			// If the Game Object that is attached to this script is not a clone.
			if (!isClone) 
			{
				// Decreases vibration time linearly.
				vibrationTime -= Time.unscaledDeltaTime;

				if (vibrationTime > 0) 
				{
					GamePad.SetVibration (PlayerIndex.One, vibrationAmount, vibrationAmount);
				}

				if (vibrationTime <= 0) 
				{
					vibrationTime = 0;
					GamePad.SetVibration (PlayerIndex.One, 0, 0);
				}

				/// POWERUPS ///
				UpdatePowerUps();
				
				/// Health ///
				UpdateHealth();
			}
		}
	}

	private void UpdateCamera()
	{
		if (Shafts.GetComponent<SmoothFollowOrig> ().target == null)
		{
			Shafts.GetComponent<SmoothFollowOrig> ().target = gameObject.transform;
		}

		if (Camera.main.orthographicSize < 30 && Health >= 25) 
		{
			//Camera.main.GetComponent<SmoothFollowOrig> ().enabled = false;
			Camera.main.orthographicSize = Mathf.Lerp (Camera.main.orthographicSize, 30, Time.deltaTime);
		}

		if (Camera.main.orthographicSize < 30 && Health < 25) 
		{
			//Camera.main.GetComponent<SmoothFollowOrig> ().enabled = true;
			//Camera.main.GetComponent<SmoothFollowOrig> ().target = GameObject.Find ("GameOverExplosion(Clone)").transform;
			Camera.main.orthographicSize = Mathf.Lerp (Camera.main.orthographicSize, 0, 0.25f * Time.deltaTime);
		}
	}

	public void UpdateHealth()
	{
		// UI health fill amounts and colors.

		if (Health == 175) 
		{
			Health25.enabled = true;
			Health50.enabled = true;
			Health75.enabled = true;
			Health100.enabled = true;
			Health125.enabled = true;
			Health150.enabled = true;
			Health175.enabled = true;

			Health25.material = HealthFull;
			Health50.material = HealthFull;
			Health75.material = HealthFull;
			Health100.material = HealthFull;
			Health125.material = HealthThreeQuarters;
			Health150.material = HealthThreeQuarters;
			Health175.material = HealthThreeQuarters;
		}

		if (Health == 150) 
		{
			Health25.enabled = true;
			Health50.enabled = true;
			Health75.enabled = true;
			Health100.enabled = true;
			Health125.enabled = true;
			Health150.enabled = true;
			Health175.enabled = false;

			Health25.material = HealthFull;
			Health50.material = HealthFull;
			Health75.material = HealthFull;
			Health100.material = HealthFull;
			Health125.material = HealthThreeQuarters;
			Health150.material = HealthThreeQuarters;
		}

		if (Health == 125) 
		{
			Health25.enabled = true;
			Health50.enabled = true;
			Health75.enabled = true;
			Health100.enabled = true;
			Health125.enabled = true;
			Health150.enabled = false;
			Health175.enabled = false;

			Health25.material = HealthFull;
			Health50.material = HealthFull;
			Health75.material = HealthFull;
			Health100.material = HealthFull;
			Health125.material = HealthThreeQuarters;
		}


		if (Health == 100) 
		{
			Health25.enabled = true;
			Health50.enabled = true;
			Health75.enabled = true;
			Health100.enabled = true;
			Health125.enabled = false;
			Health150.enabled = false;
			Health175.enabled = false;

			Health25.material = HealthFull;
			Health50.material = HealthFull;
			Health75.material = HealthFull;
			Health100.material = HealthFull;

		}

		if (Health == 75) 
		{
			Health25.enabled = true;
			Health50.enabled = true;
			Health75.enabled = true;
			Health100.enabled = false;
			Health125.enabled = false;
			Health150.enabled = false;
			Health175.enabled = false;

			Health25.material = HealthThreeQuarters;
			Health50.material = HealthThreeQuarters;
			Health75.material = HealthThreeQuarters;
		}

		if (Health == 50) 
		{
			Health25.enabled = true;
			Health50.enabled = true;
			Health75.enabled = false;
			Health100.enabled = false;
			Health125.enabled = false;
			Health150.enabled = false;
			Health175.enabled = false;

			Health25.material = HealthHalf;
			Health50.material = HealthHalf;
		}

		if (Health == 25) 
		{
			Health25.enabled = true;
			Health50.enabled = false;
			Health75.enabled = false;
			Health100.enabled = false;
			Health125.enabled = false;
			Health150.enabled = false;
			Health175.enabled = false;

			Health25.material = HealthQuarter;
		}

		if (Health > 175) 
		{
			Health = 175;
		}

		// Health at 0.
		if (Health <= 0) 
		{
			Health25.enabled = false;
			Health50.enabled = false;
			Health75.enabled = false;
			Health100.enabled = false;
			Health125.enabled = false;
			Health150.enabled = false;
			Health175.enabled = false;

			PlayerMesh.enabled = false; 				// Turns off player mesh renderer.
			PlayerCollider.enabled = false; 			// Turns off player collider.
			DeactivatePlayerElements.SetActive (false); // Turns off Player Game Objects.
			gameControllerScript.StopAllCoroutines ();  // Stops spawning objects and powerups.
			GameOver (); 								// Triggers game Over method.
			timeScaleControllerScript.enabled = false;  // Turns off time scale controller script.
		}
	}

	private void UpdatePowerUps()
	{
		// No powerup.
		if (CurrentPowerup == powerup.RegularShot) 
		{
			if (LensScript.radius >= 0 && gameControllerScript.isPaused == false) 
			{
				LensScript.radius -= 0.5f * Time.unscaledDeltaTime; 
			}

			if (LensScript.radius < 0 && gameControllerScript.isPaused == false) 
			{
				LensScript.radius = 0;
			}

			fireRate = 0.25f;
			//bloomScript.bloomIntensity = normalBloomAmount;
			GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>().enabled = true;
			MainCanvas.worldCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
			shot = RegularShot; 							// Assigns shot which costs points.
			if (GameObject.Find ("Clone(Clone)") != null) 
			{
				Destroy (GameObject.Find ("Clone(Clone)"));
			}
			BeamShot.SetActive (false); 					// Turns off the vertical beam.
			Shield.SetActive (false);					    // Turns off the shield.
			HorizontalBeam.SetActive (false); 				// Turns off the horizontal beam.
			HelixObject.SetActive (false);
			/*
			WifiIcon.SetActive (false);
			ThreeDIcon.SetActive (false);

			// Turns off all powerup icons.
			DoubleShotIcon.SetActive (false);
			BeamShotIcon.SetActive (false);
			TriShotIcon.SetActive (false);
			ShieldIcon.SetActive (false);
			HorizontalBeamIcon.SetActive (false);
			CloneIcon.SetActive (false);
			HelixIcon.SetActive (false);
			*/
			LensScript.enabled = true;
			gameControllerScript.PowerupText.text = "" + ""; // Shows how much each bullet costs as the powerup text.
			BeamShot.SetActive (false);						 // Turns off the beam shot.
			HorizontalBeam.SetActive (false); 				 // Turns off the horizontal beam shot.

			// if the lens script radius is greater than 0.
			if (LensScript.radius > 0 && Health > 25) 
			{
				LensScript.radius -= Time.unscaledDeltaTime;
			}

			// if the lens script radius is less than 0.
			if (LensScript.radius < 0) 
			{
				LensScript.radius = 0; 						// Make it equal to exactly 0.
			}
		}

		// Double shot.
		if (CurrentPowerup == powerup.DoubleShot) 
		{
			//bloomScript.bloomIntensity = powerupBloomAmount;
			shot = DoubleShot;								 // Assigns free double shot powerup.
			if (Time.timeScale > 0)
			{
				powerupTime -= Time.unscaledDeltaTime; 			 // Decreases powerup time linearly.
			}
			gameControllerScript.PowerupText.text = "DOUBLE SHOT!"; // UI displays double shot text.
			//DoubleShotIcon.SetActive (true); 				 // Turns on the double shot icon.
		}

		// WIFI shot.
		if (CurrentPowerup == powerup.wifi) 
		{
			fireRate = 0.5f;
			//bloomScript.bloomIntensity = powerupBloomAmount;
			shot = WifiShot; 								// Assigns free wifi shot powerup.
			if (Time.timeScale > 0)
			{
				powerupTime -= Time.unscaledDeltaTime; 			 // Decreases powerup time linearly.
			}
			gameControllerScript.PowerupText.text = "RIPPLE!"; // UI displays double shot text.
			//WifiIcon.SetActive (true); 						// Turns on the wifi shot icon.
		}

		// Tri shot.
		if (CurrentPowerup == powerup.TriShot) 
		{
			//bloomScript.bloomIntensity = powerupBloomAmount;
			shot = TriShot; 								// Assigns free triple shot powerup.
			if (Time.timeScale > 0)
			{
				powerupTime -= Time.unscaledDeltaTime; 			 // Decreases powerup time linearly.
			}
			gameControllerScript.PowerupText.text = "TRIPLE SHOT"; // UI displays triple shot text.
			//TriShotIcon.SetActive (true); 					// Turns on double shot icon.
		}

		// Beam shot.
		if (CurrentPowerup == powerup.BeamShot) 
		{
			//bloomScript.bloomIntensity = powerupBloomAmount;
			BeamShot.SetActive (true); 						// Turns on vertical beam.
			if (Time.timeScale > 0)
			{
				powerupTime -= Time.unscaledDeltaTime; 			 // Decreases powerup time linearly.
			}
			gameControllerScript.PowerupText.text = "ULTRA BEAM!"; // UI displays vertical beam text.
			//BeamShotIcon.SetActive (true); 					// Turns on UI icon for the vertical beam.

			// If shot is the regular shot.
			if (shot == RegularShot) 
			{
				shot = RegularShotNoCost; 					// Make it the free version.
			}
		}

		// Shield.
		if (CurrentPowerup == powerup.shield) 
		{
			//bloomScript.bloomIntensity = powerupBloomAmount;
			Shield.SetActive (true); 						// Turns on the shield.
			if (Time.timeScale > 0)
			{
				powerupTime -= Time.unscaledDeltaTime; 			 // Decreases powerup time linearly.
			}
			gameControllerScript.PowerupText.text = "GIGA SHIELD!"; // UI text to display shield.
			//ShieldIcon.SetActive (true); 					// Turns on UI icon for the shield.
			collisionCooldown = 3;
			// If lens script radius is less than or equal to 0.5 but also greater than 0.
			if (LensScript.radius <= 0.5f && LensScript.radius >= 0 && gameControllerScript.isPaused == false) 
			{
				LensScript.radius += 0.1f * Time.unscaledDeltaTime; // Increases lens script radius linearly by factor of 0.1.  
			}

			// If shot is the regular shot.
			if (shot == RegularShot) 
			{
				shot = RegularShotNoCost; // Make it the free version.
			}
		}

		// Horizontal beam.
		if (CurrentPowerup == powerup.horizontalBeam) 
		{	
			//bloomScript.bloomIntensity = powerupBloomAmount;	
			//HorizontalBeamIcon.SetActive (true);
			HorizontalBeam.SetActive (true); // Turns on the horizontal beam.
			if (Time.timeScale > 0)
			{
				powerupTime -= Time.unscaledDeltaTime; 			 // Decreases powerup time linearly.
			}
			gameControllerScript.PowerupText.text = "TERROR BEAM!"; // UI display for powerup text.
			// If shot is the regular shot.
			if (shot == RegularShot) 
			{
				shot = RegularShotNoCost; // Make it the free version.
			}
		}

		// Clone player.
		if (CurrentPowerup == powerup.Clone) 
		{
			//bloomScript.bloomIntensity = powerupBloomAmount;
			//ClonedPlayer.SetActive (true); // Turns on the clones!

			if (Time.timeScale > 0)
			{
				powerupTime -= Time.unscaledDeltaTime; 			 // Decreases powerup time linearly.
			}
			gameControllerScript.PowerupText.text = "CLONE!"; // UI display clones.
			//CloneIcon.SetActive (true); // Turns on clone icon.
			//BgmHighFilter.enabled = true;

			// If shot is the regular shot.
			if (shot == RegularShot) 
			{
				shot = RegularShotNoCost; // Make it the free version.
			}
		}

		// Helix bullets.
		if (CurrentPowerup == powerup.helix) 
		{
			//bloomScript.bloomIntensity = powerupBloomAmount;
			HelixObject.SetActive (true);

			if (Time.timeScale > 0)
			{
				powerupTime -= Time.unscaledDeltaTime; 			 // Decreases powerup time linearly.
			}
			gameControllerScript.PowerupText.text = "MEGA HELIX!"; // UI display clones.
			//HelixIcon.SetActive (true); // Turns on clone icon.
			if (shot == RegularShot) 
			{
				shot = RegularShotNoCost; // Make it the free version.
			}
		}

		// Warning powerup time.
		if (powerupTime < 3.0f && powerupTime > 2.8f) 
		{
			ActivePowerupParticles.Stop (); // Turns off main powerup particles.
			powerupTimeRunningOut.Play (); // Turns on running out particles.
			TimeRunningOutParticles.Play (); // Plays running out sound.
		}

		// When powerup runs out.
		if (powerupTime < 0) 
		{
			powerupTime = 0; // Reset powerup time.
			CurrentPowerup = powerup.RegularShot; // Powerup type is now regular.
			BeamShot.SetActive (false); // Turns off the beam.
			Shield.SetActive (false); // Turns off the shield.

			if (GameObject.Find ("Clone(Clone)") != null) 
			{
				Destroy (GameObject.Find ("Clone(Clone)"));
			}

			HorizontalBeam.SetActive (false); // Turns off the horizontal beam.
			shot = RegularShot; // Assigns shot to cost points.
		}

		// Powerup ran out.
		if (powerupTime > 0 && powerupTime < 0.05f && !powerupDeactivateAudio.GetComponent<AudioSource> ().isPlaying) 
		{
			powerupDeactivateAudio.Play (); // Plays powerup ran out sound.
		}

		if (powerupTime > 40) 
		{
			powerupTime = 40;
		}
	}

	void FixedUpdate ()
	{
		ComboText.text = "x" + ComboN;

		if (ComboTime >= 0) 
		{
			if (gameControllerScript.isPaused != true) 
			{
				ComboTime -= 1.5f * Time.deltaTime;
			}

			if (gameControllerScript.isPaused == true) 
			{
				return;
			}
		}

		if (ComboTime < 1f) 
		{
			ComboTime = 1;
		}

		if (ComboN < 1) 
		{
			ComboN = 1;
		}

		if (ComboN > 10) 
		{
			ComboN = 10;
		}

		if (AltFireImage.fillAmount < 1) 
		{
			AltFireIndicator.GetComponent<Image> ().color = new Color (0, 0, 0, 0.4f);
		}
			
		if (AltFireImage.fillAmount >= 1) 
		{
			AltFireImage.fillAmount = 1;
			AltFireIndicator.GetComponent<Image> ().color = new Color (0, 1, 0, 0.4f);
		}

		if (AltFireImage.fillAmount <= 0) 
		{
			AltFireImage.fillAmount += 0.1f * Time.unscaledDeltaTime;
		}

		// Alt fire image bar colours.
		if (AltFireImage.fillAmount >= 1 && AltFireImage.fillAmount > 0.75f) 
		{
			AltFireImage.material = HealthThreeQuarters;
		}
			
		if (AltFireImage.fillAmount < 1 && AltFireImage.fillAmount > 0.75f) 
		{
			AltFireImage.material = YellowMaterial;
		}

		if (AltFireImage.fillAmount > 0.5f && AltFireImage.fillAmount <= 0.75f) 
		{
			AltFireImage.material = HealthHalf;
		}

		if (AltFireImage.fillAmount > 0.25f && AltFireImage.fillAmount <= 0.5f) 
		{
			AltFireImage.material = HealthHalf;
		}

		if (AltFireImage.fillAmount > 0 && AltFireImage.fillAmount <= 0.25f) 
		{
			AltFireImage.material = HealthQuarter;
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if ((other.tag == "BossPart" || other.tag == "Cube" || other.tag == "Bomb") && (Shield.activeInHierarchy == false || BeamShot.activeInHierarchy == false || HorizontalBeam.activeInHierarchy == false))
		{
			collisionCooldown = 3;
			vibrationTime = vibrationDuration; // Sets vibration time to set duration.
			camShakeScrpt.shakeAmount = shakeAmount; // Sets cam shake to shake amount.
			camShakeScrpt.shakeDuration = shakeTime; // Sets shake duration to shake time amount.
			timeScaleControllerScript.CalculationMode = TimescaleController.calcMode.none;
			Time.timeScale = 0.2f;		
		}

		if (other.tag == "Barrier") 
		{
			vibrationTime = vibrationDuration; // Sets vibration time to set duration.
			camShakeScrpt.shakeAmount = shakeAmount; // Sets cam shake to shake amount.
			camShakeScrpt.shakeDuration = shakeTime; // Sets shake duration to shake time amount.
		}
	}

	public void GameOver ()
	{
		BeamShot.SetActive (false);
		HorizontalBeam.SetActive (false);
		Shield.SetActive (false);
		HelixObject.SetActive (false);
		slowTimeRemaining -= Time.unscaledDeltaTime; // decrements slow time remaining.
		BGMMusic.Stop (); // Stops main music.

		if (slowTimeRemaining > 2.0f) 
		{
			LensScript.radius += 6 * Time.deltaTime;
			Camera.main.cullingMask = layermask;
			Time.timeScale = 0.05f;
		}
			
		if (slowTimeRemaining < 2f && slowTimeRemaining > 0 && Time.timeScale < 1) 
		{
			Time.timeScale += 3f * Time.unscaledDeltaTime;
			LensScript.radius += 3 * Time.deltaTime;
			//GameOverCam.SetActive(true);
		}
			
		if (slowTimeRemaining <= 0) 
		{
			if (Time.timeScale >= 0.01f)
			{
				if (LensScript.radius > 0) 
				{
					LensScript.radius -= 3f * Time.unscaledDeltaTime;
				}

				if (LensScript.radius < 0) 
				{
					LensScript.radius = 0;
				}
				Camera.main.cullingMask = allLayers;
				slowTimeRemaining = 0;
				Time.timeScale -= 0.25f * Time.unscaledDeltaTime;

				timeScaleControllerScript.enabled = false; // Turns off timescale controller.
				PressToContinue.SetActive (true); // Activates "Press A to continue" text.

				// Plays game over loop.
				if (!GameOverLoop.isPlaying)
				{
					GameOverLoop.PlayDelayed (4.0f); // Delays.
				}
			}
		}

		if (Input.GetKeyDown ("joystick button 0") || Input.GetKeyDown(KeyCode.Space)) 
		{
			//GameOverUI.SetActive (true);
		}

		// if health is below 0 and the Game Over UI is active.
		if (Health <= 0) 
		{
			PressToContinue.SetActive (true); // Turns off press to continue UI message.

			// When timescale is above an amount, to decrement time scale.
			if (Time.timeScale > 0.0166f) 
			{
				Time.timeScale -= Time.unscaledDeltaTime * TimeSlowingSpeed; // Slows time down slowly.
				PressToContinue.SetActive (false); // Turns off press to continue UI.
			}

			// When timescale is low enough.
			if (Time.timeScale <= 0.0165f)
			{
				Cursor.lockState = CursorLockMode.None;
				//Cursor.visible = true;
				Time.timeScale = 0.001f;
				PressToContinue.SetActive (true); // Turns off press to continue UI.
			}
		}
	}
}
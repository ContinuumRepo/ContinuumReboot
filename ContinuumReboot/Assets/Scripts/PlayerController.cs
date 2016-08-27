﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using XInputDotNetPure;

public class PlayerController : MonoBehaviour 
{
	private GameController gameControllerScript; // GameController component.
	private TimescaleController timeScaleControllerScript; // TimeScale Controller component.
	private MeshCollider PlayerCollider; // Collider for the player.
	private MeshRenderer PlayerMesh; // MeshRenderer for the player
	private AudioSourcePitchByTimescale BGMPitchScript; // Pitch Script for the main music.
	public ColorCorrectionCurves ColorCorrectionCurvesScript; // Color Corrections image effect.
	private bool playedGameOverSound; // Has the game over sound been played?
	private CameraShake camShakeScrpt; // Camera shake attached to the main camera.
	public float shakeTime = 0.5f; // Time to shake the camera.
	public float shakeAmount = 1.0f; // How hard the shake is on the camera.

	[Header ("Movement")]
	private Rigidbody rb; // The attached rigidbody component.
	public float speed = 10.0f; // The overall speed the player can move (sensitivity).
	public float tilt = 0.0f; // The amount the player can tilt (default = 0).

	[Header ("Bounds")]
	public float xBoundLower = -20.0f;
	public float xBoundUpper = 20.0f;
	public float yBoundLower = -20.0f;
	public float yBoundUpper = 20.0f;
	public float zBound = 0;

	[Header ("Shooting")]
	public GameObject shot; // The main shot GameObject.
	public Transform shotSpawn; // Where the shot will be placed when instantiated.
	public float fireRate; // Time between bullets before a new one is instantiated.
	private float nextFire; 
	public GameObject EmptyInstantdestroy; // A GameObject when spawned will destroy itself immediately (To keep the console quiet of exceptions).

	[Header ("Powerups")]
	public GameObject[] Powerups; // An array of powerup objects to spawn for the player.
	public enum powerup {RegularShot, DoubleShot, TriShot, BeamShot, shield, horizontalBeam, Clone} // The different types of powerups.
	public powerup CurrentPowerup; // The above enum values.
	public GameObject RegularShot; // The bullet the player shoots when there is no powerup.
	public GameObject RegularShotNoCost; // The bullet the player shoots when there is a powerup that doesnt change the regular shot.
	public GameObject MutedRegularShot; // Smae as above but has no volume.
	public GameObject DoubleShot; // Allows the player to shoot TWO bullets at a time without costing points.
	public GameObject TriShot; // Allows the player to shoot THREE bullets at a time without costing points.
	public GameObject BeamShot; // A solid beam of energy wiping out everything in its path vertically.
	public GameObject Shield; // An unbreakable shield whic wipes out everything in its path spherically.
	public GameObject HorizontalBeam; // An unbreakable laser which wipes out everything in its path horizontally.
	public GameObject ClonedPlayer; // The extra players which help the main player.
	public bool isClone; // Is this script attached to this gameObject a clone?
	public GameObject DoubleShotIcon; // The UI for the double shot powerup.
	public GameObject TriShotIcon; // The UI for the tri shot powerup.
	public GameObject BeamShotIcon; // The UI for the beam shot powerup.
	public GameObject ShieldIcon; // The UI for the shield powerup.
	public GameObject HorizontalBeamIcon; // The UI for the horizontal beam powerup.
	public GameObject CloneIcon; // The UI for the clone player powerup.

	public Lens LensScript; // The Lens script that is attached to the main camera.
	public float powerupTime = 0; // The current powerup time left.
	public float powerupDurationA = 10.0f; // The powerupDuration.
	public float powerupDurationB = 20.0f;
	public float powerupDurationC = 12.0f;
	public float powerupDurationD = 20.0f;
	public float powerupDurationE = 12.0f;
	public float powerupDurationF = 30.0f;

	public AudioSource powerupTimeRunningOut; // The audio source to play as there is a few seconds left of the powerup.
	public AudioSource powerupDeactivateAudio; // The audio source to play as the powerup time runs out.
	public ParticleSystem ActivePowerupParticles; // Plays particle system if powerup is active.
	public ParticleSystem TimeRunningOutParticles; // Plays particle system if powerup is running out.
	public Image PowerupMeter; // The UI for the powerup bar.

	[Header ("Health")]
	public float Health; // Current health
	public float startingHealth = 100; // The initial starting health so the UI health bar appears filled 100%.
	public float minHealth = 0; // The health amount in which the player is defeated if this or below.
	public Image HealthImage; // The health bar.
	public float vibrationAmount = 1; // The vibration amount as the player loses some health.
	public float vibrationDuration = 0.4f; // The vibration duration as the player loses some health.
	public float vibrationTime; // The actual vibration time left.
	public Text HealthText; // The health text value which will be in percentage.

	[Header ("Game Over")]
	public bool initialPart; // Is the GameOver state in its initial sequence.
	public float initialTimeScale = 0.1f; // The Time.timeScale in the GameOver initial sequence.
	public float slowTimeDuration = 3.0f; // How long does the initial sequence go for?
	public float slowTimeRemaining = 0.0f; // The actual time left of the initial GameOver sequence.
	public AudioSource BGMMusic; // The main game music.
	public GameObject PressToContinue; // The initial UI "Press A to view stats".
	public GameObject DeactivatePlayerElements; // The GameObjects to deactivate once the player is defeated.
	public float TimeSlowingSpeed = 0.1f; // The rate which the Time.timeScale decreases.
	public float minTimeScale = 0.0f; // The minimum timescale which Time.timeScale should be greater.
	public GameObject GameOverUI; // The Game Over UI after the initial sequence.
	public AudioSource GameOverSound; // The GameOver Sound effect.
	public AudioSource GameOverLoop; // The defeated Game Over music loop.
	public GameObject gameOverExplosion; // The Awesome particle system for the Game Over.

	void Start () 
	{
		// Turns off powerup icons.
		DoubleShotIcon.SetActive (false);
		BeamShotIcon.SetActive (false);
		TriShotIcon.SetActive (false);
		ShieldIcon.SetActive (false);
		HorizontalBeamIcon.SetActive (false);
		CloneIcon.SetActive (false);
	
		// Finds the rigidbody this script is attached to.
		rb = GetComponent<Rigidbody> ();

		// Finds Game Controller script.
		gameControllerScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();

		// Finds Player Mesh Collider (note collider and mesh comonents are two different GameObjects).
		PlayerCollider = GameObject.Find ("Collider").GetComponent<MeshCollider>();

		// Finds Player Mesh Renderer
		PlayerMesh = GameObject.Find ("PlayerMesh").GetComponent<MeshRenderer>();

		// Gives health starting health amount.
		Health = startingHealth;
		HealthImage.fillAmount = Health / 100;

		// Timescale controller script.
		timeScaleControllerScript = GameObject.FindGameObjectWithTag ("TimeScaleController").GetComponent<TimescaleController> ();

		// Background music pitch by timescale script.
		BGMPitchScript = GameObject.FindGameObjectWithTag ("BGM").GetComponent<AudioSourcePitchByTimescale>();

		// Finds color correction curves script.
		ColorCorrectionCurvesScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ColorCorrectionCurves>();

		// Finds Camera Shake script.
		camShakeScrpt = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraShake>();

		// Finds main camera's lens script.
		LensScript = Camera.main.GetComponent<Lens> ();

		// Start powerup conditions.
		CurrentPowerup = powerup.RegularShot;
		BeamShot.SetActive (false);
		Shield.SetActive (false);
		HorizontalBeam.SetActive (false);

		// Start GameOver conditions.
		GameOverUI.SetActive (false);
		initialPart = false;
		PressToContinue.SetActive (false);
		slowTimeRemaining = slowTimeDuration;
	}

	void Update () 
	{
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

			// UI health fill amounts and colors.
			HealthImage.fillAmount = Health / 100;
			HealthImage.color = new Color (25 / Health, Health / 100, 0, 0.9f);
			HealthText.text = string.Format ("{0:0}", Mathf.Round (Health)) + "%";

			/// POWERUPS ///
			PowerupMeter.fillAmount = powerupTime / powerupDurationA; // UI fill amount for powerup.
			PowerupMeter.color = new Color (1 / powerupTime, powerupTime / 10, powerupTime / 15, 1.0f);

			// No powerup
			if (CurrentPowerup == powerup.RegularShot) 
			{
				shot = RegularShot; // Assigns shot which costs points.
				ClonedPlayer.SetActive (false); // Turns off cloned players.
				BeamShot.SetActive (false); // Turns off the vertical beam.
				Shield.SetActive (false); // Turns off the shield.
				HorizontalBeam.SetActive (false); // Turns off the horizontal beam.

				// Turns off all powerup icons.
				DoubleShotIcon.SetActive (false);
				BeamShotIcon.SetActive (false);
				TriShotIcon.SetActive (false);
				ShieldIcon.SetActive (false);
				HorizontalBeamIcon.SetActive (false);
				CloneIcon.SetActive (false);

				gameControllerScript.PowerupText.text = "" + "- 2.5% x shot"; // Shows how much each bullet costs as the powerup text.
				BeamShot.SetActive (false); // Turns off the beam shot.
				HorizontalBeam.SetActive (false); // Turns off the horizontal beam shot.
				PlayerCollider.enabled = true; // Turns the player collider on.
			
				// if the lens script radius is greater than 0.
				if (LensScript.radius > 0) 
				{
					LensScript.radius -= Time.unscaledDeltaTime;
				}

				// if the lens script radius is less than 0.
				if (LensScript.radius < 0) 
				{
					LensScript.radius = 0; // Make it equal to exactly 0.
				}
			}

			// Double Shot
			if (CurrentPowerup == powerup.DoubleShot) 
			{
				shot = DoubleShot; // Assigns free double shot powerup.
				powerupTime -= Time.unscaledDeltaTime; // Decreases powerup time linearly.
				gameControllerScript.PowerupText.text = "Double Shot"; // UI displays double shot text.
				DoubleShotIcon.SetActive (true); // Turns on the double shot icon.
			}

			// Tri shot.
			if (CurrentPowerup == powerup.TriShot) 
			{
				shot = TriShot; // Assigns free triple shot powerup.
				powerupTime -= Time.unscaledDeltaTime; // Decreases powerup time linearly.
				gameControllerScript.PowerupText.text = "Triple Shot"; // UI displays triple shot text.
				TriShotIcon.SetActive (true); // Turns on double shot icon.
			}

			// Beam shot.
			if (CurrentPowerup == powerup.BeamShot) 
			{
				BeamShot.SetActive (true); // Turns on vertical beam.
				powerupTime -= Time.unscaledDeltaTime; // Decreases powerup time linearly.
				gameControllerScript.PowerupText.text = "Ultra Beam"; // UI displays vertical beam text.
				BeamShotIcon.SetActive (true); // Turns on UI icon for the vertical beam.

				// If shot is the regular shot.
				if (shot == RegularShot) 
				{
					shot = RegularShotNoCost; // Make it the free version.
				}
			}

			// Shield.
			if (CurrentPowerup == powerup.shield) 
			{
				Shield.SetActive (true); // Turns on the shield.
				powerupTime -= Time.unscaledDeltaTime; // Decreases powerup time linearly.
				gameControllerScript.PowerupText.text = "Shield"; // UI text to display shield.
				PlayerCollider.enabled = false; // Turns off the player collider.
				ShieldIcon.SetActive (true); // Turns on UI icon for the shield.

				// If lens script radius is less than or equal to 0.5 but also greater than 0.
				if (LensScript.radius <= 0.5f && LensScript.radius >= 0) 
				{
					LensScript.radius += 0.1f * Time.unscaledDeltaTime; // Increases lens script radius linearly by factor of 0.1.  
				}

				// If shot is the regular shot.
				if (shot == RegularShot) 
				{
					shot = RegularShotNoCost; // Make it the free version.
				}
			}

			// Horizontal Beam
			if (CurrentPowerup == powerup.horizontalBeam) 
			{		
				HorizontalBeam.SetActive (true); // Turns on the horizontal beam.
				powerupTime -= Time.unscaledDeltaTime; // Decreases powerup time linearly.
				gameControllerScript.PowerupText.text = "Horizontal Beam"; // UI display for powerup text.

				// If shot is the regular shot.
				if (shot == RegularShot) 
				{
					shot = RegularShotNoCost; // Make it the free version.
				}
			}

			// Clone Player
			if (CurrentPowerup == powerup.Clone) 
			{
				ClonedPlayer.SetActive (true); // Turns on the clones!
				powerupTime -= Time.unscaledDeltaTime; // Decreases powerup time linearly.
				gameControllerScript.PowerupText.text = "Cloned!"; // UI display clones.
				CloneIcon.SetActive (true); // Turns on clone icon.
				PlayerCollider.enabled = false; // Turns off player collider.

				// If shot is the regular shot.
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
				ClonedPlayer.SetActive (false); // Turns off the clones.
				HorizontalBeam.SetActive (false); // Turns off the horizontal beam.
				shot = RegularShot; // Assigns shot to cost points.
			}

			// Powerup ran out.
			if (powerupTime > 0 && powerupTime < 0.05f && !powerupDeactivateAudio.GetComponent<AudioSource> ().isPlaying) 
			{
				powerupDeactivateAudio.Play (); // Plays powerup ran out sound.
			}
			
			/// Health ///

			// Health at 0.
			if (Health <= 0) 
			{
				PlayerMesh.enabled = false; // Turns off player mesh renderer.
				PlayerCollider.enabled = false; // Turns off player collider.
				DeactivatePlayerElements.SetActive (false); // Turns off Player Game Objects.
				gameControllerScript.StopAllCoroutines (); // Stops spawning objects and powerups.
				GameOver (); // Triggers game Over method.
				timeScaleControllerScript.enabled = false; // Turns off time scale controller script.
				BGMPitchScript.addPitch = 0; // Resets the add pitch variable with the main music.
			}

			// if collor correction saturation is less than 1
			if (ColorCorrectionCurvesScript.saturation < 1) 
			{
				ColorCorrectionCurvesScript.saturation += 0.5f * Time.unscaledDeltaTime; // Increase saturation.
			}
		}
	}

	void FixedUpdate ()
	{
		/// Movement ///
	
		if (gameControllerScript.isPreGame == false) 
		{
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			// Assigns movement to a Vector 3, changes sensitivity according to timescale.
			Vector3 movement = new Vector3 (moveHorizontal * (1/Time.timeScale), moveVertical * (1/Time.timeScale), 0.0f);
			rb.velocity = movement * speed;
			rb.position = new Vector3 (rb.position.x, rb.position.y, 0);

			// Player boundaries
			GetComponent<Rigidbody> ().position = new Vector3 (
				Mathf.Clamp (rb.position.x, xBoundLower, xBoundUpper),
				Mathf.Clamp (rb.position.y, yBoundLower, yBoundUpper),
				zBound		
			);
		}

		/// Shooting functionality ///

		// PC Controller Input.
		if ((Input.GetKey ("space") && Time.unscaledTime > nextFire && gameControllerScript.CurrentScore > 0 && Health > minHealth) ||
			(Input.GetKey (KeyCode.LeftControl) && Time.time > nextFire && gameControllerScript.CurrentScore > 0 && Health > minHealth))
		{
			nextFire = Time.unscaledTime + fireRate * (1/Time.timeScale);
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}

		// XBox 360 controller input for Windows.
		if ((Input.GetAxisRaw ("Fire1") < 0 && Time.time > nextFire && gameControllerScript.CurrentScore > 0 && Health > minHealth) || // Using Trigger.
			(Input.GetKeyDown ("joystick button 0") && Time.time > nextFire && gameControllerScript.CurrentScore > 0 && Health > minHealth)) // Using A button.
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Brick" || other.tag == "Cube")
		{
			vibrationTime = vibrationDuration; // Sets vibration time to set duration.
			camShakeScrpt.shakeAmount = shakeAmount; // Sets cam shake to shake amount.
			camShakeScrpt.shakeDuration = shakeTime; // Sets shake duration to shake time amount.
		}
	}

	void GameOver ()
	{
		slowTimeRemaining -= Time.unscaledDeltaTime; // decrements slow time remaining.
		BGMMusic.Stop (); // Stops main music.

		// if slow motion time is more than 0.5 seconds.
		if (slowTimeRemaining > 0.5f) 
		{
			Time.timeScale = 0.01f; // Make time scale 1%.
		}

		// if slow motion time is less than 0.5 but greater than 0.
		if (slowTimeRemaining < 0.5f && slowTimeRemaining > 0) 
		{
			Time.timeScale = 1; // Sets time scale to 1.
		}

		// if slow motion time is less than/equal to 0, and time scale is greater than/equal to 1%.
		if (slowTimeRemaining <= 0) 
		{
			if (Time.timeScale >= 0.01f)
			{
				slowTimeRemaining = 0; // Stops decrementing slow time remaining.
				Time.timeScale -= 0.2f * Time.unscaledDeltaTime; // Has time scale back to normal.
				timeScaleControllerScript.enabled = false; // Turns off timescale controller.
				PressToContinue.SetActive (true); // Activates "Press A to continue" text.

				// Player presses A button or space during this time.
				if (Input.GetKeyDown ("joystick button 0") || Input.GetKeyDown ("space")) 
				{
					Time.timeScale = initialTimeScale; // sets timescale to this.
					PressToContinue.SetActive (false); // tuens off press to continue text for that frame.

					// Checks if Game Over UI is active in the heirarchy.
					if (GameOverUI.activeInHierarchy == false) 
					{
						GameOverUI.SetActive (true); // Turns on Game over UI.
					}

					// Plays game over loop.
					if (!GameOverLoop.isPlaying)
					{
						GameOverLoop.PlayDelayed (4.0f); // Delays.
					}
				}
			}

			if (Time.timeScale < 0.01f) 
			{
				if (Input.GetKeyDown ("joystick button 0") || Input.GetKeyDown ("space"))
				{
					Time.timeScale = initialTimeScale; // sets timescale to this.
					PressToContinue.SetActive (false); // tuens off press to continue text for that frame.

					// Turns on Game over UI.
					if (GameOverUI.activeInHierarchy == false) 
					{
						GameOverUI.SetActive (true);
					}

					// Plays game over loop.
					if (!GameOverLoop.isPlaying) 
					{
						GameOverLoop.PlayDelayed (4.0f);
					}
				}
			}
		}

		// if health is below 0 and the Game Over UI is active.
		if (Health <= 0 && GameOverUI.activeSelf == true) 
		{
			PressToContinue.SetActive (false); // Turns off press to continue UI message.

			// When timescale is above an amount, to decrement time scale.
			if (Time.timeScale >= 0.0166f) 
			{
				Time.timeScale -= Time.unscaledDeltaTime * TimeSlowingSpeed; // Slows time down slowly.
				PressToContinue.SetActive (false); // Turns off press to continue UI.
			}

			// When timescale is low enough.
			if (Time.timeScale < 0.0165f)
			{
				Time.timeScale = 0.0165f;
				PressToContinue.SetActive (false); // Turns off press to continue UI.
			}
		}
	}
}

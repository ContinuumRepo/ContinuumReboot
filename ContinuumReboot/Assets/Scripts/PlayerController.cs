using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using XInputDotNetPure;

public class PlayerController : MonoBehaviour 
{
	private GameController gameControllerScript;
	private TimescaleController timeScaleControllerScript;
	private MeshCollider PlayerCollider;
	private MeshRenderer PlayerMesh;
	private AudioSourcePitchByTimescale BGMPitchScript;
	public ColorCorrectionCurves ColorCorrectionCurvesScript;
	private bool playedGameOverSound;
	private CameraShake camShakeScrpt;
	public float shakeTime = 0.5f;
	public float shakeAmount = 1.0f;

	[Header ("Movement")]
	private Rigidbody rb;
	public float speed = 10.0f;
	public float tilt;

	[Header ("Shooting")]
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;
	public GameObject EmptyInstantdestroy;

	[Header ("Powerups")]
	public GameObject[] Powerups;
	public enum powerup {RegularShot, DoubleShot, TriShot, BeamShot, shield, horizontalBeam, Clone}
	public powerup CurrentPowerup;
	public GameObject RegularShot;
	public GameObject DoubleShot;
	public GameObject TriShot;
	public GameObject BeamShot;
	public GameObject Shield;
	public GameObject HorizontalBeam;
	public GameObject ClonedPlayer;
	public bool isClone;
	public GameObject DoubleShotIcon;
	public GameObject TriShotIcon;
	public GameObject BeamShotIcon;
	public GameObject ShieldIcon;
	public GameObject HorizontalBeamIcon;
	public GameObject CloneIcon;

	public Lens LensScript;
	public float powerupTime = 0;
	public float powerupDuration = 10.0f;
	public AudioSource powerupTimeRunningOut;
	public AudioSource powerupDeactivateAudio;
	public ParticleSystem ActivePowerupParticles;
	public ParticleSystem TimeRunningOutParticles;
	public Image PowerupMeter;

	[Header ("Health")]
	public float Health;
	public float startingHealth = 100;
	public float minHealth = 0;
	public Image HealthImage;
	public float vibrationAmount = 1;
	public float vibrationDuration = 0.4f;
	public float vibrationTime;
	public Text HealthText;

	[Header ("Game Over")]
	public bool initialPart;
	public float initialTimeScale = 0.1f;
	public float slowTimeDuration = 3.0f;
	public float slowTimeRemaining = 0.0f;
	public AudioSource BGMMusic;
	public GameObject PressToContinue;
	public GameObject DeactivatePlayerElements;
	public float TimeSlowingSpeed = 0.1f;
	public float minTimeScale = 0.0f;
	public GameObject GameOverUI;
	public AudioSource GameOverSound;
	public AudioSource GameOverLoop;
	public GameObject gameOverExplosion;

	void Start () 
	{
		DoubleShotIcon.SetActive (false);
		BeamShotIcon.SetActive (false);
		TriShotIcon.SetActive (false);
		ShieldIcon.SetActive (false);
		HorizontalBeamIcon.SetActive (false);
		CloneIcon.SetActive (false);
		// Finds Camera Shake script.
		camShakeScrpt = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraShake>();

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
		//ColorCorrectionCurvesScript.enabled = false;

		// Turns off GameOver UI.
		GameOverUI.SetActive (false);

		// Start powerup conditions.
		CurrentPowerup = powerup.RegularShot;
		BeamShot.SetActive (false);
		Shield.SetActive (false);
		HorizontalBeam.SetActive (false);

		initialPart = false;
		PressToContinue.SetActive (false);
		slowTimeRemaining = slowTimeDuration;
		LensScript = Camera.main.GetComponent<Lens> ();
		//LensScript.enabled = false;
	}

	void Update () 
	{
		if (!isClone) {
			vibrationTime -= Time.unscaledDeltaTime;

			if (vibrationTime > 0) {
				GamePad.SetVibration (PlayerIndex.One, vibrationAmount, vibrationAmount);
			}

			if (vibrationTime <= 0) {
				vibrationTime = 0;
				GamePad.SetVibration (PlayerIndex.One, 0, 0);
			}

			// The UI fill amount of the health.
			HealthImage.fillAmount = Health / 100;
			HealthImage.color = new Color (25 / Health, Health / 100, 0, 0.9f);
			HealthText.text = string.Format ("{0:0}", Mathf.Round (Health)) + "%";
			PowerupMeter.color = new Color (1 / powerupTime, powerupTime / 10, powerupTime / 15, 1.0f);

			/// POWERUPS ///
			PowerupMeter.fillAmount = powerupTime / powerupDuration; // UI fill amount for powerup.

			// No powerup
			if (CurrentPowerup == powerup.RegularShot) {
				shot = RegularShot;
				ClonedPlayer.SetActive (false);
				gameControllerScript.PowerupText.text = "" + "- 2.5% x shot";
				BeamShot.SetActive (false);
				HorizontalBeam.SetActive (false);
				//LensScript.enabled = false;

				if (LensScript.radius > 0) {
					LensScript.radius -= 1f * Time.unscaledDeltaTime;
				}
				if (LensScript.radius < 0) {
					LensScript.radius = 0;
				}

				DoubleShotIcon.SetActive (false);
				BeamShotIcon.SetActive (false);
				TriShotIcon.SetActive (false);
				ShieldIcon.SetActive (false);
				HorizontalBeamIcon.SetActive (false);
				CloneIcon.SetActive (false);
			}

			// Double Shot
			if (CurrentPowerup == powerup.DoubleShot) {
				shot = DoubleShot;
				powerupTime -= Time.unscaledDeltaTime;
				gameControllerScript.PowerupText.text = "Double Shot";
				DoubleShotIcon.SetActive (true);
			}

			// Tri shot.
			if (CurrentPowerup == powerup.TriShot) {
				shot = TriShot;
				powerupTime -= Time.unscaledDeltaTime;
				gameControllerScript.PowerupText.text = "Triple Shot";
				TriShotIcon.SetActive (true);
			}

			// Beam shot.
			if (CurrentPowerup == powerup.BeamShot) {
				shot = EmptyInstantdestroy;
				BeamShot.SetActive (true);
				powerupTime -= Time.unscaledDeltaTime;
				gameControllerScript.PowerupText.text = "Ultra Beam";
				BeamShotIcon.SetActive (true);
			}

			// Shield.
			if (CurrentPowerup == powerup.shield) {
				shot = EmptyInstantdestroy;
				Shield.SetActive (true);
				powerupTime -= Time.unscaledDeltaTime;
				gameControllerScript.PowerupText.text = "Shield";
				LensScript.enabled = true;

				if (LensScript.radius <= 0.6f && LensScript.radius >= 0) {
					LensScript.radius += 0.1f * Time.unscaledDeltaTime;
				}
				ShieldIcon.SetActive (true);
			}

			// Horizontal Beam
			if (CurrentPowerup == powerup.horizontalBeam) {	
				shot = EmptyInstantdestroy;
				HorizontalBeam.SetActive (true);
				powerupTime -= Time.unscaledDeltaTime;
				gameControllerScript.PowerupText.text = "Horizontal Beam";
			}

			// Clone Player
			if (CurrentPowerup == powerup.Clone) {
				ClonedPlayer.SetActive (true);
				powerupTime -= Time.unscaledDeltaTime;
				gameControllerScript.PowerupText.text = "Cloned!";
				CloneIcon.SetActive (true);
			}
			
			// Warning powerup time.
			if (powerupTime < 3.0f && powerupTime > 2.8f) {
				ActivePowerupParticles.Stop ();
				powerupTimeRunningOut.Play ();
				TimeRunningOutParticles.Play ();
			}

			// When powerup runs out.
			if (powerupTime < 0) {
				powerupTime = 0;
				CurrentPowerup = powerup.RegularShot;
				BeamShot.SetActive (false);
				Shield.SetActive (false);
			}

			// Powerup ran out.
			if (powerupTime > 0 && powerupTime < 0.05f && !powerupDeactivateAudio.GetComponent<AudioSource> ().isPlaying) {
				powerupDeactivateAudio.Play ();
			}
			
			/// Health ///

			// Health at 0.
			if (Health <= 0) {
				//initialPart = true;
				PlayerMesh.enabled = false;
				PlayerCollider.enabled = false;
				DeactivatePlayerElements.SetActive (false);
				gameControllerScript.StopAllCoroutines ();
				GameOver ();
				timeScaleControllerScript.enabled = false;
				BGMPitchScript.addPitch = 0;
			}

			if (ColorCorrectionCurvesScript.saturation < 1) {
				ColorCorrectionCurvesScript.saturation += 0.5f * Time.unscaledDeltaTime;
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
			//rb.rotation = Quaternion.Euler (0.0f, rb.velocity.x * -tilt, 0.0f);
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
			vibrationTime = vibrationDuration;
			camShakeScrpt.shakeAmount = shakeAmount;
			camShakeScrpt.shakeDuration = shakeTime;
		}
	}

	void GameOver ()
	{
		slowTimeRemaining -= Time.unscaledDeltaTime; // decrements slow time remaining.
		BGMMusic.Stop ();

		if (slowTimeRemaining > 0.5f) 
		{
			Time.timeScale = 0.01f;
		}

		if (slowTimeRemaining < 0.5f && slowTimeRemaining > 0) 
		{
			Time.timeScale = 1;
		}

		if (slowTimeRemaining <= 0 && Time.timeScale >= 0.01f) 
		{
			slowTimeRemaining = 0; // Stops decrementing slow time remaining.
			Time.timeScale -= 0.2f * Time.unscaledDeltaTime; // Has time scale back to normal.
			timeScaleControllerScript.enabled = false; // Turns off timescale controller.
			PressToContinue.SetActive (true); // Activates "Press A to continue" text.

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

		if (slowTimeRemaining <= 0 && Time.timeScale < 0.01f) 
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

		if (Health <= 0 && GameOverUI.activeSelf == true) 
		{
			PressToContinue.SetActive (false);

			// When timescale is above an amount, to decrement time scale.
			if (Time.timeScale >= 0.0166f) 
			{
				Time.timeScale -= Time.unscaledDeltaTime * TimeSlowingSpeed;
				PressToContinue.SetActive (false);
			}

			// When timescale is low enough.
			if (Time.timeScale < 0.0165f)
			{
				Time.timeScale = 0.0165f;
				PressToContinue.SetActive (false);
			}
		}
	}
}

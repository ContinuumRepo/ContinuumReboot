using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class PlayerController : MonoBehaviour 
{
	private GameController gameControllerScript;
	private TimescaleController timeScaleControllerScript;
	private MeshCollider PlayerCollider;
	private MeshRenderer PlayerMesh;
	private AudioSourcePitchByTimescale BGMPitchScript;
	private ColorCorrectionCurves ColorCorrectionCurvesScript;
	private bool playedGameOverSound;

	[Header ("Movement")]
	private Rigidbody rb;
	public float speed = 10.0f;
	public float tilt;

	[Header ("Shooting")]
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;

	[Header ("Powerups")]
	public GameObject[] Powerups;
	public enum powerup {RegularShot, DoubleShot, TriShot, BeamShot, shield, rowClear, disableStacking}
	public powerup CurrentPowerup;
	public GameObject RegularShot;
	public GameObject DoubleShot;
	public GameObject TriShot;
	public GameObject BeamShot;
	public GameObject Shield;
	public float powerupTime = 0;
	public float powerupDuration = 10.0f;
	public AudioSource powerupTimeRunningOut;
	public GameObject powerupDeactivateAudio;
	public ParticleSystem ActivePowerupParticles;
	public ParticleSystem TimeRunningOutParticles;
	public Image PowerupMeter;

	[Header ("Health")]
	public float Health;
	public float startingHealth = 100;
	public float minHealth = 0;
	public Image HealthImageL;
	public Image HealthImageR;

	[Header ("Game Over")]
	public GameObject DeactivatePlayerElements;
	public float TimeSlowingSpeed = 0.1f;
	public float minTimeScale = 0.0f;
	public GameObject GameOverUI;
	public AudioSource GameOverSound;
	public AudioSource GameOverLoop;
	public GameObject gameOverExplosion;

	void Start () 
	{
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
		HealthImageL.fillAmount = Health / 100;
		HealthImageR.fillAmount = Health / 100;

		// Timescale controller script.
		timeScaleControllerScript = GameObject.FindGameObjectWithTag ("TimeScaleController").GetComponent<TimescaleController> ();

		// Background music pitch by timescale script.
		BGMPitchScript = GameObject.FindGameObjectWithTag ("BGM").GetComponent<AudioSourcePitchByTimescale>();

		// Finds color correction curves script.
		ColorCorrectionCurvesScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ColorCorrectionCurves>();
		ColorCorrectionCurvesScript.enabled = false;

		// Turns off GameOver UI.
		GameOverUI.SetActive (false);

		// Start powerup conditions.
		CurrentPowerup = powerup.RegularShot;
		BeamShot.SetActive (false);
		Shield.SetActive (false);
	}

	void Update () 
	{
		// The UI fill amount of the health.
		HealthImageL.fillAmount = Health / 100;
		HealthImageR.fillAmount = Health / 100;
		HealthImageR.color = new Color (25/Health, Health/100, 0, 0.9f);
		HealthImageL.color = new Color (25/Health, Health/100, 0, 0.9f);

		/// POWERUPS ///
		PowerupMeter.fillAmount = powerupTime / powerupDuration; // UI fill amount for powerup.

		// No powerup
		if (CurrentPowerup == powerup.RegularShot) 
		{
			shot = RegularShot;
			gameControllerScript.PowerupText.text = "" + "Cost: 2.5% score/bullet";
			BeamShot.SetActive (false);
		}

		// Double Shot
		if (CurrentPowerup == powerup.DoubleShot) 
		{
			shot = DoubleShot;
			powerupTime -= Time.unscaledDeltaTime;
			gameControllerScript.PowerupText.text = "Double Shot " + "Free";
		}

		// Tri shot.
		if (CurrentPowerup == powerup.TriShot) 
		{
			shot = TriShot;
			powerupTime -= Time.unscaledDeltaTime;
			gameControllerScript.PowerupText.text = "Triple Shot " + "Free";
		}

		// Beam shot.
		if (CurrentPowerup == powerup.BeamShot) 
		{
			shot = RegularShot;
			BeamShot.SetActive (true);
			powerupTime -= Time.unscaledDeltaTime;
			gameControllerScript.PowerupText.text = "Ultra Beam";
		}

		// Shield.
		if (CurrentPowerup == powerup.shield) 
		{
			Shield.SetActive (true);
			powerupTime -= Time.unscaledDeltaTime;
			gameControllerScript.PowerupText.text = "Shield";
		}
			
		// Warning powerup time.
		if (powerupTime < 3.0f && powerupTime > 2.8f)
		{
			ActivePowerupParticles.Stop ();
			powerupTimeRunningOut.Play ();
			TimeRunningOutParticles.Play ();
		}

		// When powerup runs out.
		if (powerupTime < 0) 
		{
			powerupTime = 0;
			CurrentPowerup = powerup.RegularShot;
			BeamShot.SetActive (false);
			Shield.SetActive (false);
		}

		// Powerup ran out.
		if (powerupTime > 0 && powerupTime < 0.02f && !powerupDeactivateAudio.GetComponent<AudioSource>().isPlaying) 
		{
			Instantiate (powerupDeactivateAudio, Vector3.zero, Quaternion.identity);
		}
			
		/// Health ///

		// Health at 0.
		if (Health <= 0) 
		{
			PlayerMesh.enabled = false;
			PlayerCollider.enabled = false;
			DeactivatePlayerElements.SetActive (false);
			gameControllerScript.StopAllCoroutines ();
			GameOver ();
			timeScaleControllerScript.enabled = false;
			BGMPitchScript.addPitch = 0;

			// Turn on Game over UI.
			if (GameOverUI.activeInHierarchy == false) 
			{
				GameOverUI.SetActive (true);
			}

			// Play game over loop.
			if (!GameOverLoop.isPlaying) 
			{
				GameOverLoop.PlayDelayed (4.0f);
			}
		}

		// Warning hit with low health.
		if (Health > 0 && Health <= 25 && ColorCorrectionCurvesScript.saturation <= 1) 
		{
			ColorCorrectionCurvesScript.enabled = true;
			ColorCorrectionCurvesScript.saturation += 0.1f * Time.unscaledDeltaTime;
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
			rb.rotation = Quaternion.Euler (0.0f, rb.velocity.x * -tilt, 0.0f);
		}

		/// Shooting functionality ///

		// PC Controller Input.
		if (Input.GetButton ("Fire1") && Time.time > nextFire && gameControllerScript.CurrentScore > 0 && Health > minHealth)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}

		// XBox 360 controller input for Windows.
		if (Input.GetAxisRaw ("Fire1") < 0 && Time.time > nextFire && gameControllerScript.CurrentScore > 0 && Health > minHealth)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}
	}

	void GameOver ()
	{
		// When timescale is above an amount, to decrement time scale.
		if (Time.timeScale >= 0.0166f)
		{
			Time.timeScale -= Time.unscaledDeltaTime * TimeSlowingSpeed;
		}

		// When timescale is low enough.
		if (Time.timeScale < 0.0165f) 
		{
			Time.timeScale = 0.0165f;
		}
	}
}

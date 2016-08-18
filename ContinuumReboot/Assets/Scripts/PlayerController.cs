using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour 
{
	private GameController gameControllerScript;
	private TimescaleController timeScaleControllerScript;
	private MeshCollider PlayerCollider;
	private MeshRenderer PlayerMesh;
	private AudioSourcePitchByTimescale BGMPitchScript;

	[Header ("Movement")]
	private Rigidbody rb;
	public float speed = 10.0f;
	public float tilt;

	[Header ("Shooting")]
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;

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

		// Turns off GameOver UI.
		GameOverUI.SetActive (false);
	}

	void Update () 
	{
		// The UI fill amount of the health.
		HealthImageL.fillAmount = Health / 100;
		HealthImageR.fillAmount = Health / 100;

		if (Health <= 0) 
		{
			PlayerMesh.enabled = false;
			PlayerCollider.enabled = false;
			DeactivatePlayerElements.SetActive (false);
			gameControllerScript.StopAllCoroutines ();
			GameOver ();
			timeScaleControllerScript.enabled = false;
			BGMPitchScript.addPitch = 0;

			if (GameOverUI.activeInHierarchy == false) 
			{
				GameOverUI.SetActive (true);
			}
		}
	}

	void FixedUpdate ()
	{
		if (gameControllerScript.isPreGame == false) 
		{
			// Movement from Space Shooter tutorial. (Uses WASD/arrow keys).
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			// Assigns movement to a Vector 3, changes sensitivity according to timescale.
			Vector3 movement = new Vector3 (moveHorizontal * (1/Time.timeScale), moveVertical * (1/Time.timeScale), 0.0f);
			rb.velocity = movement * speed;
			rb.position = new Vector3 (rb.position.x, rb.position.y, 0);
			rb.rotation = Quaternion.Euler (0.0f, rb.velocity.x * -tilt, 0.0f);
		}

		// Shooting functionality
		if (Input.GetButton ("Fire1") && Time.time > nextFire && gameControllerScript.CurrentScore > 0 && Health > minHealth)
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

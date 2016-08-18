using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	private GameController gameControllerScript;
	private TimescaleController timeScaleControllerScript;
	private MeshCollider PlayerCollider;
	private MeshRenderer PlayerMesh;
	public GameObject DeactivatePlayerElements;

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

	[Header ("Death")]
	public float TimeSlowingSpeed = 0.1f;
	public float minTimeScale = 0.0f;

	void Start () 
	{
		rb = GetComponent<Rigidbody> ();

		gameControllerScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		PlayerCollider = GameObject.Find ("Collider").GetComponent<MeshCollider>();
		PlayerMesh = GameObject.Find ("PlayerMesh").GetComponent<MeshRenderer>();
		Health = startingHealth;

		timeScaleControllerScript = GameObject.FindGameObjectWithTag ("TimeScaleController").GetComponent<TimescaleController> ();
	}

	void Update () 
	{
		if (Health <= 0) 
		{
			PlayerMesh.enabled = false;
			PlayerCollider.enabled = false;
			DeactivatePlayerElements.SetActive (false);
			gameControllerScript.StopAllCoroutines ();
			GameOver ();
			timeScaleControllerScript.enabled = false;
		}
	}

	void FixedUpdate ()
	{
		if (gameControllerScript.isPreGame == false) 
		{
			// Movement from Space Shooter tutorial. (Uses WASD/arrow keys).
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			Vector3 movement = new Vector3 (moveHorizontal * (1/Time.timeScale), moveVertical * (1/Time.timeScale), 0.0f);
			rb.velocity = movement * speed;

			rb.position = new Vector3 (rb.position.x, rb.position.y, 0);

			rb.rotation = Quaternion.Euler (0.0f, rb.velocity.x * -tilt, 0.0f);
		}

		if (Input.GetButton ("Fire1") && Time.time > nextFire && gameControllerScript.CurrentScore > 0)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}
	}

	void GameOver ()
	{
		if (Time.timeScale > 0.0166f)
		{
			Time.timeScale -= Time.unscaledDeltaTime * TimeSlowingSpeed;
		}

		if (Time.timeScale <= minTimeScale) 
		{
			Time.timeScale = 0.0166f;
		}
	}
}

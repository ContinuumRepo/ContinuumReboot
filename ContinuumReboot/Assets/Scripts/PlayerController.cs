using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	private GameController gameControllerScript;

	[Header ("Movement")]
	private Rigidbody rb;
	public float speed = 10.0f;
	public float tilt;


	[Header ("Shooting")]
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	private float nextFire;


	void Start () 
	{
		rb = GetComponent<Rigidbody> ();

		gameControllerScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
	}

	void Update () 
	{
		
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

		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}
	}		
}

using UnityEngine;
using System.Collections;

public class CloneScript : MonoBehaviour 
{
	public PlayerController playerControllerScript;
	public SmoothFollowOrig smoothFollowScript;
	public Transform player;
	public float distance = 3;
	public float delay = 1;
	public float fireRate = 0.2f;
	private float nextFire; 

	[Header ("Shooting")]
	public GameObject Bullet;
	public Transform shotSpawn;

	void Start () 
	{
		playerControllerScript = GameObject.Find ("Player").GetComponent<PlayerController> ();
		smoothFollowScript = GetComponent<SmoothFollowOrig> ();
		player = GameObject.Find ("Player").transform;

		// If target is anything but player.
		if (smoothFollowScript.target != player) 
		{
			// Try to find a cube.
			if (GameObject.FindGameObjectWithTag ("Cube") != null)
			{
				// Found? Assigned!
				smoothFollowScript.target = GameObject.FindGameObjectWithTag ("Cube").transform;
			}

			// Not found.
			if (GameObject.FindGameObjectWithTag ("Cube") == null) 
			{
				// Follow player.
				smoothFollowScript.target = player;
			}
		}

		// If target is on player.
		if (smoothFollowScript.target == player) 
		{
			// Still try to find a cube.
			if (GameObject.FindGameObjectWithTag ("Cube") != null) 
			{
				// Found? Assigned!
				smoothFollowScript.target = GameObject.FindGameObjectWithTag ("Cube").transform;
			}

			// Not found.
			if (GameObject.FindGameObjectWithTag ("Cube") == null) 
			{
				// Follow player.
				smoothFollowScript.target = player;
			}
		}

	}

	void Update () 
	{
		// If target is on player.
		if (smoothFollowScript.target == player) 
		{
			// Still try to find a cube.
			if (GameObject.FindGameObjectWithTag ("Cube") != null) 
			{
				// Found? Assigned!
				smoothFollowScript.target = GameObject.FindGameObjectWithTag ("Cube").transform;
			}

			// Not found.
			if (GameObject.FindGameObjectWithTag ("Cube") == null) 
			{
				// Follow player.
				smoothFollowScript.target = player;
			}
		}

		if (smoothFollowScript.target == null) 
		{
			// Still try to find a cube.
			if (GameObject.FindGameObjectWithTag ("Cube") != null) 
			{
				// Found? Assigned!
				smoothFollowScript.target = GameObject.FindGameObjectWithTag ("Cube").transform;
			}

			// Not found.
			if (GameObject.FindGameObjectWithTag ("Cube") == null) 
			{
				// Follow player.
				smoothFollowScript.target = player;
			}
		}

		// Automatic shooting.
		if (Time.unscaledTime > nextFire) 
		{
			if (Time.timeScale > 0) 
			{
				if (playerControllerScript.Health >= 25) 
				{
					Instantiate (Bullet, shotSpawn.position, Quaternion.Euler (0, 0, 180));
				}
				nextFire = Time.unscaledTime + fireRate;
				Change ();
			}
		}
	}

	void Change ()
	{
		// If target is anything but player.
		if (smoothFollowScript.target != player) 
		{
			// Try to find a cube.
			if (GameObject.FindGameObjectWithTag ("Cube") != null)
			{
				// Found? Assigned!
				smoothFollowScript.target = GameObject.FindGameObjectWithTag ("Cube").transform;
			}

			// Not found.
			if (GameObject.FindGameObjectWithTag ("Cube") == null) 
			{
				// Follow player.
				smoothFollowScript.target = player;
			}
		}

		// If target is on player.
		if (smoothFollowScript.target == player) 
		{
			// Still try to find a cube.
			if (GameObject.FindGameObjectWithTag ("Cube") != null) 
			{
				// Found? Assigned!
				smoothFollowScript.target = GameObject.FindGameObjectWithTag ("Cube").transform;
			}

			// Not found.
			if (GameObject.FindGameObjectWithTag ("Cube") == null) 
			{
				// Follow player.
				smoothFollowScript.target = player;
			}
		}
	}
}
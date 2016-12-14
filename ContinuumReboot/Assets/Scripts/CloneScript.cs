using UnityEngine;
using System.Collections;
using UnityStandardAssets.Cameras;

public class CloneScript : MonoBehaviour 
{
	public float distance = 3;
	public float delay = 1;
	public float fireRate = 0.2f;
	private float nextFire; 
	public GameObject Bullet;
	public Transform shotSpawn;

	private PlayerController playerControllerScript;
	private SmoothFollowOrig smoothFollowScript;
	private Transform player;

	void Start () 
	{
		FindComponents ();

		// If target is anything but player.
		if (smoothFollowScript.target != player) 
		{
			// Try to find a cube.
			if (GameObject.FindGameObjectWithTag ("Cube") != null)
			{
				// Found? Assigned!
				FindCubeTarget ();
			}

			// Not found.
			if (GameObject.FindGameObjectWithTag ("Cube") == null) 
			{
				// Follow player.
				FindPlayerTarget ();
			}
		}

		// If target is on player.
		if (smoothFollowScript.target == player) 
		{
			// Still try to find a cube.
			if (GameObject.FindGameObjectWithTag ("Cube") != null) 
			{
				// Found? Assigned!
				FindCubeTarget ();
			}

			// Not found.
			if (GameObject.FindGameObjectWithTag ("Cube") == null) 
			{
				// Follow player.
				FindPlayerTarget ();
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
				FindCubeTarget ();
			}

			// Not found.
			if (GameObject.FindGameObjectWithTag ("Cube") == null) 
			{
				// Follow player.
				FindPlayerTarget ();
			}
		}

		if (smoothFollowScript.target == null) 
		{
			// Still try to find a cube.
			if (GameObject.FindGameObjectWithTag ("Cube") != null) 
			{
				// Found? Assigned!
				FindCubeTarget ();
			}

			// Not found.
			if (GameObject.FindGameObjectWithTag ("Cube") == null) 
			{
				// Follow player.
				FindPlayerTarget ();
			}
		}

		// Automatic shooting.
		if (Time.unscaledTime > nextFire && smoothFollowScript.target != player) 
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
		if (
			smoothFollowScript.target != player
			) 
		{
			// Try to find a cube.
			if (GameObject.FindGameObjectWithTag ("Cube") != null)
			{
				// Found? Assigned!
				FindCubeTarget ();

			}

			// Not found.
			if (GameObject.FindGameObjectWithTag ("Cube") == null) 
			{
				// Follow player.
				FindPlayerTarget ();
			}
		}

		// If target is on player.
		if (
			smoothFollowScript.target == player
			) 
		{
			// Still try to find a cube.
			if (GameObject.FindGameObjectWithTag ("Cube") != null) 
			{
				// Found? Assigned!
				FindCubeTarget ();
			}

			// Not found.
			if (GameObject.FindGameObjectWithTag ("Cube") == null) 
			{
				// Follow player.
				FindPlayerTarget ();
			}
		}
	}

	void FindComponents ()
	{
		playerControllerScript = GameObject.Find ("Player").GetComponent<PlayerController> ();
		smoothFollowScript = GetComponent<SmoothFollowOrig> ();
		player = GameObject.Find ("Player").transform;
	}

	void FindCubeTarget ()
	{
		smoothFollowScript.target = GameObject.FindGameObjectWithTag ("Cube").transform;
	}

	void FindPlayerTarget ()
	{
		smoothFollowScript.target = player;
	}
}
using UnityEngine;
using System.Collections;
using UnityStandardAssets.Cameras;

public class CloneScript : MonoBehaviour 
{
	public PlayerController playerControllerScript;
	public SmoothFollowOrig smoothFollowScript;
	public LookatTarget lookScript;
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
		//lookScript = GetComponent<LookatTarget> ();
		player = GameObject.Find ("Player").transform;

		// If target is anything but player.
		if (
			//lookScript.Target != player
			smoothFollowScript.target != player
			) 
		{
			// Try to find a cube.
			if (GameObject.FindGameObjectWithTag ("Cube") != null)
			{
				// Found? Assigned!
				smoothFollowScript.target = GameObject.FindGameObjectWithTag ("Cube").transform;
				//lookScript.Target = GameObject.FindGameObjectWithTag ("Cube").transform;
			}

			// Not found.
			if (GameObject.FindGameObjectWithTag ("Cube") == null) 
			{
				// Follow player.
				smoothFollowScript.target = player;
				//lookScript.Target = player;
			}
		}

		// If target is on player.
		if (
			smoothFollowScript.target == player
			//lookScript.Target == player
			) 
		{
			// Still try to find a cube.
			if (GameObject.FindGameObjectWithTag ("Cube") != null) 
			{
				// Found? Assigned!
				smoothFollowScript.target = GameObject.FindGameObjectWithTag ("Cube").transform;
				//lookScript.Target = GameObject.FindGameObjectWithTag ("Cube").transform;
			}

			// Not found.
			if (GameObject.FindGameObjectWithTag ("Cube") == null) 
			{
				// Follow player.
				smoothFollowScript.target = player;
				//lookScript.Target = player;
			}
		}
	}

	void Update () 
	{
		// If target is on player.
		if (
			smoothFollowScript.target == player
			//lookScript.Target == player
			) 
		{
			// Still try to find a cube.
			if (GameObject.FindGameObjectWithTag ("Cube") != null) 
			{
				// Found? Assigned!
				smoothFollowScript.target = GameObject.FindGameObjectWithTag ("Cube").transform;
				//lookScript.Target = GameObject.FindGameObjectWithTag ("Cube").transform;
			}

			// Not found.
			if (GameObject.FindGameObjectWithTag ("Cube") == null) 
			{
				// Follow player.
				smoothFollowScript.target = player;
				//lookScript.Target = player;
			}
		}

		if (
			smoothFollowScript.target == null
			//lookScript.Target == null
			) 
		{
			// Still try to find a cube.
			if (GameObject.FindGameObjectWithTag ("Cube") != null) 
			{
				// Found? Assigned!
				smoothFollowScript.target = GameObject.FindGameObjectWithTag ("Cube").transform;
				//lookScript.Target = GameObject.FindGameObjectWithTag ("Cube").transform;
			}

			// Not found.
			if (GameObject.FindGameObjectWithTag ("Cube") == null) 
			{
				// Follow player.
				smoothFollowScript.target = player;
				//lookScript.Target = player;
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
			//lookScript.Target != player
			) 
		{
			// Try to find a cube.
			if (GameObject.FindGameObjectWithTag ("Cube") != null)
			{
				// Found? Assigned!
				smoothFollowScript.target = GameObject.FindGameObjectWithTag ("Cube").transform;
				//lookScript.Target = GameObject.FindGameObjectWithTag ("Cube").transform;

			}

			// Not found.
			if (GameObject.FindGameObjectWithTag ("Cube") == null) 
			{
				// Follow player.
				smoothFollowScript.target = player;
				//lookScript.Target = player;
			}
		}

		// If target is on player.
		if (
			smoothFollowScript.target == player
			//lookScript.Target == player
			) 
		{
			// Still try to find a cube.
			if (GameObject.FindGameObjectWithTag ("Cube") != null) 
			{
				// Found? Assigned!
				smoothFollowScript.target = GameObject.FindGameObjectWithTag ("Cube").transform;
				//lookScript.Target = GameObject.FindGameObjectWithTag ("Cube").transform;
			}

			// Not found.
			if (GameObject.FindGameObjectWithTag ("Cube") == null) 
			{
				// Follow player.
				smoothFollowScript.target = player;
				//lookScript.Target = player;
			}
		}
	}
}
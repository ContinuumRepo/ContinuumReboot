using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour 
{
	public GameObject Bullet;
	public float fireRate;
	private float nextFire;
	public Transform ShotSpawn;
	public int BulletCost;
	public AudioSource FireError;
	public bool FireErrorSoundPlaying;
	public ParticleSystem FireErrorParticles;
	public bool isFiring;

	private GameController GameControllerScript;
	private bool noGCS;

	void Start () 
	{
		ShotSpawn = ShotSpawn.transform;
		FireErrorSoundPlaying = false;
	}

	void Update () 
	{
		// Checks if there is a GameController script attached to GameObject with "GameController" as its tag
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		
		if (gameControllerObject != null) 
		{
			GameControllerScript = gameControllerObject.GetComponent <GameController> ();
		}
		
		if (gameControllerObject == null) 
		{
			Debug.Log ("Cannot find 'GameController' script");
			//noGCS = true;
		}
		/*
		if (noGCS == true) 
		{
			return;
		}*/
		
		if (isFiring == true) 
		{
			if (Time.time > nextFire && 
			    GameControllerScript.currentScore > BulletCost) 
			{
				nextFire = Time.time + fireRate;
				Instantiate (Bullet, ShotSpawn.position, ShotSpawn.rotation);
				Mathf.Abs (GameControllerScript.currentScore -= BulletCost);
			}
			
			if (Time.time > nextFire && 
			    GameControllerScript.currentScore < BulletCost && 
			    FireErrorSoundPlaying == false) 
			{
				GameControllerScript.currentScore = 0;
				FireError.Play ();
				FireErrorSoundPlaying = true;
				
				if (FireErrorSoundPlaying == true) 
				{
					StartCoroutine (FireErrorCooldown ());
				}
				return;
			}

		}

	}
	/*
	public void Shoot ()
	{
		if (Input.GetButton ("Fire1") && Time.time > nextFire && 
		    GameControllerScript.currentScore > BulletCost) 
		{
			nextFire = Time.time + fireRate;
			Instantiate (Bullet, ShotSpawn.position, ShotSpawn.rotation);
			Mathf.Abs (GameControllerScript.currentScore -= BulletCost);
		}
		
		if (Input.GetButton ("Fire1") && Time.time > nextFire && 
		    GameControllerScript.currentScore < BulletCost && 
		    FireErrorSoundPlaying == false) 
		{
			GameControllerScript.currentScore = 0;
			FireError.Play ();
			FireErrorSoundPlaying = true;
			
			if (FireErrorSoundPlaying == true) 
			{
				StartCoroutine (FireErrorCooldown ());
			}
			return;
		}
	}*/

	IEnumerator FireErrorCooldown ()
	{
		FireErrorParticles.Play ();
		yield return new WaitForSeconds (0.25f);
		FireErrorSoundPlaying = false;
	}
}

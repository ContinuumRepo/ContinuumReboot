using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class Player : MonoBehaviour 
{
	public GameObject player;
	public GameObject PlayerMain;
	public Game gameControllerScript;

	[Header ("MOVEMENT")]
	public Vector2 ScreenDimensions;
	public Vector2 StartingPos;
	public Vector2 mousePos;
	public Vector2 centerPos;
	public Vector2 Ratio;
	public Vector2 difference;
	public Vector2 sensitivity;
	public Vector2 offset;
	public Vector2 BoundaryX;
	public Vector2 BoundaryY;
	public Vector3 tilt;
	public Transform MovementFollower;
	public Transform DivOne; public Transform DivTwo;
	public Transform DivThree; public Transform DivFour;
	public Transform DivFive; public Transform DivSix;
	public Transform DivSeven; public Transform DivEight;
	public Transform DivNine; public Transform DivTen;
	public SmoothFollowOrig DivisionTarget;
	public SmoothFollowOrig MovementSmoothScript;
	public float delay = 3.0f;

	[Range (1, 10)]
	public int Division;

	[Header ("SHOOTING")]
	public bool canShoot;
	public float shootDuration = 10.0f;
	public float remainingShootTime = 10.0f;
	private float nextFire;
	private float nextFireB;
	public float fireRate;
	public GameObject shot;
	public Transform shotSpawn;
	public Transform shotSpawnL;
	public Transform shotSpawnR;
	public ParticleSystem ShootingCharged;
	public Image ShootingPowerupImage;
	public Text ShootingPowerupText;
	public GameObject BigBullet;

	[Header ("HEALTH AND LIVES")]
	public MeshRenderer playerMesh;
	public int currentHealth;
	public Image HealthBar;
	public int currentLives;
	public Text LivesText;
	public int startHealth = 100;
	public int startLives = 3;
	public int minHealth = 0;
	public int minLives = 0;
	public int maxHealth = 100;
	public int maxLives = 10;
	public bool isDead;
	public float recoverTime = 4.0f;
	public GameObject[] RecoverEffect;
	public GameObject RecoverExplosion;
	public AudioSource RecoverAudio;
	public GameObject MeshObject; // The entire mesh and visuals of the player
	public ParticleSystem LivesParticleEffect;
	public GameObject Crosshair;
	public float slowTimeSpeed;
	public TimeController timeControllerScript;

	[Header ("WARP")]
	public bool isWarping;
	public float warpDuration;
	public float warpTimeRemaining;
	public GameObject WarpStars;
	public float warpStarsEmissionRate;
	private bool instantiatedWarpStars;
	public Collider PlayerCollider; // To make player invincible
	public bool PlayWarpSound;
	public AudioSource WarpStarsSound;
	public AudioSource BackgroundMusic;
	public Camera PowerupCam;
	//public GameObject Panel;

	[Header ("GAME OVER STATS")]
	public GameObject SaveStatsObject;
	public int GOScore;
	public Text GOScoreText;
	public int GOPoints;
	public Text GOPointsText;
	public Text HighScoreText;
	public Text TotalPointsText;
	public bool savedStats;

	void Start () 
	{

		Camera.main.GetComponent<MotionBlur> ().enabled = false;
		// Turns on player movement after a delay
		StartCoroutine (MovementSmoothEnabler ());

		// Finds gameControllerScript
		gameControllerScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<Game> ();

		// Sets starting division
		Division = 4;
		DivisionTarget.target = DivFour.transform;

		// Disables powerup GameObjects and powerup camera
		PowerupCam.enabled = false;
		//Panel.SetActive (false);

		// Sets starting health
		currentHealth = startHealth;
		LivesText.text = "" + currentLives + "";

		SaveStatsObject.SetActive (false);
		GOScoreText.text = "";
		GOPointsText.text = "";
		HighScoreText.text = "";
		TotalPointsText.text = "";

		savedStats = false;

	}

	void Update () 
	{
		LivesText.text = "" + currentLives + "";
		float healthbarfill = 0.01f * currentHealth;
		HealthBar.fillAmount = healthbarfill;
		HealthBar.color = new Color (0.25f/HealthBar.fillAmount, 0 + HealthBar.fillAmount, 0, 1);

		GOScoreText.text = "" + GOScore + "";
		GOPointsText.text = "" + GOPoints + "";
		HighScoreText.text = "" + PlayerPrefs.GetInt ("High Score") + "";
		TotalPointsText.text = "" + PlayerPrefs.GetInt ("Total Points") + "";


		if (currentLives < 0) 
		{
			currentLives = 0;
		}

		if (gameControllerScript.isPreGame == true) 
		{
			player.transform.position = new Vector3 ((5 * Mathf.Sin(Time.timeSinceLevelLoad)), (0), (0));
		}

		if (isWarping == false && gameControllerScript.isPreGame == false) 
		{
			// ** MOVEMENT ** //

			// Calculates screen dimensions for debug purposes
			ScreenDimensions = new Vector2 (Screen.width, Screen.height);

			// Calculates co ordinates of centre of screen in Vector 2 pixels
			centerPos = new Vector2 (Screen.width / 2, Screen.height / 4);

			// Calculates co ordinates of mouse position in Vector 2 pixels
			mousePos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);

			// Calculates difference in xPixels and yPixels
			difference = new Vector2 (centerPos.x - mousePos.x, centerPos.y - mousePos.y);

			// Calculates ratio of mouse position to center of screen from -1 to 1
			Ratio.x = 2 * (difference.x / ScreenDimensions.x);
			Ratio.y = Mathf.Clamp (2 * (difference.y / ScreenDimensions.y), -1f, 1f);

			if (currentLives > minLives)
			{
				// Assigns transform to the follower
				player.GetComponent<Transform>().position = new Vector3 (
					(Mathf.Clamp ((-Ratio.x * sensitivity.x) + offset.x, BoundaryX.x, BoundaryX.y)),
					(Mathf.Clamp ((-Ratio.y * sensitivity.y) + offset.y, BoundaryY.x, BoundaryY.y)),
					0
				);

				// Trying to rotate the player
				PlayerMain.GetComponent<Transform> ().rotation = Quaternion.Euler 
				(
						(PlayerMain.transform.position.y - player.transform.position.y) * tilt.x, 
						(PlayerMain.transform.position.x - player.transform.position.x) * tilt.y, 
						(PlayerMain.transform.position.x - player.transform.position.x) * tilt.z 
				);
			}

			if (PowerupCam.fieldOfView < 60)
			{
				PowerupCam.fieldOfView += Time.deltaTime * 40;
			}

		}
		/*
		// Starts a warp
		if (isWarping == true && warpTimeRemaining > 0) 
		{
			Warp();
		}*/

		/*
		// Turns off warping powerup
		if (isWarping == true && warpTimeRemaining <= 0)
		{
			StopWarp ();
		}*/

		if (gameControllerScript.isPaused == true) 
		{
			WarpStarsSound.Pause ();
		}

		if (gameControllerScript.isPaused == false) 
		{
			WarpStarsSound.UnPause ();
		}

		// Checks if in warp mode
		if (isWarping == true) 
		{
			// Assigns transform
			player.transform.position = new Vector3 (0, -20, 0);
			player.transform.rotation = Quaternion.identity;
		}
			
		// ** SHOOTING ** //
		if (canShoot == true && remainingShootTime > 0) 
		{
			remainingShootTime -= Time.deltaTime;

			if (Time.time > nextFire)
			{
				nextFire = Time.time + fireRate;
				Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
				//Instantiate(shot, shotSpawnL.position, shotSpawnL.rotation);
				//Instantiate(shot, shotSpawnR.position, shotSpawnR.rotation);
				ShootingCharged.Play ();
			}

			if (Time.time > nextFireB) 
			{
				nextFireB = nextFire + (nextFire / 2);
				Instantiate (shot, shotSpawnR.position, shotSpawnR.rotation);
			}
		}

		if (canShoot == true && remainingShootTime < 0) 
		{
			remainingShootTime = shootDuration;
			Instantiate (BigBullet, shotSpawn.position, shotSpawn.rotation);
			canShoot = false;
		}

		if (canShoot == false) 
		{
			ShootingCharged.Stop ();
		}

		ShootingPowerupImage.fillAmount = 0.1f * remainingShootTime;

		// Is begin shooting
		if (ShootingPowerupImage.fillAmount > 0 && ShootingPowerupImage.fillAmount < 1) 
		{
			ShootingPowerupImage.color = new Color (0.25f/ShootingPowerupImage.fillAmount, 0 + ShootingPowerupImage.fillAmount, 0, 1);
			ShootingPowerupText.text = "SHOOTING" + "";
		}

		// Finished shooting
		if (ShootingPowerupImage.fillAmount == 0 || ShootingPowerupImage.fillAmount == 1) 
		{
			ShootingPowerupImage.color = new Color (0, 1, 0, 0);
			ShootingPowerupText.text = "" + "";
		}

		/*
		// ** DIVISIONS ** //
		Mathf.Clamp (Division, 1, 10);

		if (Division > 10) 
		{
			Division = 10;
		}

		if (Division < 1) 
		{
			Division = 1;
		}

		// Divisions
		if (Division == 1) 
		{
			DivisionTarget.target = DivOne.transform;
		}

		if (Division == 2) 
		{
			DivisionTarget.target = DivTwo.transform;
		}

		if (Division == 3) 
		{
			DivisionTarget.target = DivThree.transform;
		}

		if (Division == 4) 
		{
			DivisionTarget.target = DivFour.transform;
		}

		if (Division == 5) 
		{
			DivisionTarget.target = DivFive.transform;
		}

		if (Division == 6) 
		{
			DivisionTarget.target = DivSix.transform;
		}

		if (Division == 7) 
		{
			DivisionTarget.target = DivSeven.transform;
		}

		if (Division == 8) 
		{
			DivisionTarget.target = DivEight.transform;
		}

		if (Division == 9) 
		{
			DivisionTarget.target = DivNine.transform;
		}

		if (Division == 10) 
		{
			DivisionTarget.target = DivTen.transform;
		}*/

		// ** HEALTH AND LIVES ** //

		if (currentHealth <= minHealth && isDead == false) 
		{
			isDead = true;
			StartCoroutine (DieAndRevive ());
		}

		if (currentLives <= minLives && Time.timeScale > 0) 
		{
			gameControllerScript.isGameOver = true;
			gameControllerScript.GameOverPanel.SetActive (true);
			//Time.timeScale -= slowTimeSpeed * Time.deltaTime;
			//timeControllerScript.enabled = false;
			//timeControllerScript.timeScalenow -= slowTimeSpeed * Time.deltaTime;
			PlayerCollider.enabled = false; // Turns off collider.
			playerMesh.enabled = false; // Turns off mesh renderer.
			MeshObject.SetActive (false);
			RecoverEffect[0].GetComponent<ParticleSystem> ().Play (); // Plays recover effect.
			RecoverEffect[1].GetComponent<ParticleSystem> ().Play (); // Plays recover effect.
			Crosshair.SetActive (false);
			canShoot = false;
			remainingShootTime = 0;
		}

		if (currentLives <= minLives && Time.timeScale <= 0.001f)
		{
			saveStats ();
		}
	}

	IEnumerator DieAndRevive ()
	{
		if (currentLives > minLives)
		{
			
			PlayerCollider.enabled = false; // Turns off collider.
			playerMesh.enabled = false; // Turns off mesh renderer.
			MeshObject.SetActive (false);
			RecoverEffect[0].GetComponent<ParticleSystem> ().Play (); // Plays recover effect.
			RecoverEffect[1].GetComponent<ParticleSystem> ().Play (); // Plays recover effect.
			Crosshair.SetActive (false);
			canShoot = false;
			remainingShootTime = 0;
			RecoverAudio.Play ();

			yield return new WaitForSeconds (recoverTime); // Time to recover.
			currentLives -= 1; // Decrements lives
			MeshObject.SetActive (true);
			playerMesh.enabled = true; // Turns on mesh renderer.
			PlayerCollider.enabled = true; // Turns on collider.
			currentHealth = maxHealth; // Revives health points.
			isDead = false;
			RecoverExplosion.GetComponent<ParticleSystem> ().Play ();
			Crosshair.SetActive (true);
			LivesParticleEffect.Play ();
			StopCoroutine (DieAndRevive ()); // Stops the method.
		}

	}

	void saveStats ()
	{
		if (savedStats == false) 
		{
			SaveStatsObject.SetActive (true);

			GOScore = gameControllerScript.currentScore;
			GOPoints = gameControllerScript.currentPoints;

			// High Scores
			if (PlayerPrefs.GetInt ("High Score") < GOScore) {
				PlayerPrefs.SetInt ("High Score", GOScore);
			}

			if (PlayerPrefs.GetInt ("High Score") > GOScore) {
				PlayerPrefs.SetInt ("High Score", PlayerPrefs.GetInt ("High Score"));
			}

			// Increment Total points
			PlayerPrefs.SetInt ("Total Points", PlayerPrefs.GetInt ("Total Points") + gameControllerScript.currentPoints);

			// Save the stats
			savedStats = true;
		}
	}

	public void Warp ()
	{
		warpTimeRemaining -= Time.unscaledDeltaTime;
		Camera.main.GetComponent<MotionBlur> ().enabled = true;
		//Camera.main.GetComponent<MotionBlur> ().blurAmount += Time.deltaTime;

		if (instantiatedWarpStars == false) 
		{
			WarpStars.SetActive (true);
			//PowerupCam.enabled = true;
			PlayerCollider.enabled = false;
			PlayWarpSound = true;
			//Panel.SetActive (false);
			instantiatedWarpStars = true;
		}
		/*
		if (PowerupCam.fieldOfView > 20) 
		{
			PowerupCam.fieldOfView -= Time.deltaTime * 10;
		}
		*/
		if (WarpStarsSound.isPlaying == false) 
		{
			WarpStarsSound.Play ();
			BackgroundMusic.Pause ();
		}
	}

	public void StopWarp ()
	{
		//PowerupCam.enabled = false;
		PlayerCollider.enabled = true;
		warpTimeRemaining = warpDuration;
		isWarping = false;
		WarpStarsSound.Stop ();
		BackgroundMusic.UnPause ();
		//Panel.SetActive (true);
		//Panel.GetComponent<Animator> ().Play ("Warp");
		//Camera.main.GetComponent<MotionBlur> ().blurAmount = 0.0f;
		instantiatedWarpStars = false;
		WarpStars.SetActive (false);
		//Camera.main.GetComponent<MotionBlur> ().enabled = false;
	}
		
	IEnumerator MovementSmoothEnabler ()
	{
		mousePos = StartingPos;
		StartingPos = new Vector2 (0.5f * Screen.width, 0.5f * Screen.height);
		yield return new WaitForSeconds (delay);
		MovementSmoothScript.enabled = true;
		StopCoroutine (MovementSmoothEnabler ());
	}
}
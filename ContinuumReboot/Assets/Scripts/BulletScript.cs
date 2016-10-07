using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;
using XInputDotNetPure;

public class BulletScript : MonoBehaviour 
{
	public bulletType BulletType;
	private AutoMoveAndRotate MoveAndRotateScript; 			// Auto Move and Rotate component.
	public PlayerController playerControllerScript;
	public float newSpeed = -70.0f; 						// New speed when ricoshet.
	private CameraShake camShakeScript; 					// The camera shake component.
	public float InitialShakeDuration = 0.25f;				// Shake time.
	public float InitialShakeStrength = 0.5f; 				// Shake strength.
	private GameController gameControllerScript; 			// Game Controller component.
	public TimescaleController timeScaleControllerScript; 	// Time scale controller component.
	//public ParticleSystem[] RicoshetParticle;   			// Particle combos, should be children of the Player GameObject.
	public AudioSource[] ComboAudio;
	public float VibrationTime = 0.04f;  					// How long should the vibrationh occur
	public bool useMutedBullet;								// Is the bullet (no cost) without sound?
	public int ricoshetNumber;
	public int ricoshetMax;
	public int ComboNN;
	public enum bulletType 
	{
		regularShot,
		mutedShot,
		altFire,
		rippleShot,
		helix,
		horizontalBeam,
		verticalBeam
	}

	void Start () 
	{
		MoveAndRotateScript = GetComponent<AutoMoveAndRotate> ();
		playerControllerScript = GameObject.Find ("Player").GetComponent<PlayerController>();
		ComboNN = playerControllerScript.ComboN;

		if (BulletType == bulletType.mutedShot) 
		{
			GetComponent<BoxCollider> ().enabled = true;
		}

		if (GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ().enabled == true) 
		{
			camShakeScript = Camera.main.GetComponent<CameraShake> ();
			camShakeScript.shakeDuration = InitialShakeDuration / 3;
			camShakeScript.shakeAmount = InitialShakeStrength / 3;
		}

		if (BulletType == bulletType.regularShot) 
		{
			//GetComponent<AudioSource> ().volume = 0.5f;
		}
			
		VibrationTime = 0.04f;
		gameControllerScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();
		timeScaleControllerScript = GameObject.FindGameObjectWithTag ("TimeScaleController").GetComponent<TimescaleController>();

		ComboAudio[0] = GameObject.FindGameObjectWithTag ("ComboAudioZero").GetComponent<AudioSource>();
		ComboAudio[1] = GameObject.FindGameObjectWithTag ("ComboAudioOne").GetComponent<AudioSource>();
		ComboAudio[2] = GameObject.FindGameObjectWithTag ("ComboAudioTwo").GetComponent<AudioSource>();
		ComboAudio[3] = GameObject.FindGameObjectWithTag ("ComboAudioThree").GetComponent<AudioSource>();
		ComboAudio[4] = GameObject.FindGameObjectWithTag ("ComboAudioFour").GetComponent<AudioSource>();
		ComboAudio[5] = GameObject.FindGameObjectWithTag ("ComboAudioFive").GetComponent<AudioSource>();
		ComboAudio[6] = GameObject.FindGameObjectWithTag ("ComboAudioSix").GetComponent<AudioSource>();
		ComboAudio[7] = GameObject.FindGameObjectWithTag ("ComboAudioSeven").GetComponent<AudioSource>();
		ComboAudio[8] = GameObject.FindGameObjectWithTag ("ComboAudioEight").GetComponent<AudioSource>();
		ComboAudio[9] = GameObject.FindGameObjectWithTag ("ComboAudioNine").GetComponent<AudioSource>();

		if (playerControllerScript.ComboTime > 0.1f) 
		{
			playerControllerScript.ComboTime -= 0.1f;
		}
	}

	void Update ()
	{
		if (BulletType == bulletType.regularShot) 
		{
			MoveAndRotateScript.moveUnitsPerSecond.value = new Vector3 (0, -30 * Time.timeScale, 0);
		}

		if (VibrationTime > 0) 
		{
			GamePad.SetVibration (PlayerIndex.One, 0, 0.25f); 
			VibrationTime -= Time.fixedDeltaTime;
		}

		if (VibrationTime <= 0) 
		{
			GamePad.SetVibration (PlayerIndex.One, 0, 0);
			VibrationTime = 0;
		}

		if (ricoshetNumber > ricoshetMax) 
		{
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Barrier" && BulletType != bulletType.horizontalBeam)
		{
			gameObject.transform.rotation = Quaternion.Euler (0, 0, Random.Range (135, 225));
		}

		if (other.tag == "Barrier" && BulletType == bulletType.horizontalBeam) 
		{
			gameObject.transform.rotation = Quaternion.identity;
		}

		if (other.tag == "Brick" || other.tag == "Cube")
		{
			playerControllerScript.ComboTime += 0.25f;
			//ComboAudio [ComboNN].Play ();
			ComboAudio [playerControllerScript.ComboN - 1].Play ();
			camShakeScript.shakeDuration = InitialShakeDuration;
			camShakeScript.shakeAmount = InitialShakeStrength;

			if (BulletType == bulletType.regularShot) 
			{
				if (ricoshetNumber < ricoshetMax) 
				{
				}

				if (ricoshetNumber >= ricoshetMax) 
				{
					Destroy (gameObject);
				}

				//ComboAudio [ComboNN - 1].Play ();

				ricoshetNumber += 1;
				transform.rotation = Quaternion.Euler (0, 0, Random.Range (135, 225));
			}

			if (BulletType == bulletType.mutedShot) 
			{
				transform.rotation = Quaternion.Euler (0, 0, Random.Range (135, 225));
			}

			if (BulletType == bulletType.verticalBeam) 
			{
				//ComboAudio [playerControllerScript.ComboN].Play ();
				Destroy (other.gameObject);
			}

			if (BulletType == bulletType.horizontalBeam) 
			{
				//ComboAudio [playerControllerScript.ComboN].Play ();
				Destroy (other.gameObject);
				gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
			}

			if (BulletType == bulletType.helix) 
			{
				//ComboAudio [playerControllerScript.ComboN].Play ();
				Destroy (other.gameObject);
			}

			if (BulletType == bulletType.rippleShot) 
			{
				ComboAudio [playerControllerScript.ComboN].Play ();
				Destroy (other.gameObject);
			}

			if (BulletType == bulletType.altFire) 
			{
				//ComboAudio [playerControllerScript.ComboN].Play ();
				Destroy (other.gameObject);
			}
		}
	}
}
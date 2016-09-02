using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;
using XInputDotNetPure;

public class BulletScript : MonoBehaviour 
{
	public bulletType BulletType;
	private AutoMoveAndRotate MoveAndRotateScript; 			// Auto Move and Rotate component.
	public float newSpeed = -70.0f; 						// New speed when ricoshet.
	private CameraShake camShakeScript; 					// The camera shake component.
	public float InitialShakeDuration = 0.25f;				// Shake time.
	public float InitialShakeStrength = 0.5f; 				// Shake strength.
	private GameController gameControllerScript; 			// Game Controller component.
	public TimescaleController timeScaleControllerScript; 	// Time scale controller component.
	public ParticleSystem[] RicoshetParticle;   // Particle combos, should be children of the Player GameObject.
	public AudioSource[] ComboAudio;
	public float VibrationTime = 0.04f;  		// How long should the vibrationh occur
	public bool useMutedBullet;					// Is the bullet (no cost) without sound?
	public int ricoshetNumber;
	public int ricoshetMax;

	public enum bulletType 
	{
		regularShot,
		mutedShot,
		ricoshetShot,
		altFire,
		rippleShot,
		helix,
		horizontalBeam,
		verticalBeam
	}

	void Start () 
	{
		if (GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ().enabled == true) 
		{
			camShakeScript = Camera.main.GetComponent<CameraShake> ();
			camShakeScript.shakeDuration = InitialShakeDuration;
			camShakeScript.shakeAmount = InitialShakeStrength;
		}

		if (BulletType == bulletType.regularShot) 
		{
			GetComponent<AudioSource> ().volume = 0.5f;
		}

		if (BulletType == bulletType.ricoshetShot) 
		{
			GetComponent<AudioSource> ().volume = 0;
		}

	
		VibrationTime = 0.04f;
		MoveAndRotateScript = GetComponent<AutoMoveAndRotate> ();
		gameControllerScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();
		timeScaleControllerScript = GameObject.FindGameObjectWithTag ("TimeScaleController").GetComponent<TimescaleController>();
	
		RicoshetParticle[0] = GameObject.FindGameObjectWithTag ("ComboOrangeParticles").GetComponent<ParticleSystem>();
		RicoshetParticle[1] = GameObject.FindGameObjectWithTag ("ComboYellowParticles").GetComponent<ParticleSystem>();
		RicoshetParticle[2] = GameObject.FindGameObjectWithTag ("ComboGreenParticles").GetComponent<ParticleSystem>();
		RicoshetParticle[3] = GameObject.FindGameObjectWithTag ("ComboCyanParticles").GetComponent<ParticleSystem>();
		RicoshetParticle[4] = GameObject.FindGameObjectWithTag ("ComboPurpleParticles").GetComponent<ParticleSystem>();

		ComboAudio[0] = GameObject.FindGameObjectWithTag ("ComboAudioZero").GetComponent<AudioSource>();
		ComboAudio[1] = GameObject.FindGameObjectWithTag ("ComboAudioOne").GetComponent<AudioSource>();
		ComboAudio[2] = GameObject.FindGameObjectWithTag ("ComboAudioTwo").GetComponent<AudioSource>();
		ComboAudio[3] = GameObject.FindGameObjectWithTag ("ComboAudioThree").GetComponent<AudioSource>();
		ComboAudio[4] = GameObject.FindGameObjectWithTag ("ComboAudioFour").GetComponent<AudioSource>();
		ComboAudio[5] = GameObject.FindGameObjectWithTag ("ComboAudioFive").GetComponent<AudioSource>();
		ComboAudio[6] = GameObject.FindGameObjectWithTag ("ComboAudioSix").GetComponent<AudioSource>();
		ComboAudio[7] = GameObject.FindGameObjectWithTag ("ComboAudioSeven").GetComponent<AudioSource>();

	}

	void Update ()
	{
		if (VibrationTime > 0) 
		{
			GamePad.SetVibration (PlayerIndex.One, 0, 0.25f); // Sets vibration amount ot 25%.
			VibrationTime -= Time.fixedDeltaTime; // Continuously decreases vibration time left.
		}

		if (VibrationTime <= 0) 
		{
			GamePad.SetVibration (PlayerIndex.One, 0, 0); // Sets controller to not vibrate.
			VibrationTime = 0; // Make it equal to 0.
		}

		if (ricoshetNumber > ricoshetMax) 
		{
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Brick" || other.tag == "Cube")
		{
			camShakeScript.shakeDuration = InitialShakeDuration;
			camShakeScript.shakeAmount = InitialShakeStrength;

			if (BulletType == bulletType.regularShot) 
			{
				if (ricoshetNumber <= ricoshetMax) 
				{
					RicoshetParticle [Mathf.Clamp(ricoshetNumber, 0, 5)].Play ();
				}

				if (ricoshetNumber > ricoshetMax) 
				{
					Destroy (gameObject);
				}

				BulletType = bulletType.ricoshetShot;
				transform.rotation = Quaternion.Euler (0, 0, Random.Range (-360, 360));
			}

			if (BulletType == bulletType.mutedShot) 
			{
				BulletType = bulletType.ricoshetShot;
				transform.rotation = Quaternion.Euler (0, 0, Random.Range (-360, 360));
			}

			if (BulletType == bulletType.ricoshetShot) 
			{
				if (ricoshetNumber <= ricoshetMax) 
				{
					RicoshetParticle [Mathf.Clamp(ricoshetNumber, 0, 5)].Play ();
					ComboAudio [ricoshetNumber].Play ();
					ricoshetNumber += 1;
					Instantiate (gameObject, gameObject.transform.position, Quaternion.Euler (0, 0, Random.Range (-360, 360)));
				}

				if (ricoshetNumber > ricoshetMax) 
				{
					Destroy (gameObject);
				}
			}

			if (BulletType == bulletType.verticalBeam) 
			{
				ComboAudio [4].Play ();
				Destroy (other.gameObject);
			}

			if (BulletType == bulletType.horizontalBeam) 
			{
				ComboAudio [3].Play ();
				Destroy (other.gameObject);
			}

			if (BulletType == bulletType.helix) 
			{
				ComboAudio [5].Play ();
				Destroy (other.gameObject);
			}

			if (BulletType == bulletType.rippleShot) 
			{
				ComboAudio [6].Play ();
				Destroy (other.gameObject);
			}

			if (BulletType == bulletType.altFire) 
			{
				ComboAudio [2].Play ();
				Destroy (other.gameObject);
			}

		}
	}
}
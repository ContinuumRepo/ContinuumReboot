using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;
using XInputDotNetPure;

public class BulletScript : MonoBehaviour 
{
	public bulletType BulletType;							// What type of bullet is this?

	public enum bulletType 
	{
		regularShot,
		mutedShot,
		altFire,
		rippleShot,
		helix,
		horizontalBeam,
		verticalBeam,
		shield
	}
		
	[Header ("Camera Shake")]
	public float InitialShakeDuration = 0.25f;				// Shake time.
	public float InitialShakeStrength = 0.5f; 				// Shake strength.

	[Header ("Combos")]
	public int ComboNN;										// Current combo.
	public AudioSource[] ComboAudio;						// Array of audio to play depnding on the combo.
	public int ricoshetNumber;								// The current ricoshet.
	public int ricoshetMax;									// How many ricochets until destroy.
	public float newSpeed = -70.0f; 						// New speed when ricoshet.

	[Header ("Misc")]
	public float VibrationTime = 0.04f;  					// How long should the vibrationh occur
	public bool useMutedBullet;								// Is the bullet (no cost) without sound?

	private PlayerController playerControllerScript;		// Player Controller script component.
	private AutoMoveAndRotate MoveAndRotateScript; 			// Auto Move and Rotate component.
	private CameraShake camShakeScript; 					// The camera shake component.

	void Start () 
	{
		// Finds scripts.
		MoveAndRotateScript = GetComponent<AutoMoveAndRotate> ();
		playerControllerScript = GameObject.Find ("Player").GetComponent<PlayerController>();
		camShakeScript = Camera.main.GetComponent<CameraShake> ();

		camShakeScript.shakeDuration = InitialShakeDuration / 3;
		camShakeScript.shakeAmount = InitialShakeStrength / 3;
		ComboNN = playerControllerScript.ComboN;
		VibrationTime = 0.04f;

		if (BulletType == bulletType.mutedShot) 
		{
			GetComponent<BoxCollider> ().enabled = true;
		}
			
		if (playerControllerScript.ComboTime > 0.1f) 
		{
			playerControllerScript.ComboTime -= 0.1f;
		}

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

		if (ComboNN > 10) 
		{
			ComboNN = 10;
		}

		if (playerControllerScript.ComboN > 10) 
		{
			playerControllerScript.ComboN = 10;
		}

		if (playerControllerScript.ComboTime > 10) 
		{
			playerControllerScript.ComboTime = 10;
		}
	}

	void OnTriggerEnter (Collider other)
	{
		// When bullet is a horizontal beam.
		if (other.tag == "Barrier" && BulletType == bulletType.horizontalBeam) 
		{
			gameObject.transform.rotation = Quaternion.identity;
		}

		// When bullet is not a horizontal beam.
		if (other.tag == "Barrier" && BulletType != bulletType.horizontalBeam)
		{
			gameObject.transform.rotation = Quaternion.Euler (0, 0, Random.Range (135, 225));
		}

		// When bullet hits a brick.
		if (other.tag == "BossPart" || other.tag == "Cube")
		{
			if (playerControllerScript != null)
			{
				playerControllerScript.ComboTime += 0.4f;
			}

			if (playerControllerScript == null) 
			{
				//Debug.Log ("Cannot find player controller script: Could be that this is a muted bullet spawning right in front of a cube.");
			}

			if (playerControllerScript.ComboN < 10 || ComboNN < 10 || playerControllerScript.ComboTime < 10) 
			{
				ComboAudio [Mathf.Clamp(playerControllerScript.ComboN, 0, 9)].Play ();
			}

			if (playerControllerScript.ComboN > 10 || ComboNN > 10 || playerControllerScript.ComboTime > 10) 
			{
				ComboAudio [9].Play ();
			}

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

				ricoshetNumber += 1;
				transform.rotation = Quaternion.Euler (0, 0, Random.Range (135, 225));
			}

			if (BulletType == bulletType.shield) 
			{
				//Destroy (other.gameObject);
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

		if (ComboNN > 10) 
		{
			ComboNN = 10;
		}
	}
}
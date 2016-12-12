using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class ShieldScript : MonoBehaviour 
{
	[Header ("Camera shake settings")]
	public float InitialShakeDuration = 0.25f;
	public float InitialShakeStrength = 0.5f;

	[Header ("Audio clips")]
	public int PlayElement;
	public AudioSource[] Oneshots;
	public AudioSource Explosion;

	[Header ("Misc")]
	public GameObject BulletNoCost;
	public ParticleSystem[] RicoshetParticle;
	public float VibrationTime = 0.04f;

	private CameraShake camShakeScript;
	private PlayerController playerControllerScript;

	void Start () 
	{
		FindComponents ();
		SetCamShakeSettings ();

		VibrationTime = 0.04f;
		PlayElement = 0;
	}

	void Update ()
	{
		Vibrate ();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Brick" || other.tag == "Cube") 
		{
			camShakeScript.shakeDuration = InitialShakeDuration;
			camShakeScript.shakeAmount = InitialShakeStrength;
			PlayElement = 0;
			Explosion.Play ();
			Oneshots [0].Play ();
		}
	}

	void FindComponents ()
	{
		camShakeScript = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraShake> ();

		// Finds Combo Particle System game objects in Scene (Should be attached as a child of the "Player" GameObject).
		RicoshetParticle[0] = GameObject.FindGameObjectWithTag ("ComboOrangeParticles").GetComponent<ParticleSystem>();
		RicoshetParticle[1] = GameObject.FindGameObjectWithTag ("ComboYellowParticles").GetComponent<ParticleSystem>();
		RicoshetParticle[2] = GameObject.FindGameObjectWithTag ("ComboGreenParticles").GetComponent<ParticleSystem>();
		RicoshetParticle[3] = GameObject.FindGameObjectWithTag ("ComboCyanParticles").GetComponent<ParticleSystem>();
		RicoshetParticle[4] = GameObject.FindGameObjectWithTag ("ComboPurpleParticles").GetComponent<ParticleSystem>();
	}

	void SetCamShakeSettings ()
	{
		camShakeScript.shakeDuration = InitialShakeDuration;
		camShakeScript.shakeAmount = InitialShakeStrength;
	}

	void Vibrate ()
	{
		VibrationTime -= Time.fixedDeltaTime;

		if (VibrationTime > 0) 
		{
			GamePad.SetVibration (PlayerIndex.One, 0, 0.25f);
		}

		if (VibrationTime <= 0) 
		{
			GamePad.SetVibration (PlayerIndex.One, 0, 0);
			VibrationTime = 0;
		}
	}
}

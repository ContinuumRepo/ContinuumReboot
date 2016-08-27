using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class ShieldScript : MonoBehaviour 
{
	private CameraShake camShakeScript;
	public float InitialShakeDuration = 0.25f;
	public float InitialShakeStrength = 0.5f;
	private GameController gameControllerScript;
	public TimescaleController timeScaleControllerScript;
	public float DecrementPortion = 0.1f;
	public float DecrementAmount = 100.0f;
	public int PlayElement;
	public AudioSource[] Oneshots;
	public GameObject BulletNoCost;

	public AudioSource Explosion;
	public ParticleSystem[] RicoshetParticle;
	public float VibrationTime = 0.04f;

	void Start () 
	{
		camShakeScript = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraShake> ();
		camShakeScript.shakeDuration = InitialShakeDuration;
		camShakeScript.shakeAmount = InitialShakeStrength;
		VibrationTime = 0.04f;
		PlayElement = 0;
		gameControllerScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();
		timeScaleControllerScript = GameObject.FindGameObjectWithTag ("TimeScaleController").GetComponent<TimescaleController>();

		// Finds Combo Particle System game objects in Scene (Should be attached as a child of the "Player" GameObject).
		RicoshetParticle[0] = GameObject.FindGameObjectWithTag ("ComboOrangeParticles").GetComponent<ParticleSystem>();
		RicoshetParticle[1] = GameObject.FindGameObjectWithTag ("ComboYellowParticles").GetComponent<ParticleSystem>();
		RicoshetParticle[2] = GameObject.FindGameObjectWithTag ("ComboGreenParticles").GetComponent<ParticleSystem>();
		RicoshetParticle[3] = GameObject.FindGameObjectWithTag ("ComboCyanParticles").GetComponent<ParticleSystem>();
		RicoshetParticle[4] = GameObject.FindGameObjectWithTag ("ComboPurpleParticles").GetComponent<ParticleSystem>();
	}

	void Update ()
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

		if (Oneshots [0].isPlaying == false) 
		{
			Destroy (Oneshots[0]);
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Brick" || other.tag == "Cube") 
		{
			camShakeScript.shakeDuration = InitialShakeDuration;
			camShakeScript.shakeAmount = InitialShakeStrength;
			PlayElement = 0;
			//Instantiate (Oneshots [PlayElement], Vector3.zero, Quaternion.identity);
			Explosion.Play ();
			Oneshots [0].Play ();
		}

		if (other.tag == "Brick" || other.tag == "Cube") 
		{

		}
	}
}

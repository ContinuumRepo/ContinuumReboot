using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;
using XInputDotNetPure;

public class BulletScript : MonoBehaviour 
{
	private AutoMoveAndRotate MoveAndRotateScript;
	public float newSpeed = -70.0f;
	private GameController gameControllerScript;
	public enum bulletcost {FixedRate, Percentage}
	public bulletcost BulletCostType;
	public float DecrementPortion = 0.1f;
	public float DecrementAmount = 100.0f;
	public int PlayElement;
	public AudioSource[] Oneshots;
	public int ricoshet; 
	public int ricoshetMax = 6;
	public GameObject BulletNoCost;
	public bool useRandomRotation = true;
	public AudioSource BeamExplosion;
	public ParticleSystem[] RicoshetParticle;
	public float VibrationTime = 0.04f;

	void Start () 
	{
		VibrationTime = 0.04f;
		PlayElement = 0;
		ricoshet = 0;
		MoveAndRotateScript = GetComponent<AutoMoveAndRotate> ();
		gameControllerScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();

		// Finds Combo Particle System game objects in Scene (Should be attached as a child of the "Player" GameObject).
		RicoshetParticle[0] = GameObject.FindGameObjectWithTag ("ComboOrangeParticles").GetComponent<ParticleSystem>();
		RicoshetParticle[1] = GameObject.FindGameObjectWithTag ("ComboYellowParticles").GetComponent<ParticleSystem>();
		RicoshetParticle[2] = GameObject.FindGameObjectWithTag ("ComboGreenParticles").GetComponent<ParticleSystem>();
		RicoshetParticle[3] = GameObject.FindGameObjectWithTag ("ComboCyanParticles").GetComponent<ParticleSystem>();
		RicoshetParticle[4] = GameObject.FindGameObjectWithTag ("ComboPurpleParticles").GetComponent<ParticleSystem>();

		if (BulletCostType == bulletcost.Percentage)
		{
			gameControllerScript.CurrentScore = gameControllerScript.CurrentScore - (DecrementPortion * gameControllerScript.CurrentScore);
		}

		if (BulletCostType == bulletcost.FixedRate) 
		{
			gameControllerScript.CurrentScore -= DecrementAmount;
		}
	}

	void Update ()
	{
		//PlayElement = Mathf.Clamp (PlayElement, 0, Oneshots.Length);
		VibrationTime -= Time.fixedDeltaTime;

		if (VibrationTime > 0) 
		{
			GamePad.SetVibration (PlayerIndex.One, 0, 0.4f);
		}

		if (VibrationTime <= 0) 
		{
			GamePad.SetVibration (PlayerIndex.One, 0, 0);
			VibrationTime = 0;
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Brick" || other.tag == "Cube") 
		{
			if (useRandomRotation == true) 
			{
				if (ricoshet < ricoshetMax) 
				{
					gameObject.transform.rotation = Quaternion.Euler (0.0f, 0.0f, Random.Range (-360, 360));
					MoveAndRotateScript.moveUnitsPerSecond.value = new Vector3 (0.0f, newSpeed, 0.0f);
					Instantiate (Oneshots [PlayElement], Vector3.zero, Quaternion.identity);
					PlayElement += 1;
					ricoshet += 1;
					Instantiate (BulletNoCost, gameObject.transform.position, Quaternion.Euler (0, 0, Random.Range (-360, 360)));
					RicoshetParticle [Mathf.Clamp(PlayElement - 1, 0, 5)].Play ();
					VibrationTime = 0.04f;
				}

				if (ricoshet >= ricoshetMax) 
				{
					gameObject.transform.rotation = Quaternion.Euler (0.0f, 0.0f, Random.Range (-360, 360));
					MoveAndRotateScript.moveUnitsPerSecond.value = new Vector3 (0.0f, newSpeed, 0.0f);
					Instantiate (Oneshots [5], Vector3.zero, Quaternion.identity);
					RicoshetParticle [5].Play ();
					PlayElement = 1;
					ricoshet = 1;
				}
		
				if (PlayElement > 8) 
				{
					PlayElement = 1;
				}
			}

			if (useRandomRotation == false) 
			{
				PlayElement = 0;
				Instantiate (Oneshots [PlayElement], Vector3.zero, Quaternion.identity);
				BeamExplosion.Play ();
			}
		}

		if (other.tag == "Brick" || other.tag == "Cube" && ricoshet >= ricoshetMax) 
		{
			
		}

		if (other.tag == "Barrier") 
		{
			gameObject.transform.rotation = Quaternion.Euler (0.0f, 0.0f, Random.Range (-360, 360));
		}
	}
}
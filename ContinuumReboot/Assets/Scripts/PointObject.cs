using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;

public class PointObject : MonoBehaviour 
{
	private PlayerController PlayerControllerScript; 		// The Player Controller script.
	private GameController gameControllerScript; 			// The GameController script.

	public MeshRenderer playerMesh;

	public bool randomiseType;
	public bool changeTypeOverTime;
	public float changeTime = 1.0f;
	public enum type {Orange, Yellow, Green, Cyan, Purple, Red}; // Cube types.
	public type PointType; 			    						 // To show above enum in inspector.

	public Material OrangeMaterial;
	public Material YellowMaterial;
	public Material GreenMaterial;
	public Material CyanMaterial;
	public Material PurpleMaterial;
	public Material RedMaterial;

	[Header ("Rewarding")]
	public float CurrentPointReward;
	public float OrangePointReward = 150;						    // Rewards the player this many points when a bullet hits it.
	public float YellowPointReward = 150;
	public float GreenPointReward = 150;
	public float CyanPointReward = 150;
	public float PurplePointReward = 150;
	public float RedPointReward = 150;

	[Header ("Explosions")]
	public GameObject OrangeExplosion;
	public GameObject YellowExplosion;
	public GameObject GreenExplosion;
	public GameObject CyanExplosion;
	public GameObject PurpleExplosion;
	public GameObject RedExplosion;

	public ParticleSystem MainEngineParticles; 				// Engine Particle system.
	public GameObject PlayerExplosion; 						// The explosion when the player hits it.
	public float Damage = 25.0f; 							// Damage amount to player.
	public Animator ScoreText;

	public TimescaleController timeController;
	//public Text ComboText;
	public bool isBossPart;
	public bool isTutorialPart;

	void Start ()
	{
		if (randomiseType == true) 
		{
			PointType = (type)Random.Range (0, 5);
		}

		if (changeTypeOverTime == true) 
		{
			InvokeRepeating ("ChangeBrickType", 0, changeTime);
		}

		//ComboText = GameObject.Find 							("ComboText").			GetComponentInChildren  <Text> ();
		ScoreText = GameObject.FindGameObjectWithTag 			("ScoreText").			GetComponent 			<Animator> ();
		gameControllerScript = GameObject.FindGameObjectWithTag ("GameController").		GetComponent 			<GameController> ();
		timeController = GameObject.FindGameObjectWithTag 		("TimeScaleController").GetComponent 			<TimescaleController> ();
		PlayerControllerScript = GameObject.Find 				("Player").				GetComponent 			<PlayerController>();

		if (PlayerControllerScript.Health >= 25 && isTutorialPart == false) 
		{
			playerMesh = GameObject.FindGameObjectWithTag ("PlayerMesh").GetComponent<MeshRenderer> ();
		}

		if (PlayerControllerScript.Health < 25 && isTutorialPart == false) 
		{
			Destroy (gameObject);
		}

		// Finding the main engine particle system GameObject.
		GameObject MainEngine = GameObject.FindGameObjectWithTag ("MainEngine");
		MainEngineParticles = MainEngine.GetComponent<ParticleSystem> ();
	}

	void Update ()
	{
		if (PointType == type.Orange) 
		{
			GetComponent<MeshRenderer> ().material = OrangeMaterial;
			CurrentPointReward = OrangePointReward;
		}

		if (PointType == type.Yellow) 
		{
			GetComponent<MeshRenderer> ().material = YellowMaterial;
			CurrentPointReward = YellowPointReward;
		}

		if (PointType == type.Green) 
		{
			GetComponent<MeshRenderer> ().material = GreenMaterial;
			CurrentPointReward = GreenPointReward;
		}

		if (PointType == type.Cyan) 
		{
			GetComponent<MeshRenderer> ().material = CyanMaterial;
			CurrentPointReward = CyanPointReward;
		}

		if (PointType == type.Purple) 
		{
			GetComponent<MeshRenderer> ().material = PurpleMaterial;
			CurrentPointReward = PurplePointReward;
		}

		if (PointType == type.Red) 
		{
			GetComponent<MeshRenderer> ().material = RedMaterial;
			CurrentPointReward = RedPointReward;
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Bullet") 
		{
			ScoreText.Play ("Scoretext");

			// Orange brick.
			if (PointType == type.Orange) 
			{
				if (isTutorialPart == false) 
				{
					gameControllerScript.CurrentScore += OrangePointReward * Time.timeScale * PlayerControllerScript.ComboN;
				}

				Instantiate (OrangeExplosion, transform.position, transform.rotation);
				OrangeExplosion.GetComponentInChildren <Text> ().text = "x" + PlayerControllerScript.ComboN + "";
				OrangeExplosion.GetComponentInChildren <Animator> ().Play (0);
				OrangeExplosion.GetComponentInChildren <Text> ().color = new Color (1, 0.5f, 0);
			}

			// Yellow brick.
			if (PointType == type.Yellow) 
			{
				if (isTutorialPart == false) 
				{
					gameControllerScript.CurrentScore += YellowPointReward * 2 * Time.timeScale * PlayerControllerScript.ComboN;
				}

				Instantiate (YellowExplosion, transform.position, transform.rotation);
				YellowExplosion.GetComponentInChildren <Text> ().text = "x" + PlayerControllerScript.ComboN + "";
				YellowExplosion.GetComponentInChildren <Animator> ().Play (0);
				YellowExplosion.GetComponentInChildren <Text> ().color = new Color (1, 1, 0);
			}

			// Green brick.
			if (PointType == type.Green) 
			{
				if (isTutorialPart == false) 
				{
					gameControllerScript.CurrentScore += GreenPointReward * 3 * Time.timeScale * PlayerControllerScript.ComboN;
				}

				Instantiate (GreenExplosion, transform.position, transform.rotation);
				GreenExplosion.GetComponentInChildren <Text> ().text = "x" + PlayerControllerScript.ComboN + "";
				GreenExplosion.GetComponentInChildren <Animator> ().Play (0);
				GreenExplosion.GetComponentInChildren <Text> ().color = new Color (0, 1, 0);
			}

			// Cyan brick.
			if (PointType == type.Cyan) 
			{
				if (isTutorialPart == false)
				{
					gameControllerScript.CurrentScore += CyanPointReward * 4 * Time.timeScale * PlayerControllerScript.ComboN;
				}

				Instantiate (CyanExplosion, transform.position, transform.rotation);
				CyanExplosion.GetComponentInChildren <Text> ().text = "x" + PlayerControllerScript.ComboN + "";
				CyanExplosion.GetComponentInChildren <Animator> ().Play (0);
				CyanExplosion.GetComponentInChildren <Text> ().color = new Color (0, 1, 1);
			}

			// Purple brick.
			if (PointType == type.Purple) 
			{
				if (isTutorialPart == false) 
				{
					gameControllerScript.CurrentScore += PurplePointReward * 5 * Time.timeScale * PlayerControllerScript.ComboN;
				}

				Instantiate (PurpleExplosion, transform.position, transform.rotation);
				PurpleExplosion.GetComponentInChildren <Text> ().text = "x" + PlayerControllerScript.ComboN + "";
				PurpleExplosion.GetComponentInChildren <Animator> ().Play (0);
				PurpleExplosion.GetComponentInChildren <Text> ().color = new Color (0.9f, 0.2f, 1);
			}

			// Red brick.
			if (PointType == type.Red) 
			{
				if (isTutorialPart == false) 
				{
					gameControllerScript.CurrentScore += RedPointReward * 5 * Time.timeScale * PlayerControllerScript.ComboN;
				}

				Instantiate (RedExplosion, transform.position, transform.rotation);
				RedExplosion.GetComponentInChildren <Text> ().text = "x" + PlayerControllerScript.ComboN + "";
				RedExplosion.GetComponentInChildren <Animator> ().Play (0);
				RedExplosion.GetComponentInChildren <Text> ().color = new Color (1, 0, 0);
			}

			Destroy (gameObject);
		}

		// When a brick hits the player.
		if (other.tag == "Player") 
		{		
			// If player health is greater than 25.
			if (gameObject.tag == "Cube") 
			{
				if (PlayerControllerScript.Health > 25) 
				{
					PlayerControllerScript.OverlayTime = 2;

					//Finds Points to destroy.
					GameObject[] Destroyers = GameObject.FindGameObjectsWithTag ("Cube");
					for (int i = Destroyers.Length - 1; i > 0; i--) 
					{
						Destroy (Destroyers [i].gameObject);
					}
				}
			}

			PlayerControllerScript.collisionCooldown = 3;	

			if (PlayerControllerScript.Health > 10) 
			{
				// Instantiates a larger explosion.
				Instantiate (PlayerExplosion, transform.position, transform.rotation);
			}

			// Gives damage to player.
			PlayerControllerScript.Health -= Damage;

			if (PlayerControllerScript.Health > 25)
			{
			}

			// If player health is less than 10.
			if (PlayerControllerScript.Health <= 10) 
			{
				// Instantiate huge explosion.
				Instantiate (PlayerControllerScript.gameOverExplosion, gameObject.transform.position, Quaternion.identity);
			}
		}
	}

	public void ChangeBrickType ()
	{
		PointType = (type)Random.Range (0, 5);
	}
}
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour 
{
	public int Health = 200; // The Enemy's HP.
	public GameObject Explosion; // The explosion to instantiate when a bullet hits the enemy.
	public GameObject EnemyExplosion; // To instantiate when the enemy is destroyed.
	public int PointReward = 250; // The points to add to the score in the game controller.
	private GameController gameControllerScript; // The script component.
	private float nextBomb;
	public float bombRate = 2.0f; // 1/bombRate = bombs/sec.
	public GameObject[] Bombs; // List of bombs to shoot.
	public int BombNumberMax; // How many bombs are there in the list including element 0.
	public int CurrentBomb; // The bomb number we are up to.
	public Transform EnemyTrans; // The transofrm which the enemy will follow.
	public enum enemyType {Normal, Boss}
	public enemyType EnemyType;
	public AudioSource Music;
	public AudioClip NormalMusic;
	public AudioClip BossMusic;
	public Image EnemyHealthBar;
	public bool dontuseEnemyTrans;

	void Start () 
	{
		//EnemyHealthBar = gameObject.GetComponentInChildren<Image> ();
		Music = GameObject.FindGameObjectWithTag ("BGM").GetComponent<AudioSource> ();
		gameControllerScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>(); // Finds the Game Controller.

		if (EnemyType == enemyType.Normal)
		{
			if (GameObject.Find ("Boss") == null) 
			{
				Music.clip = NormalMusic;
			}

			EnemyTrans = GameObject.Find ("EnemyRestPoint").GetComponent<Transform>(); // Finds the transform the enemy will follow.
		}

		if (EnemyType == enemyType.Boss) 
		{
			Music.clip = BossMusic;
			Music.Play ();
			EnemyTrans = GameObject.Find ("EnemyBossRestPoint").GetComponent<Transform>(); // Finds the transform the enemy will follow.
		}

		if (dontuseEnemyTrans == false)
		{
			GetComponent<SmoothFollowOrig> ().target = EnemyTrans.transform; // Finds the smooth following script.
		}

		BombNumberMax = Bombs.Length; // MAkes bomb number max equal to the length of the bombs array.
		gameObject.transform.rotation = Quaternion.Euler (-90, 0, 0); // Sets custom rotation of the gameObject.
	}

	void Update () 
	{
		
		if (EnemyType == enemyType.Normal) 
		{
			EnemyHealthBar.fillAmount = (float)Health / 200.0f;
			if (Health > 0 && Time.time > nextBomb) 
			{
				//EnemyHealthBar.fillAmount = ;
				nextBomb = Time.time + bombRate;
				Instantiate (Bombs [CurrentBomb], gameObject.transform.position, gameObject.transform.rotation);
				CurrentBomb += 1;

				if (CurrentBomb >= BombNumberMax) 
				{
					CurrentBomb = 0;
				}
			}

			if (Health <= 0) 
			{
				//Music.UnPause ();
				Instantiate (EnemyExplosion, gameObject.transform.position, Quaternion.Euler (60, 90, 0));
				gameControllerScript.WaveLabel.SetActive (true);
				gameControllerScript.WaveLabel.GetComponent<DestroyOrDeactivateByTime> ().enabled = true;
				gameControllerScript.WaveLabel.GetComponent<Animator> ().Play ("WaveLabel");
				gameControllerScript.wave += 1;
				gameControllerScript.hazardCount += 2;
				Destroy (gameObject);
				Debug.Log ("Destroyed an enemy");
			}
		}

		if (EnemyType == enemyType.Boss) 
		{
			Music.pitch = 1;
			EnemyHealthBar.fillAmount = (float)Health / 800.0f;
			if (Health > 0 && Time.time > nextBomb) 
			{
				nextBomb = Time.time + bombRate;
				Instantiate (Bombs [CurrentBomb], gameObject.transform.position, gameObject.transform.rotation);
				CurrentBomb += 1;

				if (CurrentBomb >= BombNumberMax) 
				{
					CurrentBomb = 0;
				}
			}

			if (Health <= 0) 
			{
				Music.clip = NormalMusic;
				Music.UnPause ();
				Music.Play ();
				Instantiate (EnemyExplosion, gameObject.transform.position, Quaternion.Euler (60, 90, 0));
				gameControllerScript.WaveLabel.SetActive (true);
				gameControllerScript.WaveLabel.GetComponent<DestroyOrDeactivateByTime> ().enabled = true;
				gameControllerScript.WaveLabel.GetComponent<Animator> ().Play ("WaveLabel");
				gameControllerScript.wave += 1;
				gameControllerScript.hazardCount += 2;
				Destroy (gameObject);
				Debug.Log ("Destroyed a boss");
			}
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Bullet") 
		{
			Health -= 25;
			Instantiate (Explosion, other.gameObject.transform.position, gameObject.transform.rotation);
			gameControllerScript.CurrentScore += PointReward;
		}
	}
}

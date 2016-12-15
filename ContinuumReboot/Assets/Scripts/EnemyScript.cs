using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour 
{
	public enemyType EnemyType;
	public enum enemyType {Normal, Boss}			// What type of boss are we?
	public int Health = 200; 						// The Enemy's HP.
	public int PointReward = 250; 					// The points to add to the score in the game controller.
	public GameObject Explosion; 					// The explosion to instantiate when a bullet hits the enemy.
	public GameObject EnemyExplosion; 				// To instantiate when the enemy is destroyed.

	public float bombRate = 2.0f; 					// 1/bombRate = bombs/sec.
	public GameObject[] Bombs; 						// List of bombs to shoot.
	public int BombNumberMax; 						// How many bombs are there in the list including element 0.
	public int CurrentBomb; 						// The bomb number we are up to.
	public Transform EnemyTrans; 					// The transofrm which the enemy will follow.

	public Image EnemyHealthBar;					// Enemy health bar image.
	public bool dontuseEnemyTrans;					// Use tranform?

	private float nextBomb;
	private GameController gameControllerScript; 	// The script component.

	void Start () 
	{
		FindComponents ();

		if (EnemyType == enemyType.Boss) 
		{
			EnemyTrans = GameObject.FindGameObjectWithTag ("EnemyTrans").transform; // Finds the transform the enemy will follow.
		}

		if (dontuseEnemyTrans == false)
		{
			EnemyTrans = GameObject.FindGameObjectWithTag ("Player").transform;
		}

		BombNumberMax = Bombs.Length; // Makes bomb number max equal to the length of the bombs array.
		gameObject.transform.rotation = Quaternion.Euler (-90, 0, 0); // Sets custom rotation of the gameObject.
	}

	void Update () 
	{
		if (EnemyType == enemyType.Normal) 
		{
			if (Health > 0 && Time.time > nextBomb) 
			{
				nextBomb = Time.time + bombRate;
				Instantiate (Bombs [CurrentBomb], gameObject.transform.position, Quaternion.identity);
				CurrentBomb += 1;

				if (CurrentBomb >= BombNumberMax) 
				{
					CurrentBomb = 0;
				}
			}

			if (Health <= 0) 
			{
				Instantiate (EnemyExplosion, gameObject.transform.position, Quaternion.Euler (60, 90, 30));
				Debug.Log ("Destroyed an enemy");
			}
		}

		if (EnemyType == enemyType.Boss) 
		{
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
				Instantiate (EnemyExplosion, gameObject.transform.position, Quaternion.Euler (60, 90, 0));
				gameControllerScript.WaveLabel.SetActive (true);
				gameControllerScript.WaveLabel.GetComponent<DestroyOrDeactivateByTime> ().enabled = true;
				gameControllerScript.WaveLabel.GetComponent<Animator> ().Play ("WaveLabel");
				gameControllerScript.wave += 1;
				gameControllerScript.hazardCount += 2;
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

	void FindComponents ()
	{
		// Finds the Game Controller.
		gameControllerScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>(); 
	}
}
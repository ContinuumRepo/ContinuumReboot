using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour 
{
	public GameObject explosion;
	public GameObject explosionPlayer;
	public int enemyKillValue = 300;
	private GameController GameControllerScript;
	public GameObject enemyTextParticles;
	public int health = 100;
	public int healthDecrease = 20;
	public int damage = 20;

	private BasicPlayerMovement playerScript;
	
	void Start () 
	{
		// Checks if there is a GameController script attached to GameObject with "GameController" as its tag
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		GameObject playerObject = GameObject.FindGameObjectWithTag ("Player");
		
		if (gameControllerObject != null)
		{
			GameControllerScript = gameControllerObject.GetComponent <GameController>();
		}
		
		if (GameControllerScript == null)
		{
		}
		
		if (playerObject == null || playerScript == null) 
		{
		}
		
		if (playerObject != null || playerScript != null) 
		{
			playerScript = playerObject.GetComponent<BasicPlayerMovement> ();
		}
	}
	
	void Update () 
	{		


		if (health < 0) 
		{
			GameControllerScript.enemyKills += 1;
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "PlayerGeom" || other.tag == "LevelMod") 
		{
			GameControllerScript.currentScore += enemyKillValue;
			Instantiate (enemyTextParticles, enemyTextParticles.transform.position, enemyTextParticles.transform.rotation);

			Debug.Log ("You got hit by an enemy.");

			// Makes nice explosion
			Instantiate (explosionPlayer, transform.position, explosionPlayer.transform.rotation);
			playerScript.currentHealth -= damage;
			Destroy (gameObject);
		}

		if (other.tag == "Bullet") 
		{
			Instantiate (explosion, transform.position, explosion.transform.rotation);
			health -= healthDecrease;
			Destroy(other.gameObject);
			GameControllerScript.currentScore += enemyKillValue;
		}
	}
}

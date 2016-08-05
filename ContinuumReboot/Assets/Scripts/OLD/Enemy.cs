using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public GameObject Explosion;
	public GameObject PlayerExplosion;
	public int pointValue = 1000;
	public int HitPoints = 4;
	public int damagePlayerHealth;

	private Player playerControllerScript;
	private Game gameControllerScript;

	void Start () 
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		gameControllerScript = gameControllerObject.GetComponent<Game> ();

		GameObject playerControllerObject = GameObject.FindGameObjectWithTag ("PlayerObject");
		playerControllerScript = playerControllerObject.GetComponent<Player> (); 

		//GameObject PointsTextAnimObject = GameObject.FindGameObjectWithTag("PointsText");
		//PointsTextAnim = PointsTextAnimObject.GetComponent<Animator> ();
	}

	void FixedUpdate () 
	{
		if (HitPoints <= 0) 
		{
			Destroy(gameObject);
			Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
			Debug.Log ("You destroyed an enemy");
		}
	}

	void OnTriggerEnter (Collider other)
	{
		// When player hits enemy
		if (other.tag == "Player") 
		{
			playerControllerScript.currentHealth -= damagePlayerHealth;
			Instantiate (PlayerExplosion, gameObject.transform.position, gameObject.transform.rotation);
			Debug.Log ("Enemy collided with you.");
			Destroy(gameObject);
		}

		// When bullet hits enemy
		if (other.tag == "Bullet") 
		{
			gameControllerScript.currentScore += pointValue;
			HitPoints -= 1;
			// Plays default Animation
			// PointsTextAnim.Play(0);

			Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
			Destroy(other.gameObject);
			Debug.Log ("You hit an enemy with a bullet.");
		}
	}
}

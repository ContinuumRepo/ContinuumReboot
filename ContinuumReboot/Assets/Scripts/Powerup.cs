using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour 
{
	public GameObject Explosion;
	public Player playerScript;
	public enum powerup {Shoot, Warp}
	public powerup PowerupType;

	void Start () 
	{
		GameObject playerObject = GameObject.FindGameObjectWithTag ("PlayerObject");
		playerScript = playerObject.GetComponent<Player> ();
	}
	

	void Update () 
	{
	
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") 
		{
			if (PowerupType == powerup.Shoot)
			{
				playerScript.canShoot = true;
				playerScript.remainingShootTime = playerScript.shootDuration;

				Debug.Log ("You collected a powerup, (shooting).");
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				
				Destroy (gameObject);
			}

			if (PowerupType == powerup.Warp)
			{
				//playerScript.isWarping = true;
				playerScript.warpTimeRemaining = playerScript.warpDuration;
				playerScript.Warp ();
				Debug.Log ("You collected a powerup, (warp).");
				Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
				
				Destroy (gameObject);
			}
		}
	}
}
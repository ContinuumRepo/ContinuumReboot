using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;

public class BombScript : MonoBehaviour 
{
	private AutoMoveAndRotate moveScript;
	public enum bombMode {left, right, slowLeft, slowRight}
	public bombMode BombMode;
	private PlayerController PlayerControllerScript;
	public int Damage = 25;
	public GameObject PlayerExplosion;
	public GameObject BombExplosion;

	void Start () 
	{
		PlayerControllerScript = GameObject.Find ("Player").GetComponent<PlayerController>();
		moveScript = GetComponent<AutoMoveAndRotate> ();

		if (BombMode == bombMode.left)
		{
			moveScript.moveUnitsPerSecond.value = new Vector3 (-1, 0, 0); 
		}

		if (BombMode == bombMode.right) 
		{
			moveScript.moveUnitsPerSecond.value = new Vector3 (1, 0, 0);
		}

		if (BombMode == bombMode.slowLeft) 
		{
			moveScript.moveUnitsPerSecond.value = new Vector3 (-2, 1, 0);
		}

		if (BombMode == bombMode.slowRight) 
		{
			moveScript.moveUnitsPerSecond.value = new Vector3 (2, 1, 0);
		}
	}
		
	void Update () 
	{
	
	}

	void OnTriggerEnter (Collider other)
	{
		// When a brick hits the player.
		if (other.tag == "Player") {	
			//playerScript.playerMesh.material = normalMaterial;
			// If player health is greater than 25.
			if (PlayerControllerScript.Health > 25) {
				// Finds Points to destroy.
				GameObject[] Destroyers = GameObject.FindGameObjectsWithTag ("Cube");
				for (int i = Destroyers.Length - 1; i > 0; i--) {
					Destroy (Destroyers [i].gameObject);
				}
			}

			if (PlayerControllerScript.Health > 10) {
				// Instantiates a larger explosion.
				Instantiate (PlayerExplosion, transform.position, transform.rotation);
			}

			// Gives damage to player.
			PlayerControllerScript.Health -= Damage;

			// Desaturates screen.
			PlayerControllerScript.ColorCorrectionCurvesScript.saturation = 0;

			// If player health is less than 10.
			if (PlayerControllerScript.Health <= 10) {
				// Instantiate huge explosion.
				Instantiate (PlayerControllerScript.gameOverExplosion, gameObject.transform.position, Quaternion.identity);
			}
		}
		Instantiate (BombExplosion, gameObject.transform.position, gameObject.transform.rotation);
		Destroy (gameObject);
	}
}

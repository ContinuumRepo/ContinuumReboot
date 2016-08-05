using UnityEngine;
using System.Collections;

public class TimeMod : MonoBehaviour 
{
	public GameObject Explosion;
	private Player playerControllerScript;
	private SmoothFollowOrig smoothFollowScript;
	public enum type {Positive, Negative};
	public type TimeModType;

	void Start () 
	{
		GameObject playerControllerObject = GameObject.FindGameObjectWithTag ("PlayerObject");
		playerControllerScript = playerControllerObject.GetComponent<Player> ();

		/*GameObject smoothFollowObject = GameObject.FindGameObjectWithTag ("MovementFollower");
		smoothFollowScript = smoothFollowObject.GetComponent<SmoothFollowOrig> ();*/
	}

	void Update () 
	{
	
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") 
		{
			if (TimeModType == type.Positive)
			{
				playerControllerScript.Division += 1;
				Instantiate (Explosion, transform.position, transform.rotation);
				Destroy(gameObject);
				Debug.Log("You got a positive time mod.");

			}

			if (TimeModType == type.Negative)
			{
				playerControllerScript.Division -= 1;
				Instantiate (Explosion, transform.position, transform.rotation);
				Destroy(gameObject);
				Debug.Log("You got a negative time mod.");
			}
		}
	}
}

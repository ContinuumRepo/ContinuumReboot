using UnityEngine;
using System.Collections;

public class LookAtPlayer : MonoBehaviour 
{
	public GameObject Player;
	public bool NewLook;
	
	void Start () 
	{
	
	}

	void Update () 
	{
		gameObject.transform.LookAt (Player.transform);

		if (NewLook == true) 
		{
			transform.rotation = Quaternion.LookRotation(gameObject.transform.position - Player.transform.position);
		}
	}
}

using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour 
{
	public GameObject Brain;
	public GameObject bossExplosion;
	private GameObject boss;
	private Transform bossHover;
	private SmoothFollowOrig smoothFollowScript;
	private GameController gameControllerScript;

	void Start ()
	{
		gameControllerScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();
		Brain = GameObject.Find ("Brain");
		boss = GameObject.FindGameObjectWithTag ("Boss");
		bossHover = GameObject.Find ("EnemyBossRestPoint").GetComponent<Transform>();

		smoothFollowScript = GetComponent<SmoothFollowOrig> ();
		smoothFollowScript.target = bossHover.transform;
	}

	void Update () 
	{
		if (Brain == null || Brain.GetComponent<EnemyScript>().Health <= 0) 
		{
			gameControllerScript.WaveLabel.SetActive (true);
			gameControllerScript.WaveLabel.GetComponent<DestroyOrDeactivateByTime> ().enabled = true;
			gameControllerScript.WaveLabel.GetComponent<Animator> ().Play ("WaveLabel");
			gameControllerScript.wave += 1;
			gameControllerScript.hazardCount += 2;
			Instantiate (bossExplosion, gameObject.transform.position, Quaternion.identity);
			Destroy (boss);
			Destroy (gameObject);
		}
	}
}

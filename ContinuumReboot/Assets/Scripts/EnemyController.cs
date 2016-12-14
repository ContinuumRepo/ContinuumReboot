using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class EnemyController : MonoBehaviour 
{
	public GameObject Brain;
	public GameObject bossExplosion;
	public string bossName;
	public Text NameText;
	//public GameObject Shafts;
	public GameObject BossTextObject;
	private GameObject boss;
	private Transform bossHover;
	private SmoothFollowOrig smoothFollowScript;
	private GameController gameControllerScript;


	void Start ()
	{
		BossTextObject = GameObject.FindGameObjectWithTag ("BossText");
		BossTextObject.GetComponent<AudioSource> ().Play ();
		FindComponents ();
		//OverrideShaftsCaster ();
		NameText.text = "" + bossName + "";
		BossTextObject.GetComponent<Animator> ().Play (0);
	}

	void Update () 
	{
		if (Brain == null || Brain.GetComponent<EnemyScript>().Health <= 0) 
		{
			KillBoss ();
			//ResetShaftsCaster ();
			Destroy (gameObject);
		}
	}

	void FindComponents ()
	{
		gameControllerScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();
		Brain = GameObject.Find ("Brain");

		boss = GameObject.FindGameObjectWithTag ("Boss");
		bossHover = GameObject.Find ("EnemyBossRestPoint").GetComponent<Transform> ();

		NameText = GameObject.Find ("BossTextText").GetComponent<Text> ();
		BossTextObject.GetComponentInChildren<Animator> ().Play (0);

		smoothFollowScript = GetComponent<SmoothFollowOrig> ();
		smoothFollowScript.target = bossHover.transform;
	}

	/*void OverrideShaftsCaster ()
	{
		Shafts = GameObject.FindGameObjectWithTag ("Shafts");
		Shafts.GetComponent<SmoothFollowOrig> ().target = gameObject.transform;
	}*/

	void KillBoss ()
	{
		gameControllerScript.WaveLabel.SetActive (true);
		gameControllerScript.WaveLabel.GetComponent<DestroyOrDeactivateByTime> ().enabled = true;
		gameControllerScript.WaveLabel.GetComponent<Animator> ().Play ("WaveLabel");
		gameControllerScript.wave += 1;
		gameControllerScript.hazardCount += 2;
		Instantiate (bossExplosion, gameObject.transform.position, Quaternion.identity);
		Destroy (boss);
	}

	/*void ResetShaftsCaster ()
	{
		if (Shafts.GetComponent<SmoothFollowOrig> ().target == null) 
		{
			Shafts.GetComponent<SmoothFollowOrig> ().target = GameObject.Find ("Player").transform;
			//Shafts.GetComponent<SmoothFollowOrig> ().target = gameObject.transform;
		}
	}*/
}

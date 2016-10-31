﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class EnemyController : MonoBehaviour 
{
	public GameObject Brain;
	public GameObject bossExplosion;
	public string bossName;
	public Text NameText;
	public Animator NameAnim;
	private GameObject boss;
	private Transform bossHover;
	private SmoothFollowOrig smoothFollowScript;
	private GameController gameControllerScript;
	public GameObject Shafts;

	void Start ()
	{
		gameControllerScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();
		Brain = GameObject.Find ("Brain");
		boss = GameObject.FindGameObjectWithTag ("Boss");
		bossHover = GameObject.Find ("EnemyBossRestPoint").GetComponent<Transform> ();

		smoothFollowScript = GetComponent<SmoothFollowOrig> ();
		smoothFollowScript.target = bossHover.transform;

		NameText = GameObject.Find ("BossText").GetComponent<Text> ();
		NameAnim = GameObject.Find ("BossText").GetComponent<Animator> ();
		NameText.text = "" + bossName + "";
		NameAnim.Play (0);
		Shafts = GameObject.FindGameObjectWithTag ("Shafts");
		Shafts.GetComponent<SmoothFollowOrig> ().target = gameObject.transform;
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

		if (Shafts.GetComponent<SmoothFollowOrig> ().target == null) 
		{
			Shafts.GetComponent<SmoothFollowOrig> ().target = GameObject.Find ("Player").transform;
		}
	}
}

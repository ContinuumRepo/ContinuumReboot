using UnityEngine;
using System.Collections;

public class TimescaleByDistanceFromObject : MonoBehaviour {

	public Transform ObjectRef;
	public Transform Player;

	// Use this for initialization
	void Start () {
	
	}

	void DistanceCalc () {
	}
	
	// Update is called once per frame
	void Update () {
		Time.timeScale = Vector3.Distance (ObjectRef.transform.position, (Player.transform.position) / 8) + 0.05f;
	}
}

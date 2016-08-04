using UnityEngine;
using System.Collections;

public class TimeScaleSetter : MonoBehaviour {

	public float TimeScale;

	// Use this for initialization
	void Start () {
		Time.timeScale = TimeScale;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TimeScaler (float TimeScale) {
		Time.timeScale = TimeScale;
	}
}

using UnityEngine;
using System.Collections;

public class StartTimeScale : MonoBehaviour {

	public float timeScale;

	// Use this for initialization
	void Start () {
		Time.timeScale = timeScale;
	}
}

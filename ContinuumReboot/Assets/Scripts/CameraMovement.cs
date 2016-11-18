using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		//GetComponent<Transform> ().position = new Vector3 (0.2f, 2.1f, -165.1f);
		GetComponent<Transform> ().rotation = Quaternion.Euler (0, 0, -135);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

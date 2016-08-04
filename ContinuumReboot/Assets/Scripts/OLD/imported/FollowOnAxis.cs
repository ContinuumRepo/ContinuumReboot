using UnityEngine;
using System.Collections;

public class FollowOnAxis : MonoBehaviour {
	
	Transform bar;
	
	void Start() {
		bar = GameObject.Find("Player").transform;
	}
	
	void Update() {
		transform.position = new Vector3(bar.position.x, transform.position.y, transform.position.z);
	}
}

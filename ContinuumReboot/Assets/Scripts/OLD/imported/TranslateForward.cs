using UnityEngine;
using System.Collections;

public class TranslateForward : MonoBehaviour 
{
	public float speed = 1.0f;

	void Start () 
	{
	
	}

	void Update () 
	{
		transform.Translate (Vector3.forward * speed * Time.deltaTime);
	
	}
}

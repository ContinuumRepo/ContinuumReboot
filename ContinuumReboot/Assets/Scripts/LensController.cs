using UnityEngine;
using System.Collections;

public class LensController : MonoBehaviour
{
	private Lens lensScript;
	public float radius;

	void Start () 
	{
		lensScript = Camera.main.GetComponent<Lens> ();		
		lensScript.enabled = true;
	}

	void Update () 
	{
		lensScript.radius = radius;
	}
}

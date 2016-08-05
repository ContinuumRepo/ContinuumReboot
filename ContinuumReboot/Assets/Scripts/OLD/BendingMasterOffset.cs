using UnityEngine;
using System.Collections;

public class BendingMasterOffset : MonoBehaviour 
{
	public Vector4 offset = new Vector4 (0, 0, 0, 0);
	public Vector3 Amp;
	public Vector3 speed;


	void Start () 
	{
		
	}

	void Update () 
	{
		offset = new Vector4 (Amp.x * Mathf.Sin (speed.x * Time.time), 
		                      Amp.y * Mathf.Cos (speed.y * Time.time), 
		                      Mathf.Abs(Amp.z * Mathf.Cos (speed.z * Time.time)), 
		                      0);
	}
}

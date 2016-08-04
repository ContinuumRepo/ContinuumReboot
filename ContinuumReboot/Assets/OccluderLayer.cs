using UnityEngine;
using System.Collections;

public class OccluderLayer : MonoBehaviour 
{
	public int Layer;
	private Material material;
	
	void Start () 
	{
		material = GetComponent<Renderer> ().material;
		material.SetInt ("_ZWrite", Layer);
	}

	void Update () 
	{

	}
}

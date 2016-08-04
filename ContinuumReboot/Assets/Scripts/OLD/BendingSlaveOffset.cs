using UnityEngine;
using System.Collections;

public class BendingSlaveOffset : MonoBehaviour
{
	public Vector4 offset;
	private Renderer rend;
	private BendingMasterOffset offsetScript;

	void Start () 
	{
		GameObject offsetObject = GameObject.FindGameObjectWithTag ("BendingMaster");
		offsetScript = offsetObject.GetComponent<BendingMasterOffset> ();

		rend = GetComponent<Renderer> ();
	}

	void Update () 
	{
		offset = offsetScript.offset;
		rend.material.SetVector ("_QOffset", offsetScript.offset);
	}
}

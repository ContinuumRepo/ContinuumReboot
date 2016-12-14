using UnityEngine;
using System.Collections;

public class GlobalMouseVisibility : MonoBehaviour 
{
	public Vector3 currentMousePos;
	public Vector3 mousePosition;

	void Start () 
	{
		InvokeRepeating ("CheckMousePos", 0, 3);
	}

	void Update () 
	{
		currentMousePos = Input.mousePosition;

		if (Input.mousePosition != mousePosition) {
			Cursor.visible = true;
		} else {
			Cursor.visible = false;
		}
	}
		
	void UpdateMousePos ()
	{
		mousePosition = Input.mousePosition;
	}

	void CheckMousePos ()
	{
		UpdateMousePos ();
	}
}

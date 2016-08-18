using UnityEngine;
using System.Collections;

public class GlobalMouseVisibility : MonoBehaviour 
{
	public float visibleTime;
	public float visibleDuration = 4.0f; 
	public Vector3 mousePos;
	public bool isVisible;

	void Start () 
	{
		visibleTime = visibleDuration;
	}

	void Update () 
	{
		visibleTime -= Time.unscaledDeltaTime;
		mousePos = Input.mousePosition;

		if (visibleTime <= 0)
		{
			Cursor.visible = false;
			isVisible = false;
		}
			
		if (visibleTime > 0) 
		{
			Cursor.visible = true;
			isVisible = true;
		}

		if (Input.GetAxis ("Mouse X") > 0 || Input.GetAxis ("Mouse Y") > 0) 
		{
			visibleTime = visibleDuration;
		}
	}
}

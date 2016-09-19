using UnityEngine;
using System.Collections;

public class GlobalMouseVisibility : MonoBehaviour 
{
	public float visibleTime;
	public float visibleDuration = 4.0f; 
	public Vector3 mousePos;
	public bool isVisible;

	public enum mode 
	{
		Normal,
		Alternative
	}

	public mode VisibleMode;

	void Start () 
	{
		if (VisibleMode == mode.Normal)
		{
			visibleTime = visibleDuration;
		}

		if (VisibleMode == mode.Alternative) 
		{
			Cursor.visible = false;
		}
	}

	void Update () 
	{
		if (VisibleMode == mode.Normal) 
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

		if (VisibleMode == mode.Alternative) 
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Confined;
		}
	}
}

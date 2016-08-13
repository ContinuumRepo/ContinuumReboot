using UnityEngine;
using System.Collections;

public class MenuSettings: MonoBehaviour 
{
	public int WindowWidth;
	public int WindowHeight;
	public bool goFullscreen = false;

	public void SetResolution (bool setRes)
	{
		Screen.SetResolution (WindowWidth, WindowHeight, goFullscreen);
	}
}

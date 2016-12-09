using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FindCamInCanvas : MonoBehaviour 
{
	public Camera Cam;
	public space Space;
	public enum space
	{
		world,
		camera,
		overlay
	}

	void Start () 
	{
		if (Cam == null) 
		{
			Cam = Camera.main;	
		}

		if (Space == space.world) 
		{
			GetComponent<Canvas> ().renderMode = RenderMode.WorldSpace;
			GetComponent<Canvas> ().worldCamera = Cam;
		}

		if (Space == space.camera)
		{
			GetComponent<Canvas> ().renderMode = RenderMode.ScreenSpaceCamera;
			GetComponent<Canvas> ().worldCamera = Cam;
		}

		if (Space == space.overlay)
		{
			GetComponent<Canvas> ().renderMode = RenderMode.ScreenSpaceOverlay;
		}
	}
}

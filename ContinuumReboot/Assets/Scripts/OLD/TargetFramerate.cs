using UnityEngine;
using System.Collections;

public class TargetFramerate : MonoBehaviour 
{
	public int Framerate = 60;

	void Awake() 
	{
		Application.targetFrameRate = Framerate;
	}
}

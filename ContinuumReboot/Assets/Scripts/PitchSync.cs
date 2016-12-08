using UnityEngine;
using System.Collections;

public class PitchSync : MonoBehaviour 
{
	public AudioSource master;
	public AudioSource slave;

	void Update () 
	{
		slave.pitch = master.pitch;
	}
}

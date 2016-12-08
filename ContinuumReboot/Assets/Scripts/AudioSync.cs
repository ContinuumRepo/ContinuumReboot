using UnityEngine;
using System.Collections;

public class AudioSync : MonoBehaviour 
{
	public AudioSource master;
	public AudioSource slave;

	void Update () 
	{
		slave.timeSamples = master.timeSamples;
	}
}

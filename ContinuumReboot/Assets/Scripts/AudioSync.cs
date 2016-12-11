// Syncronizes audio source with another audio source.

using UnityEngine;
using System.Collections;

public class AudioSync : MonoBehaviour 
{
	public AudioSource master;
	public AudioSource slave;

	void LateUpdate () 
	{
		slave.timeSamples = master.timeSamples;
	}
}

using UnityEngine;
using System.Collections;

public class RandomExplosionSound : MonoBehaviour 
{
	public AudioClip[] Audioclips;
	[Range (0, 1)]
	public float volume = 0.75f;

	void Start () 
	{
		GetComponent<AudioSource> ().PlayOneShot (Audioclips [Random.Range (0, Audioclips.Length)], volume);
	}
}

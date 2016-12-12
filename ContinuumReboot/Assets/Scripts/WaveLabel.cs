using UnityEngine;
using System.Collections;

public class WaveLabel : MonoBehaviour 
{
	public int waveNumber;

	void Start () 
	{
		waveNumber = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().wave;
		GetComponent<Canvas> ().worldCamera = Camera.main;
	}
}

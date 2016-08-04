using UnityEngine;
using System.Collections;

public class LightScript : MonoBehaviour 
{
	public Light lightObject;
	//private TimeController timeControllerScript;

	void Start () 
	{
		//GameObject timeControllerObject = GameObject.FindGameObjectWithTag ("TSCalc");
		//timeControllerScript = timeControllerObject.GetComponent<TimeController> ();

		lightObject = GetComponent<Light> ();
	}

	void Update () 
	{
		lightObject.color = new Color (1-Time.timeScale + 0.6f, Time.timeScale, Time.timeScale/2);
	}
}

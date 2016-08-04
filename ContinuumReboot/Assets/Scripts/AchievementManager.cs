using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour 
{
	public string[] Achievement;
	public bool[] unlockedAchievement;
	public GameObject[] ParentGameObjects;
	public bool resetAchievements;

	[Header ("A1: Got Game")]
	public GameObject[] AchievementObject;

	void Start () 
	{
		if (unlockedAchievement [0] == false && PlayerPrefs.GetString(Achievement[0]) == "Locked") 
		{
			unlockedAchievement [0] = true;
			Instantiate (AchievementObject [0], Vector3.zero, Quaternion.identity);
			Debug.Log ("You unlocked achievement " + Achievement [0] + ".");
		}
	
	}

	void FixedUpdate ()
	{
		if (unlockedAchievement [0] == false) 
		{
			PlayerPrefs.SetString (Achievement [0], "Locked");
		}

		if (unlockedAchievement [0] == true) 
		{
			PlayerPrefs.SetString (Achievement [0], "Unlocked");
		}

		if (Input.GetKeyDown (KeyCode.F8) && resetAchievements == false) 
		{
			resetAchievements = true;

			// Put PlayerPrefs stuff here to reset
			PlayerPrefs.SetString(Achievement[0], "Locked");
			Debug.Log ("You reset all achievements.");
		}
	}
}

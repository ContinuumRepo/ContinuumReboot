using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HighscoreController : MonoBehaviour
{
	public int maxNoHS = 10;

	private List<string> hsNames = new List<string>();
	private List<int> hsScores = new List<int>();

	/// <summary>
	/// Gets all names and scores for highscores
	/// from PlayerPrefs and sets them to lists.
	/// These are used to check for and update
	/// highscores at Game Over.
	/// </summary>
	void Start()
	{
		string prefsNameBase = "HSName";
		string prefsScoreBase = "HSScore";

		for (int i = 0; i < maxNoHS; i++)
		{
			string keyName = prefsNameBase + i.ToString();
			string keyScore = prefsScoreBase + i.ToString();

			if (PlayerPrefs.HasKey (keyName))
			{
				hsNames[i] = PlayerPrefs.GetString (keyName);
				hsScores[i] = PlayerPrefs.GetInt (keyScore);
			}
			else
			{
				break;
			}
		}
	}

	/// <summary>
	/// Checks if 'score' is a larger value than the scores already saved.
	/// Returns true if it one of the top 10 scores.
	/// Returns false if it is not larger than any saved.
	/// </summary>
	public bool CheckForHighScore (int score)
	{
		// If there are no highscores set, score is a new highscore
		if (hsScores.Count <= 0)
		{
			return true;
		}

		// Loop through set highscores, if score is greater than any set, return true
		for (int i = 0; i < hsNames.Count; i++)
		{
			if (score > hsScores[i])
				return true;
		}

		// Looped and none were true, then return false
		return false;
	}

	/// <summary>
	/// Calculates the score against those already saved and
	/// inserts the score and name into the appropriate index.
	/// </summary>
	public void InsertNewHighScore (string name, int score)
	{
		// If there are no highscores set, add as the first index
		if (hsScores.Count <= 0)
		{
			hsNames.Add (name);
			hsScores.Add (score);
		}
		else
		{
			for (int i = 0; i < hsNames.Count; i++)
			{
				if (score > hsScores[i])
				{
					hsNames.Insert (i, name);
					hsScores.Insert (i, score);
					UpdatePrefs();
					break;
				}
			}
		}
	}

	/// <summary>
	/// Updates the Highscore PlayerPrefs after the lists have been changed
	/// </summary>
	private void UpdatePrefs()
	{
		if (hsNames.Count != hsScores.Count)
		{
			Debug.LogWarning ("Number of Highscore names do not match the number of Highscore scores! Highscores not saved!");
		}
		else
		{
			string prefsNameBase = "HSName";
			string prefsScoreBase = "HSScore";

			for (int i = 0; i < hsNames.Count; i++)
			{
				string keyName = prefsNameBase + i.ToString();
				string keyScore = prefsScoreBase + i.ToString();

				PlayerPrefs.SetString (keyName, hsNames[i]);
				PlayerPrefs.SetInt (keyScore, hsScores[i]);
			}
		}
	}
}


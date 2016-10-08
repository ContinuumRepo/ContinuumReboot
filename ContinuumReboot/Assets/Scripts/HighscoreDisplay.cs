using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighscoreDisplay : MonoBehaviour
{
	public Text[] highScoreNames;
	public Text[] highScoreScores;

	void Start()
	{
		string prefsNameBase = "HSName";
		string prefsScoreBase = "HSScore";

		for (int i = 0; i < highScoreNames.Length; i++)
		{
			string keyName = prefsNameBase + i.ToString();
			string keyScore = prefsScoreBase + i.ToString();

			if (PlayerPrefs.HasKey (keyName))
			{
				highScoreNames[i].text = PlayerPrefs.GetString (keyName);
				highScoreScores[i].text = PlayerPrefs.GetInt (keyScore).ToString();
			}
			else
			{
				break;
			}
		}
	}
}


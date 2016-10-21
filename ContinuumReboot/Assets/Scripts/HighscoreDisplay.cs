using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighscoreDisplay : MonoBehaviour
{
	public int maxNoHS = 10; // Same as maxNoHS in Highscore Controller, does not have to be the same as the amount displayed

	public Text[] highScoreNames;
	public Text[] highScoreScores;
	public Text[] highScoreWaves;

	public GameObject resetPanel;
	public MenuBackButton backButtonScript;

	private string prefsNameBase = "HSName";
	private string prefsScoreBase = "HSScore";
	private string prefsWaveBase = "HSWave";

	private bool resetting = false;

	void Start()
	{
		for (int i = 0; i < highScoreNames.Length; i++)
		{
			string keyName = prefsNameBase + i.ToString();
			string keyScore = prefsScoreBase + i.ToString();
			string keyWave = prefsWaveBase + i.ToString();

			if (PlayerPrefs.HasKey (keyName))
			{
				highScoreNames[i].text = PlayerPrefs.GetString (keyName);
				highScoreScores[i].text = PlayerPrefs.GetInt (keyScore).ToString();
				highScoreWaves[i].text = PlayerPrefs.GetInt (keyWave).ToString();
			}
			else
			{
				break;
			}
		}
	}

	void Update()
	{
		if (Input.GetAxis ("Cancel") > 0) // If B is clicked
		{
			if (resetting)
			{
				backButtonScript.BackToMainButton();
			}
			else
			{
				resetPanel.SetActive (false);
				PlayerPrefs.SetString ("InputMenu", "leaderboards");
			}
		}

		// If select is clicked, bring up reset confirm panel
		if (Input.GetKeyDown ("joystick button 6"))
		{
			resetPanel.SetActive (true);
			PlayerPrefs.SetString ("InputMenu", "resethighscores");
		}

		if (Input.GetKeyDown (KeyCode.Slash))
		{
			resetPanel.SetActive (true);
			PlayerPrefs.SetString ("InputMenu", "resethighscores");
		}
	}

	public void ResetHighScores()
	{
		for (int i = 0; i < maxNoHS; i++)
		{
			string keyName = prefsNameBase + i.ToString();
			string keyScore = prefsScoreBase + i.ToString();
			string keyWave = prefsWaveBase + i.ToString();

			if (PlayerPrefs.HasKey (keyName))
			{
				PlayerPrefs.DeleteKey (keyName);
				PlayerPrefs.DeleteKey (keyScore);
				PlayerPrefs.DeleteKey (keyWave);
			}
			else
			{
				break;
			}
		}
		SceneManager.LoadScene ("main");
	}
}


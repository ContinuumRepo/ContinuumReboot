using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameOverController : MonoBehaviour
{
	public GameObject highscoreText;
	public Text gameOverScoreText;
	public GameController gameController;
	public HighscoreController hsController;
	public HighscoreInput hsInput;

	public InputScroll gameOverScroll;
	public float gameOverInputWait;

	private int score;
	private int wave;

	void OnEnable ()
	{
		score = (int) Mathf.Round (gameController.CurrentScore);
		wave = gameController.wave;

		gameOverScoreText.text = score.ToString();

		if (hsController.CheckForHighScore (score))
		{
			highscoreText.SetActive (true);
			hsInput.enabled = true;
			gameOverScroll.enabled = false;
		}
		else
		{
			highscoreText.SetActive (false);
			EnableGameOverScroll();
		}
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Slash))
			Debug.Log (PlayerPrefs.GetString ("InputMenu"));
	}

	public void SubmitHighscore (string name)
	{
		hsController.InsertNewHighScore (name, score, wave);
		hsInput.enabled = false;
		EnableGameOverScroll();
	}

	private void EnableGameOverScroll ()
	{
		gameOverScroll.enabled = true;
		gameOverScroll.WaitToRenameInputMenu (gameOverInputWait, "gameover");
	}
}

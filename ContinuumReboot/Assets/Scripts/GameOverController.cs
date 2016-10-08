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

	void OnEnable ()
	{
		score = (int) Mathf.Round (gameController.CurrentScore);

		gameOverScoreText.text = score.ToString();

		if (hsController.CheckForHighScore (score))
		{
			highscoreText.SetActive (true);
			hsInput.enabled = true;
		}
		else
		{
			EnableGameOverScroll();
		}

	}

	public void SubmitHighscore (string name)
	{
		hsController.InsertNewHighScore (name, score);
		hsInput.enabled = false;
		EnableGameOverScroll();
	}

	private void EnableGameOverScroll ()
	{
		gameOverScroll.enabled = true;
		gameOverScroll.WaitToRenameInputMenu (gameOverInputWait, "gameover");
	}
}

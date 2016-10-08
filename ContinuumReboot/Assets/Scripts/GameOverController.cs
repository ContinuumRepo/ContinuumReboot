using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameOverController : MonoBehaviour
{
	public GameObject highscoreText;
	public Text gameOverScoreText;
	public GameController gameController;

	public InputScroll gameOverScroll;
	public float gameOverInputWait;

	void OnEnable ()
	{
		int score = (int) Mathf.Round (gameController.CurrentScore);

		gameOverScoreText.text = score.ToString();

		if (CheckForHighScore (score))
		{
			highscoreText.SetActive (true);
			// Enable highscore input
		}
		else
		{
			gameOverScroll.enabled = true;
			gameOverScroll.WaitToRenameInputMenu (gameOverInputWait, "gameover");
		}

	}

	private bool CheckForHighScore (int score)
	{


		return false;
	}
}

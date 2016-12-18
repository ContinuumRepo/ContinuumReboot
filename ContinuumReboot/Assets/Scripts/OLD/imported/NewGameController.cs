using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NewGameController : MonoBehaviour 
{

	[Header ("TIME")]
	public Text TimescaleText;
	public float TimescaleValue;
	//public TimescaleController Tscalc;
	//public GameObject debugInfoUI;

	public Transform CentralSpawnTransform;

	[Header ("POINTS")]
	public GameObject[] Points;
	public int pointCount;
	public float pointStartWait;
	public float pointSpawnWait;
	public float pointWaveWait;
	public Vector3 pointSpawnValues;
	public int currentPoints;
	public Text currentPointsText;

	[Header ("PAUSING")]
	public bool isPaused;

	[Header ("SCORING")]
	public int currentScore;
	public Text ScoreTextMobile;
	public float Addscore;
	//public ParticleSystem MainEngine;
	//public Text EndScoreText;
	//public int HighScore;
	//public Text HighScoreText;
	//public GameObject CelebrationEffect;
	//public bool savedStats = false; 
	//public float metres;
	//public Text LengthText;
	
	void Start () 
	{
		StartCoroutine (PointSpawnWaves ());
	}

	void Update () 
	{
		if (isPaused) 
		{
			Time.timeScale = 0;
			TimescaleValue = 0;
		}

		if (!isPaused) 
		{
			TimescaleValue = Time.timeScale;
			// Converts into 2 decimal places
			TimescaleText.text = "x " + string.Format ("{0:0.00}", Mathf.Round (TimescaleValue * 100f) / 100f) + "";
			// sets UI World points
			currentPointsText.text = "" + currentPoints + "";

			ScoreTextMobile.text = "" + currentScore;
			//EndScoreText.text = "";

			// SCORING
			currentScore += Mathf.RoundToInt (Time.timeScale * Time.deltaTime * 100.0f);

			if (currentScore < 0) {
				currentScore = 1;
			}
		}
	}

	IEnumerator PointSpawnWaves ()
	{
		yield return new WaitForSeconds (pointStartWait);
		while (true) {
			for (int i = 0; i < pointCount; i++) {
				GameObject hazard = Points [UnityEngine.Random.Range (0, Points.Length)];
				Vector3 PointSpawnPos = new Vector3 (Mathf.RoundToInt(Random.Range(-pointSpawnValues.x, pointSpawnValues.x)), Mathf.RoundToInt(Random.Range(-pointSpawnValues.y, pointSpawnValues.y) + 8), pointSpawnValues.z);
				Instantiate (hazard, PointSpawnPos, CentralSpawnTransform.rotation);
				yield return new WaitForSeconds (pointSpawnWait);
				//spawnWait = spawnWait - 0.002f; // If you want to make it go faster over time.
			}
			
			yield return new WaitForSeconds (pointWaveWait);
		}
	}
}

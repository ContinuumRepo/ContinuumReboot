using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game : MonoBehaviour 
{
	[Header ("GAME CONDITIONS")]
	public bool isGameOver;
	public bool isPreGame;
	public bool isPaused;

	[Header ("SCORING")]
	public int currentScore;
	public int ScoreSpeed;
	public Text ScoreText;

	[Header ("POINTS")]
	public GameObject[] Points;
	public float pointStartWait;
	public float pointCount;
	public float pointWaveWait;
	public float pointSpawnWait;
	public Vector3 pointSpawnPos;
	public int currentPoints;
	public Text currentPointsText;

	[Header ("TIME MODS")]
	public GameObject[] TimeMods;
	public float timeModStartWait;
	public float timeModCount;
	public float timeModWaveWait;
	public float timeModSpawnWait;
	public Vector3 timeModSpawnPos;

	[Header ("ENEMIES")]
	public GameObject[] Enemies;
	public float enemyStartWait;
	public float enemyCount;
	public float enemyWaveWait;
	public float enemySpawnWait;
	public Vector3 enemySpawnPos;
	public float minEnemySpawnRate = 0.25f;

	[Header ("POWERUPS")]
	public float powerupStartWait;
	public float powerupCount;
	public Vector3 powerupSpawnPos;
	public float powerupSpawnWait;
	public float powerupWaveWait;
	public GameObject[] powerups;

	[Header ("BARRIERS")]
	public float barrierStartWait;
	public float barrierCount;
	public Vector3 barrierSpawnPos;
	public float barrierSpawnWait;
	public float barrierWaveWait;
	public GameObject barrier;

	[Header ("GAME OVER")]
	public GameObject GameOverPanel;
	//public Text GOPointsText;
	//public Text GOLMText;
	//public Text GOEnemyKillsText;
	//public Text TotalPointsText;
	//public Text TotalLevelModsText;
	public GameObject Crosshair;

	void Start () 
	{
		StartCoroutine (PowerupWaves ());
		StartCoroutine (PointSpawnWaves ());
		StartCoroutine (TimeModSpawnWaves ());
		StartCoroutine (EnemySpawnWaves ());
		StartCoroutine (BarrierSpawnWaves ());
		GameOverPanel.SetActive (false);
		isPreGame = true;
	}

	void FixedUpdate () 
	{
		currentScore += Mathf.RoundToInt (Time.timeScale * Time.deltaTime * 100.0f);
		ScoreText.text = "" + currentScore + "";
		currentPointsText.text = "" + currentPoints + "";

		if (currentScore < 0) 
		{
			currentScore = 0;
		}

		if (isPreGame == true) 
		{
			Crosshair.SetActive (false);
		}

		if (enemySpawnWait < minEnemySpawnRate || enemyWaveWait < minEnemySpawnRate) 
		{
			enemySpawnWait = minEnemySpawnRate;
			enemyWaveWait = minEnemySpawnRate;
		}
			
	}

	// Points
	IEnumerator PointSpawnWaves ()
	{
		yield return new WaitForSeconds (pointStartWait);
		while (true) 
		{
			for (int i = 0; i < pointCount; i++) 
			{	
				GameObject points = Points[Random.Range (0, Points.Length)];
				Vector3 pointSpawnPosition = new Vector3 (Random.Range (-pointSpawnPos.x, pointSpawnPos.x), 
				                                     Random.Range (-pointSpawnPos.y - 20, pointSpawnPos.y - 20), 
				                                     pointSpawnPos.z);

				//Quaternion spawnRotation = Quaternion.identity;
				Instantiate (points, pointSpawnPosition, Quaternion.identity);
				yield return new WaitForSeconds (pointSpawnWait);
			}
			yield return new WaitForSeconds (pointWaveWait);
		}
	}

	// Time Mods
	IEnumerator TimeModSpawnWaves ()
	{
		yield return new WaitForSeconds (timeModStartWait);
		while (true) 
		{
			for (int i = 0; i < pointCount; i++) 
			{	
				GameObject timeMods = TimeMods[Random.Range (0, TimeMods.Length)];
				Vector3 timeModSpawnPosition = new Vector3 (Random.Range (-timeModSpawnPos.x, timeModSpawnPos.x), 
				                                            Random.Range (-timeModSpawnPos.y - 20, timeModSpawnPos.y - 20), 
				                                            timeModSpawnPos.z);
				
				//Quaternion spawnRotation = Quaternion.identity;
				Instantiate (timeMods, timeModSpawnPosition, Quaternion.identity);
				yield return new WaitForSeconds (timeModSpawnWait);
			}
			yield return new WaitForSeconds (timeModWaveWait);
		}
	}

	// Enemies
	IEnumerator EnemySpawnWaves ()
	{
		yield return new WaitForSeconds (enemyStartWait);
		while (true) 
		{
			for (int i = 0; i < enemyCount; i++) 
			{	
				GameObject enemy = Enemies[Random.Range (0, Enemies.Length)];
				Vector3 enemySpawnPosition = new Vector3 (Random.Range (-enemySpawnPos.x, enemySpawnPos.x), 
					Random.Range (-enemySpawnPos.y - 20, enemySpawnPos.y - 20), 
					enemySpawnPos.z);

				//Quaternion spawnRotation = Quaternion.identity;
				Instantiate (enemy, enemySpawnPosition, Quaternion.identity);
				yield return new WaitForSeconds (enemySpawnWait);
				enemySpawnWait -= 0.002f; // If you want to make it go faster over time.
				enemyWaveWait -= 0.015f;
			}
			yield return new WaitForSeconds (enemyWaveWait);
		}
	}

	// Barriers
	IEnumerator BarrierSpawnWaves ()
	{
		
		yield return new WaitForSeconds (barrierStartWait);
		while (true) {
			for (int i = 0; i < barrierCount; i++) {
				GameObject barriers = barrier;
				
				Instantiate (barriers, barrierSpawnPos, Quaternion.identity);
				yield return new WaitForSeconds (barrierSpawnWait);
				//spawnWait = spawnWait - 0.002f; // If you want to make it go faster over time.
			}
			
			yield return new WaitForSeconds (barrierWaveWait);
		}
	}

	// Powerups
	IEnumerator PowerupWaves ()
	{
		yield return new WaitForSeconds (powerupStartWait);
		while (true) {
			for (int i = 0; i < powerupCount; i++) {
				GameObject powerup = powerups[Random.Range (0, powerups.Length)];;

				Vector3 powerupSpawnPosition = new Vector3 (Random.Range (-powerupSpawnPos.x, powerupSpawnPos.x), 
				                                            Random.Range (-powerupSpawnPos.y - 20, powerupSpawnPos.y - 20), 
				                                            powerupSpawnPos.z);

				Instantiate (powerup, powerupSpawnPosition, Quaternion.identity);
				yield return new WaitForSeconds (powerupSpawnWait);
				powerupSpawnWait += 0.002f; // If you want to make it go slower over time.
			}
			
			yield return new WaitForSeconds (powerupWaveWait);
		}
	}
}

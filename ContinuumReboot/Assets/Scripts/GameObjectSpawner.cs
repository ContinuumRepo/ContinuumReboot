using UnityEngine;
using System.Collections;

public class GameObjectSpawner : MonoBehaviour 
{
	public GameObject[] Blocks;
	public float startWait;
	public float spawnWait;
	public Vector3 spawnValues;
	public int spawnCount;
	public float verticalOffset;

	void Start () 
	{
		StartCoroutine (SpawnBlocks ());
	}
	
	void Update () 
	{
	
	}

	IEnumerator SpawnBlocks ()
	{
		yield return new WaitForSeconds (startWait);
		while (true) {
			for (int i = 0; i < spawnCount; i++) {
				GameObject hazard = Blocks [UnityEngine.Random.Range (0, Blocks.Length)];

				Vector3 spawnPosition = new Vector3 (spawnValues.x, Random.Range(-spawnValues.y, spawnValues.y) - verticalOffset, spawnValues.z);

				Instantiate (hazard,  spawnPosition, Quaternion.Euler(0, 180, 45));
				yield return new WaitForSeconds (spawnWait);
				//spawnWait = spawnWait - 0.002f; // If you want to make it go faster over time.
			}

			//yield return new WaitForSeconds (WaveWait);
		}
	}
}

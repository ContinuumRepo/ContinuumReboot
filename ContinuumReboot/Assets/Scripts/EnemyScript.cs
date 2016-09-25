using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour 
{
	public int Health = 50;
	public GameObject Explosion;
	private float nextBomb;
	public float bombRate = 2.0f;
	public GameObject[] Bombs;
	public int BombNumberMax;
	public int CurrentBomb;
	public Transform EnemyTrans;

	void Start () 
	{
		EnemyTrans = GameObject.Find ("EnemyRestPoint").GetComponent<Transform>();
		GetComponent<SmoothFollowOrig> ().target = EnemyTrans.transform;
		BombNumberMax = Bombs.Length;
		gameObject.transform.rotation = Quaternion.Euler (-90, 0, 0);
	}

	void Update () 
	{
		if (Health > 0 && Time.time > nextBomb)
		{
			nextBomb = Time.time + bombRate;
			Instantiate (Bombs [CurrentBomb], gameObject.transform.position, gameObject.transform.rotation);
			CurrentBomb += 1;

			if (CurrentBomb >= BombNumberMax) 
			{
				CurrentBomb = 0;
			}
		}

		if (Health <= 0) 
		{
			Destroy (gameObject);
			Debug.Log ("Destroyed an enemy");
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Bullet") 
		{
			Health -= 25;
			Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
			//Destroy (other.gameObject);
		}
	}
}

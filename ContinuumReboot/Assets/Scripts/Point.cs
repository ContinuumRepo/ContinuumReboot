using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Point : MonoBehaviour 
{
	private Game gameControllerScript;
	public int pointValue = 1;
	public GameObject Explosion;
	public ParticleSystem StaticPointsExplosion;
	public Text pointsText;
	private Animator PointsTextAnim;
	public enum colour {Orange, Yellow, Green, Blue, Purple}
	public colour ColorType;

	void Start () 
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		gameControllerScript = gameControllerObject.GetComponent<Game> ();
		GameObject PointsTextAnimObject = GameObject.FindGameObjectWithTag("PointsText");
		PointsTextAnim = PointsTextAnimObject.GetComponent<Animator> ();
		pointsText = PointsTextAnimObject.GetComponent<Text> ();
		GameObject StaticPointsObject = GameObject.FindGameObjectWithTag ("StaticPointExplosion");
		StaticPointsExplosion = StaticPointsObject.GetComponent<ParticleSystem> ();

		pointsText.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
	}

	void Update () 
	{
	
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Bullet" || other.tag == "Player") 
		{
			gameControllerScript.currentPoints += pointValue;
			// Plays default Animation
			PointsTextAnim.Play(0);

			Instantiate (Explosion, gameObject.transform.position, gameObject.transform.rotation);
			//Instantiate (StaticPointsExplosion, StaticExplosionPos, Quaternion.identity);
			StaticPointsExplosion.Play ();
			Destroy (gameObject);
			Debug.Log ("You collected a point.");

			if (ColorType == colour.Orange) 
			{
				StaticPointsExplosion.startColor = new Color (1.0f, 0.25f, 0.25f);
				pointsText.color = new Color (1.0f, 0.25f, 0.25f);
			}

			if (ColorType == colour.Yellow) 
			{
				StaticPointsExplosion.startColor = new Color (1.0f, 1.0f, 0.0f);
				pointsText.color = new Color (1.0f, 1.0f, 0.0f);
			}

			if (ColorType == colour.Green) 
			{
				StaticPointsExplosion.startColor = new Color (0.0f, 1.0f, 0.0f);
				pointsText.color = new Color (0.0f, 1.0f, 0.0f);
			}

			if (ColorType == colour.Blue) 
			{
				StaticPointsExplosion.startColor = new Color (0.0f, 0.5f, 1.0f);
				pointsText.color = new Color (0.0f, 0.5f, 1.0f);
			}

			if (ColorType == colour.Purple) 
			{
				StaticPointsExplosion.startColor = new Color (1.0f, 0.0f, 1.0f);
				pointsText.color = new Color (1.0f, 0.0f, 1.0f);
			}
		}
	}
}

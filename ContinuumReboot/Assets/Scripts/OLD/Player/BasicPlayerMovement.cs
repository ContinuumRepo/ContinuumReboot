using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BasicPlayerMovement : MonoBehaviour
{

	[Header ("Health Stats")]
	public float currentHealth;
	public float startingHealth = 100.0f;
	public float deathHealth = 0.0f;
	public float Lives;
	public float startingLives = 3.0f;
	public float decrementLife = 1.0f;
	public Image HealthFill;

    void Start()
    {
    }
	
    void Update ()
    {

		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			Jump ();
		}

		HealthFill.fillAmount = currentHealth / 100;

		// Health is 76 or above
		if (currentHealth > 75 && currentHealth <= 100) 
		{
			HealthFill.color = new Color (0, 1, 0.549f);
		}

		// Health is 51 - 75
		if (currentHealth > 50 && currentHealth <= 75) 
		{
			HealthFill.color = new Color (0.19f, 1, 0.0f);
		}

		// Health is 26 - 50
		if (currentHealth > 25 && currentHealth <= 50) 
		{
			HealthFill.color = new Color (1, 1, 0.0f);
		}

		// Health is less than 25
		if (currentHealth <= 25) {
			HealthFill.color = new Color (1, 0, 0.0f);
		}
	}

	void Jump ()
	{
	}
}

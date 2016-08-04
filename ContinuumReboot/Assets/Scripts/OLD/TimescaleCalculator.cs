using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimescaleCalculator : MonoBehaviour 
{
	public GameObject Player;
	public BasicPlayerMovement PlayerMovementScript;
	public float distance;
	public float TimeScale;
	public float TimeScaleNow;
	public float Dampener;
	public float dampenIncreaser = 1500.0f;
	public float YTime = 1.2f;
	public Renderer Tube;

	[Header ("LEVELS")]
	[Range (1, 10)]
	public int Level;
	public float timeScaleLevelAdder = 1.0f;
	public float levelOneDuration = 60.0f;
	public float levelOneTimeRemaining;
	public bool isLevelOne;
	public Image LevelOneSlider;

	public float levelTwoDuration = 60.0f;
	public float levelTwoTimeRemaining;
	public bool isLevelTwo;
	public Image LevelTwoSlider;

	public float levelThreeDuration = 60.0f;
	public float levelThreeTimeRemaining;
	public bool isLevelThree;
	public Image LevelThreeSlider;


	public float levelFourDuration = 60.0f;
	public float levelFourTimeRemaining;
	public bool isLevelFour;
	public Image LevelFourSlider;

	public float levelFiveDuration = 0.0f;
	public float levelFiveTimeRemaining;
	public bool isLevelFive;
	public Image LevelFiveSlider;

	public GameObject MainCam;
	public GameObject FasterLabel;
	public float LevelWarningTime = 5.0f;
	public Text currentLevelText;
	public Text nextLevelText;
	public float MoveSpeed;

	void Start () 
	{
		isLevelOne = true;
		Level = 1;
		Time.timeScale = 1;
		//StartCoroutine (LevelOne ());
		levelOneTimeRemaining = levelOneDuration;
		levelTwoTimeRemaining = levelTwoDuration;
		levelThreeTimeRemaining = levelThreeDuration;
		levelFourTimeRemaining = levelFourDuration;
		levelFiveTimeRemaining = levelFiveDuration;

		LevelOneSlider.enabled = true;
		LevelTwoSlider.enabled = false;
		LevelThreeSlider.enabled = false;
		LevelFourSlider.enabled = false;
		LevelFiveSlider.enabled = false;

		LevelOneSlider.fillAmount = 0;
		LevelTwoSlider.fillAmount = 0;
		LevelThreeSlider.fillAmount = 0;
		LevelFourSlider.fillAmount = 0;
		LevelFiveSlider.fillAmount = 0;

		FasterLabel.SetActive (false);
		
	}

	void Update () 
	{
		currentLevelText.text = "" + Level + "";
		nextLevelText.text = "" + (Level + 1) + "";
		Tube.materials[0].SetColor ("_EmissionColor", new Color (1- (TimeScaleNow/2), 1+ (TimeScaleNow/5), 1+ (TimeScaleNow/2)));

		// Timer for level 1
		if (levelOneTimeRemaining > 0 && isLevelOne)
		{
			levelOneTimeRemaining -= Time.unscaledDeltaTime;
			LevelOneSlider.fillAmount = (levelOneDuration - levelOneTimeRemaining) / levelOneDuration;
		}

		// Caps Duration of Level One
		if (levelOneTimeRemaining > levelOneDuration) 
		{
			levelOneTimeRemaining = levelOneDuration;
		}

		// Level One Warning (shows faster text object)
		if (Level == 1 && levelOneTimeRemaining < LevelWarningTime) 
		{
			FasterLabel.SetActive (true);
		}

		// End of Level 1
		if (levelOneTimeRemaining <= 0 && isLevelOne) 
		{
			levelOneTimeRemaining = levelOneDuration;
			Level += 1;
			isLevelOne = false;
			isLevelTwo = true;
			FasterLabel.SetActive (false);
		}

		// Timer for level 2
		if (levelTwoTimeRemaining > 0 && isLevelTwo)
		{
			levelTwoTimeRemaining -= Time.unscaledDeltaTime;
			LevelOneSlider.enabled = true;
			LevelTwoSlider.enabled = true;
			LevelThreeSlider.enabled = true;
			LevelFourSlider.enabled = false;
			LevelFiveSlider.enabled = false;
			LevelTwoSlider.fillAmount = (levelTwoDuration - levelTwoTimeRemaining) / levelTwoDuration;
		}

		// Caps Duration of Level Two
		if (levelTwoTimeRemaining > levelTwoDuration) 
		{
			levelTwoTimeRemaining = levelTwoDuration;
		}

		// Level Two Warning (shows faster text object)
		if (Level == 2 && levelTwoTimeRemaining < LevelWarningTime) 
		{
			FasterLabel.SetActive (true);
		}
		
		if (levelTwoTimeRemaining <= 0 && isLevelTwo) 
		{
			levelTwoTimeRemaining = levelTwoDuration;
			Level += 1;
			isLevelOne = false;
			isLevelTwo = false;
			isLevelThree = true;
			FasterLabel.SetActive (false);
		}

		// Timer for level 3
		if (levelThreeTimeRemaining > 0 && isLevelThree)
		{
			levelThreeTimeRemaining -= Time.unscaledDeltaTime;

			LevelOneSlider.enabled = false;
			LevelTwoSlider.enabled = true;
			LevelThreeSlider.enabled = true;
			LevelFourSlider.enabled = false;
			LevelFiveSlider.enabled = false;
			LevelThreeSlider.fillAmount = (levelThreeDuration - levelThreeTimeRemaining) / levelThreeDuration;
		}

		// Caps Duration of Level Three
		if (levelThreeTimeRemaining > levelThreeDuration) 
		{
			levelThreeTimeRemaining = levelThreeDuration;
		}

		// Level Three Warning (shows faster text object)
		if (Level == 3 && levelThreeTimeRemaining < LevelWarningTime) 
		{
			FasterLabel.SetActive (true);
		}
		
		if (levelThreeTimeRemaining <= 0 && isLevelThree) 
		{
			levelThreeTimeRemaining = levelThreeDuration;
			Level += 1;
			isLevelOne = false;
			isLevelTwo = false;
			isLevelThree = false;
			isLevelFour = true;
			FasterLabel.SetActive (false);
		}

		// Timer for level 4
		if (levelFourTimeRemaining > 0 && isLevelFour)
		{
			levelFourTimeRemaining -= Time.unscaledDeltaTime;
			LevelTwoSlider.enabled = false;
			LevelFourSlider.enabled = true;

			LevelOneSlider.enabled = false;
			LevelTwoSlider.enabled = false;
			LevelThreeSlider.enabled = true;
			LevelFourSlider.enabled = true;
			LevelFiveSlider.enabled = true;
			LevelFourSlider.fillAmount = (levelFourDuration - levelFourTimeRemaining) / levelFourDuration;
		}

		// Caps Duration of Level Four
		if (levelFourTimeRemaining > levelFourDuration) 
		{
			levelFourTimeRemaining = levelFourDuration;
		}

		// Level Four Warning (shows faster text object)
		if (Level == 4 && levelFourTimeRemaining < LevelWarningTime) 
		{
			FasterLabel.SetActive (true);
		}
		
		if (levelFourTimeRemaining <= 0 && isLevelFour) 
		{
			levelFourTimeRemaining = levelFourDuration;
			Level += 1;
			isLevelOne = false;
			isLevelTwo = false;
			isLevelThree = false;
			FasterLabel.SetActive (false);
		}

		// Timer for level 5
		if (levelFiveTimeRemaining > 0 && isLevelFive)
		{
			//levelFiveTimeRemaining -= Time.unscaledDeltaTime;

			LevelOneSlider.enabled = false;
			LevelTwoSlider.enabled = false;
			LevelThreeSlider.enabled = false;
			LevelFourSlider.enabled = true;
			LevelFiveSlider.enabled = true;
			LevelFiveSlider.fillAmount = (levelFiveDuration - levelFiveTimeRemaining) / levelFiveDuration;
		}
		
		if (levelFiveTimeRemaining <= 0 && isLevelFive) 
		{
			levelFiveTimeRemaining = levelFiveDuration;
			//Level += 1;
			isLevelOne = false;
			isLevelTwo = false;
			isLevelThree = false;
			isLevelFour = false;
		}

		//___________________________________________________________________________________________

		// Level 1
		if (Level == 1) 
		{
			//PlayerMovementScript.Division = 2;
		}

		// Level 2
		if (Level == 2) 
		{
			TimeScaleNow = Time.timeScale;

			// Calculates distance between player and reference point
			distance = Vector3.Distance (Player.transform.position, gameObject.transform.position);

			// Sets timescale value to increase continuously
			TimeScale += Time.unscaledDeltaTime / dampenIncreaser;

			// Calculates final Time.timeScale
			Time.timeScale = (1 + TimeScale + (distance / Dampener)) - YTime;
		}

		// Level 3
		if (Level == 3) 
		{
			TimeScaleNow = Time.timeScale;
			
			// Calculates distance between player and reference point
			distance = Vector3.Distance (Player.transform.position, gameObject.transform.position);
			
			// Sets timescale value to increase continuously
			TimeScale += Time.unscaledDeltaTime / dampenIncreaser;
			
			// Calculates final Time.timeScale
			Time.timeScale = (1 + TimeScale + (distance / (Dampener - timeScaleLevelAdder))) - YTime;
		}

		// Level 4
		if (Level == 4) 
		{
			TimeScaleNow = Time.timeScale;
			
			// Calculates distance between player and reference point
			distance = Vector3.Distance (Player.transform.position, gameObject.transform.position);
			
			// Sets timescale value to increase continuously
			TimeScale += Time.unscaledDeltaTime / dampenIncreaser;
			
			// Calculates final Time.timeScale
			Time.timeScale = (1 + TimeScale + (distance / (Dampener - 2 * timeScaleLevelAdder))) - YTime;
		}

		// Level 5
		if (Level == 5) 
		{
			TimeScaleNow = 8;
			
			// Calculates distance between player and reference point
			distance = Vector3.Distance (Player.transform.position, gameObject.transform.position);
			
			// Sets timescale value to increase continuously
			TimeScale += Time.unscaledDeltaTime / dampenIncreaser;
			
			// Calculates final Time.timeScale
			Time.timeScale = (1 + TimeScale + (distance / (Dampener - 3 * timeScaleLevelAdder))) - YTime;
		}
	}
}

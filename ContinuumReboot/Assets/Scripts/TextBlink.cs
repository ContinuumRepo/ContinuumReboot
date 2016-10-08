using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour
{
	public Text blinkingText;
	public float blinkSpeed;

	private bool blink = false;
	
	// Update is called once per frame
	void Update ()
	{
		if (!blink)
			StartCoroutine (BlinkOff());
	}

	IEnumerator BlinkOff()
	{
		blink = true;
		blinkingText.enabled = false;
		yield return new WaitForSeconds (blinkSpeed);
		StartCoroutine (BlinkOn());
	}

	IEnumerator BlinkOn()
	{
		blinkingText.enabled = true;
		yield return new WaitForSeconds (blinkSpeed);
		blink = false;
	}
}

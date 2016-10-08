using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour
{
	public Text blinkingText;
	public float blinkSpeed;

	private bool blink = false;

	void OnEnable ()
	{
		StopCoroutine (BlinkOn());
		StopCoroutine (BlinkOff());
		blink = false;
	}
	
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
		yield return WaitForUnscaledSeconds (blinkSpeed);
		StartCoroutine (BlinkOn());
	}

	IEnumerator BlinkOn()
	{
		blinkingText.enabled = true;
		yield return WaitForUnscaledSeconds (blinkSpeed);
		blink = false;
	}

	IEnumerator WaitForUnscaledSeconds (float time)
	{
		float ttl = 0;
		while(time > ttl)
		{
			ttl += Time.unscaledDeltaTime;
			yield return null;
		}
	}
}

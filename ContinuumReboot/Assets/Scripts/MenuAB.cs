using UnityEngine;
using System.Collections;

public class MenuAB : MonoBehaviour
{
	public string prefsBackToName;
	public string currentPrefsName;
	public MenuBackButton backButtonScript;

	void Update ()
	{
		if (Input.GetAxis ("FireButton") > 0 || Input.GetAxis ("Submit") > 0)
		{
			// A has been pressed
		}
		else if (Input.GetAxis ("Cancel") > 0)
		{
			if ("credits" == currentPrefsName)
			{
				backButtonScript.BackFromCreditsButton();
			}
			else
			{
				backButtonScript.BackToMainButton();
			}
		}
	}
}

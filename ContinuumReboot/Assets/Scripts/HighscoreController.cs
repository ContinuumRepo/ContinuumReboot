using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighscoreController : MonoBehaviour
{
	public Text P1Name;
	public Text P1Score;

	public Text P2Name;
	public Text P2Score;

	public Text P3Name;
	public Text P3Score;

	public Text P4Name;
	public Text P4Score;

	public Text P5Name;
	public Text P5Score;

	void Start()
	{
		if( PlayerPrefs.HasKey( "Player1Name" ) )
		{
			P1Name.text = PlayerPrefs.GetString ("Player1Name");
			P1Score.text = PlayerPrefs.GetString ("Player1Score");

			if( PlayerPrefs.HasKey( "Player2Name" ) )
			{
				P2Name.text = PlayerPrefs.GetString ("Player2Name");
				P2Score.text = PlayerPrefs.GetString ("Player2Score");

				if( PlayerPrefs.HasKey( "Player3Name" ) )
				{
					P3Name.text = PlayerPrefs.GetString ("Player3Name");
					P3Score.text = PlayerPrefs.GetString ("Player3Score");

					if( PlayerPrefs.HasKey( "Player4Name" ) )
					{
						P4Name.text = PlayerPrefs.GetString ("Player4Name");
						P4Score.text = PlayerPrefs.GetString ("Player4Score");

						if( PlayerPrefs.HasKey( "Player5Name" ) )
						{
							P5Name.text = PlayerPrefs.GetString ("Player5Name");
							P5Score.text = PlayerPrefs.GetString ("Player5Score");
						}
					}
				}
			}
		}
	}
}


using UnityEngine;
using System.Collections;

public class ComboScaler : MonoBehaviour 
{
	private PlayerController playerControllerScript;
	public int combo;
	public float ampScale = 0.2f;
	public float addScale = 0.25f;

	[Header ("Explosion Text Animation Array")]
	public string[] ClipNames;

	void Start () 
	{
		FindPlayerControllerScript ();
		CheckCombo ();
		Scale ();
		Animate ();
	}

	void FindPlayerControllerScript ()
	{
		playerControllerScript = GameObject.Find ("Player").GetComponent <PlayerController> ();
	}

	void CheckCombo ()
	{
		combo = playerControllerScript.ComboN;
	}

	void Scale ()
	{
		GetComponent<RectTransform>().localScale = new Vector3 
			(
				(ampScale * playerControllerScript.ComboN) + addScale, 
				(ampScale * playerControllerScript.ComboN) + addScale, 
				1
			);
	}

	void Animate ()
	{
		GetComponentInChildren <Animator> ().Play (ClipNames[Random.Range (0, ClipNames.Length)]);
	}
}

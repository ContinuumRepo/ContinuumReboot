using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputScroll : MonoBehaviour
{
	public GameObject[] buttons;
	public float dead = 0.1f;

	private bool idxSet = false;
	private int buttonIndex;
	[SerializeField]
	private int indexLocation; //What button the player currently has selected
	private GameObject highlighted;

	// Use this for initialization
	void Start ()
	{
		buttonIndex = buttons.Length;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (buttonIndex > 0)
		{
			float value = Input.GetAxis ("Vertical");

			if (value > dead) // Select button above
			{
				if (idxSet == false) // When the player first inputs, set highlighted to be first button
				{
					InitHighlighted();
				}
			}
			else if (value < -dead) // Select button below
			{
				if (idxSet == false) // When the player first inputs, set highlighted to be first button
				{
					InitHighlighted();
				}
			}
		}
	}

	private void InitHighlighted()
	{
		indexLocation = 0;
		idxSet = true;
		buttons [indexLocation].GetComponent <Button>().Select();
		buttons [indexLocation].GetComponent <EventTrigger>().Invoke ("OnPointerEnter", 0);
	}
}

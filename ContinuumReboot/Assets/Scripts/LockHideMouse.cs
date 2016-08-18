using UnityEngine;
using System.Collections;

public class LockHideMouse : MonoBehaviour 
{
	public bool hideMouse;
	public enum lockmode
	{
		LockCursor, ConfineCursor, FreeCursor 
	}

	public lockmode CursorLockModeType;

	void Start () 
	{
		// Lock cursor
		if (CursorLockModeType == lockmode.LockCursor) 
		{
			Cursor.lockState = CursorLockMode.Locked;

			if (hideMouse) 
			{
				Cursor.visible = false;
			}

			if (!hideMouse) 
			{
				Cursor.visible = true;
			}
		}

		// Confine cursor (Free but only stays within the game's window)
		if (CursorLockModeType == lockmode.ConfineCursor) 
		{
			Cursor.lockState = CursorLockMode.Confined;

			if (hideMouse) 
			{
				Cursor.visible = false;
			}

			if (!hideMouse) 
			{
				Cursor.visible = true;
			}
		}

		// Free cursor
		if (CursorLockModeType == lockmode.FreeCursor) 
		{
			Cursor.lockState = CursorLockMode.None;

			if (hideMouse) 
			{
				Cursor.visible = false;
			}

			if (!hideMouse) 
			{
				Cursor.visible = true;
			}
		}
	}
}

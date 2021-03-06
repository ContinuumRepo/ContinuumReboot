﻿using UnityEngine;
using System.Collections;

public class BrickMovement : MonoBehaviour
{
	[Header ("Single-Column Brick")]
	public float moveSpeed;
	public float rotationSpeed;
	public float tweenRate = 0.1f; // The speed at which the brick will move between cells in the stack

	[Header ("Multi-Column Brick Group")]
	public bool isSourceBrick; // must be the leftmost brick
	public int groupWidth; // how many bricks wide the group is

	// Private Variables
	private int column = 0; // The column this brick is in
	private bool cellSet = false; // If the cube is stacked
	private int[] stackLocation = null; // This brick's location in the brick stack - column, row

	private float[] yPos;
	private float cellDist; // Distance between each cell center

	private BrickStackController stackCont;

	void Start ()
	{
		stackCont = GameObject.FindGameObjectWithTag ("BrickStackController").GetComponent <BrickStackController> ();
		cellDist = stackCont.GetCellDistance;

		Quaternion newRotation = Quaternion.Euler (45, 0, 45);
		this.transform.rotation = Quaternion.Slerp (this.transform.rotation, newRotation, rotationSpeed * Time.timeScale);

		int tempRows = stackCont.GetTotalRows;
		yPos = new float[tempRows];
		for (int i = 0; i < tempRows; i++) // Y-loc for each row
			yPos [i] = stackCont.GetBrickYPos (i);

		int tempColumns = stackCont.GetTotalColumns;
		for (int i = 0; i < tempColumns; i++) // X-loc for each column
			if (stackCont.GetBrickXPos (i) == this.transform.position.x)
			{
				SetColumn = i;
				break;
			}
	}

	void FixedUpdate ()
	{
		if (!cellSet)
		{
			transform.Translate ( 0, moveSpeed * Time.deltaTime, 0, Space.Self);
			transform.Rotate (0, rotationSpeed * Time.deltaTime, 0);
		}

		/*
		 * // This else statement shuffled the bricks down when those beneath were destroyed
		else
		{
			// If enemy is not in bottom cell and the cell below it is empty
			if ((stackLocation[1] != 0) && (!stackCont.CellOccupied (stackLocation[0], stackLocation[1] - 1)))
			{
				Y -= cellDist * (tweenRate * Time.timeScale); // Start moving brick down to the next cell
				if (Y <= (float) yPos[stackLocation[1]] - cellDist)
				{
					Y = (float) yPos[stackLocation[1]] - cellDist;
					stackCont.ResetCell (stackLocation[0], stackLocation[1]); // Set current cell to false
					stackLocation[1]--; // Decrement the row number to represent the new cell
					stackCont.SetBrick (stackLocation[0], stackLocation[1]); // Set new cell to true
				}
			}
		}*/
		
		if (gameObject.tag == "Cube")
			setStackPosition ();

		// **Possibly replace with script on boundary cube that destroys bricks on contact
		if (GetComponent<Rigidbody> ().position.y < -19) // Destroy brick if it passes past the bottom of the stack
			Destroy (gameObject);
	}

	void OnDestroy ()
	{
		if (cellSet)
		{
			stackCont.ResetCell (stackLocation[0], stackLocation[1]);
		}
	}

	public int SetColumn
	{
		set {column = value;}
	}

	public float Y
	{
		get {return GetComponent<Rigidbody>().position.y;}
		set {GetComponent<Rigidbody>().position = new Vector3 (GetComponent<Rigidbody>().position.x, value, GetComponent<Rigidbody>().position.z);}
	}
	/*
	public void ResetStack ()
	{
		if (stackLocation != null) 
		{
			stackCont.ResetCell (stackLocation[0], stackLocation[1]);
		}
	}*/

	private void setStackPosition ()
	{
		for (int i = yPos.Length-1; i >= 0; i--)
		{
			if (!cellSet)
			{
				// Set brick (in i+1) if cell below (i) is occupied and is not in the top row
				if ((stackCont.CellOccupied (column, i)) && (i != yPos.Length-1) && (Y <= (float) yPos[i+1]))
				{
					GetComponent<Rigidbody>().velocity = Vector3.zero;
					Quaternion newRotation = Quaternion.identity;
					this.transform.rotation = Quaternion.Slerp (this.transform.rotation, newRotation, rotationSpeed * Time.timeScale);
					Y = (float) yPos[i+1];
					stackLocation = new int[2] {column, i+1};
					stackCont.SetBrick (stackLocation[0], stackLocation[1]);
					cellSet = true;
				}
				// Destroy brick if cell below is occupied and is in the top row
				else if ((stackCont.CellOccupied (column, i)) && (i == yPos.Length-1) && (Y <= (float) yPos[i] + cellDist))
				{
					Destroy (gameObject);
				}
				// Set brick in bottom cell if it is not occupied
				else if ((!stackCont.CellOccupied (column, i)) && (i == 0) && (Y <= (float) yPos[0]))
				{
					GetComponent<Rigidbody>().velocity = Vector3.zero;
					Quaternion newRotation = Quaternion.identity;
					this.transform.rotation = Quaternion.Slerp (this.transform.rotation, newRotation, rotationSpeed * Time.timeScale);
					Y = (float) yPos[0];
					stackLocation = new int[2] {column, i};
					stackCont.SetBrick (stackLocation[0], stackLocation[1]);
					cellSet = true;
				}else cellSet = false;
			}
		}
	}
}

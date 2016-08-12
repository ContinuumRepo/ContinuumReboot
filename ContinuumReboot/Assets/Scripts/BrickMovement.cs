using UnityEngine;
using System.Collections;

public class BrickMovement : MonoBehaviour
{
	public float moveSpeed;
	public float rotationSpeed;
	public float tweenRate = 0.1f; // The speed at which the brick will move between cells in the stack

	private int column = 0; // The column this brick is in
	private bool cellSet = false; // If the cube is stacked
	private int[] stackLocation = null; // This brick's location in the brick stack - column, row

	private float[] yPos;
	private float cellDist; // Distance between each cell center

	private BrickStackController stackCont;

	void Start ()
	{
		stackCont = GameObject.FindGameObjectWithTag ("Brick Stack Controller").GetComponent <BrickStackController> ();
		cellDist = stackCont.GetCellDistance;

		Quaternion newRotation = Quaternion.Euler (45, 0, 45);
		this.transform.rotation = Quaternion.Slerp (this.transform.rotation, newRotation, rotationSpeed * Time.timeScale);
		yPos = stackCont.GetBrickYPos; // Y-loc for each row
	}

	void FixedUpdate ()
	{
		if (!cellSet)
		{
			this.GetComponent<Rigidbody> ().velocity = transform.up * (moveSpeed * Time.timeScale);
			this.transform.Rotate (0, rotationSpeed * Time.timeScale, 0);
		}
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
		}
		
		if (gameObject.tag == "Cube")
			setStackPosition ();

		// **Possibly replace with script on boundary cube that destroys bricks on contact
		if (GetComponent<Rigidbody> ().position.y < -19) // Destroy brick if it passes past the bottom of the stack
			Destroy (gameObject);
	}

	void OnDestroy ()
	{
		if (cellSet)
			stackCont.ResetCell (stackLocation[0], stackLocation[1]);
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

	void setStackPosition ()
	{
		for (int i = yPos.Length-1; i >= 0; i--)
		{
			if (!cellSet)
			{
				// Set brick (in i+1) if cell below (i) is occupied and is not in the top row
				if ((stackCont.CellOccupied (i, column)) && (i != yPos.Length-1) && (Y <= (float) yPos[i+1]))
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
				else if ((stackCont.CellOccupied (i, column)) && (i == yPos.Length-1) && (Y <= (float) yPos[i] + cellDist))
				{
					Destroy (gameObject);
				}
				// Set brick in bottom cell if it is not occupied
				else if ((!stackCont.CellOccupied (i, column)) && (i == 0) && (Y <= (float) yPos[0]))
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

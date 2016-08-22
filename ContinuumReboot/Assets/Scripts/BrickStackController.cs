using UnityEngine;
using System.Collections;

public class BrickStackController : MonoBehaviour
{
	public int totalBrickColumns;
	public int totalBrickRows;
	public int brickWidth = 2;
	public int gameAreaWidth = 70;
	public int gameAreaHeight = 40;

	private bool[,] brickArr;
	private float [] brickXpos; // Actual locations of each column in world space
	private float [] brickYpos; // Actual locations of each row in world space

	void Awake()
	{
		// Initialise brick stack (column, row) with each cell unoccupied (false)
		brickArr = new bool [totalBrickColumns, totalBrickRows];
		for (int i = 0; i < totalBrickColumns; i++)
			for (int j = 0; j < totalBrickRows; j++)
				brickArr [i, j] = false;
		
		CalcBrickXPos ();
		CalcBrickYPos ();
	}

	public int GetTotalColumns
	{
		get {return totalBrickColumns;}
	}

	public int GetTotalRows
	{
		get {return totalBrickRows;}
	}

	public float GetCellDistance
	{
		get {return (brickWidth * 1.5f);} // 1.5 makes up the width of a brick + the dist between two (half a brick width)
	}

	public float GetBrickXPos (int index)
	{
		return brickXpos [index];
	}

	public float GetBrickYPos (int index)
	{
		return brickYpos [index];
	}

	public bool CellOccupied (int column, int row)
	{
		return brickArr [column, row];
	}

	public void ResetCell (int column, int row)
	{
		brickArr [column, row] = false;
	}

	public void SetBrick (int column, int row)
	{
		brickArr [column, row] = true;
	}

	// Find the actual locations of each column in world space
	// This depends on the amount of columns, the width of the bricks, and the width of the game area
	private void CalcBrickXPos ()
	{
		bool brickColumnsFit = false;
		int stackWidth = 0;
		do
		{
			stackWidth = (totalBrickColumns * brickWidth) + ((totalBrickColumns + 1) * (brickWidth / 2));

			if (stackWidth <= gameAreaWidth) // Check that stack of bricks will fit inside the game area horizontally
				brickColumnsFit = true;
			else
				totalBrickColumns--; // Reduce the number of brick columns
		} while (!brickColumnsFit);

		float margin = (gameAreaWidth % stackWidth) / 2; // Extra space bounding brick stacking area
		float startLoc = -(gameAreaWidth/2) + margin - (brickWidth/2);

		brickXpos = new float[totalBrickColumns];
		for (int i = 0; i < totalBrickColumns; i++)
		{
			brickXpos[i] = startLoc + ((brickWidth * 1.5f) * (i + 1));
		}
	}

	// Find the actual locations of each row in world space
	// This depends on the amount of rows, the height of the bricks, and the height of the game area
	// This is bottom to top - brickYpos[0] will be at the bottom
	private void CalcBrickYPos ()
	{
		bool brickRowsFit = false;
		int stackHeight = 0;
		do
		{
			stackHeight = (totalBrickRows * brickWidth) + ((totalBrickRows + 1) * (brickWidth / 2));

			if (stackHeight <= gameAreaHeight) // Check that stack of bricks will fit inside the game area vertically
				brickRowsFit = true;
			else
				totalBrickRows--; // Reduce the number of brick rows
		} while (!brickRowsFit);

		float startLoc = -(gameAreaHeight/2) - (brickWidth/2);

		brickYpos = new float[totalBrickRows];
		for (int i = 0; i < totalBrickRows; i++)
		{
			brickYpos[i] = startLoc + ((brickWidth * 1.5f) * (i + 1));
		}
	}
}

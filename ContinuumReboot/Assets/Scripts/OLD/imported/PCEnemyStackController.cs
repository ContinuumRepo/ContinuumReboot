using UnityEngine;
using System.Collections;

public class PCEnemyStackController : MonoBehaviour
{
	private bool[,] enemyArr = new bool[13,20];
	
	void Start()
	{
		for (int i = 0; i < 13; i++)
			for (int j = 0; j < 20; j++)
				enemyArr [i, j] = false;
	}
	
	public bool CellOccupied (int row, int column)
	{
		return enemyArr [row, column];
	}

	public void ResetCell (int row, int column)
	{
		enemyArr [row, column] = false;
	}

	public void SetEnemy (int row, int column)
	{
		enemyArr [row, column] = true;
	}
}


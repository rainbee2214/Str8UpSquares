using UnityEngine;
using System.Collections;

public class GridEnemy : MonoBehaviour
{
	public GameObject enemyGrid;
	public GameObject[][] enemy;

	public int width, height;

	void Start () 
	{

		enemy = new GameObject[width][];
		for (int i = 0; i < width; i++)
		{
			enemy[i] = new GameObject[height];
		}

		float xF, yF;
		for (int x = 0; x < width; x++)
		{
			xF = x;
			for (int y = 0; y <height; y++)
			{
				yF = y;
				enemy[x][y] = Instantiate(enemyGrid, new Vector2((xF/10)*2, (yF/10)*2), Quaternion.identity) as GameObject;
			}
		}
	}
	

	void Update () 
	{
	
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Generator : MonoBehaviour 
{
	public List<Vector2> currentPositions; // Holds the current positions of all grid squares 
	//Enemies and loot can only be generated on top of an existing grid square

	void Start () 
	{

	}
	
	void Update () 
	{	

	}
	
	void GenerateGrid (int width, int height, Vector2 startingPosition)
	{
		
//		for (int y = 0; y < height; y++)
//		{
//			for (int x = 0; x < width; x++)
//			{
//				grid[y*width + x] = Instantiate (gridSquare, new Vector2(0f,0f), Quaternion.identity) as GameObject;
//				grid[y*width + x].gameObject.GetComponent<GridSquareController>().currentColour = colour;
//				
//				grid[y*width + x].name = ("Grid Square" + ": (" + x + "," + y + ")");
//				grid[y*width + x].gameObject.transform.position = new Vector2((width-x) + offset.x,(height-y) + offset.y);
//			}
//		}
	}

	void GenerateEnemies()
	{

	}

	void GenerateLoot()
	{

	}
}

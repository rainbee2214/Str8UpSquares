using UnityEngine;
using System.Collections;

public class GridGenerator : MonoBehaviour 
{
	public static GridGenerator gridGenerator;


	#region Properties

	private int size = 1;
	public int Size
	{
		get{return size;}
		set{size = value;}
	}
	#endregion
	
	public GameObject wall;
	public GameObject[] walls;

	public GameObject gridSquare;
	public GameObject[][] grid;
	
	public const int startingWidth = 5;
	public const int startingHeight = startingWidth;
	
	public int currentWidth;
	public int currentHeight;
	public int currentColour;
	
	public bool threshholdReached = false;
	
	private int threshhold = 100;
	private GameObject temp;
	private int startingColour;
	
	public int tempScore = 1;
	
	void Start () 
	{
		walls = new GameObject[4];
		//currentWidth = startingWidth;
		//currentHeight = startingHeight;
		startingColour = Random.Range(0,7);
		currentColour = startingColour;
		generateGrid(currentWidth,currentHeight, currentColour);
	}

	void Update () 
	{	
		tempScore++;
		//if (tempScore > threshhold) threshholdReached = true;
		if (threshholdReached)
		{
			deleteGrid();
			currentWidth+=2;
			currentHeight+=2;
			currentColour++;
			if (currentColour == 7) currentColour = 0;
			generateGrid(currentWidth,currentHeight, currentColour);
			threshholdReached = false;
			threshhold *= 2;
		}
		Size = currentWidth;
	}
	
	int timesGenerated = 0;
	
	void deleteGrid ()
	{
		for (int x = 0; x < currentWidth; x++)
		{
			for (int y = 0; y < currentHeight; y++)
			{
				Destroy(grid[x][y].gameObject);
			}
		}
		for (int i = 0; i < 4; i++)
		{
			Destroy(walls[i].gameObject);
		}
	}
	
	void generateGrid (int width, int height, int colour)
	{
		grid = new GameObject[width][];
		timesGenerated++;
		for (int i = 0; i < width; i++)
		{
			grid[i] = new GameObject[height];
		}
		
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				// Fills in grid array with gridSquares, and sets them to the starting colour
				grid[x][y] = Instantiate (gridSquare, new Vector2(0f,0f), Quaternion.identity) as GameObject;
				grid[x][y].gameObject.GetComponent<GridSquareController>().currentColour = colour;
				
				// Sets all grid squares to their proper locations
				grid[x][y].name = ("Grid Square" + timesGenerated +": (" + x + "," + y + ")");
				grid[x][y].gameObject.transform.position = new Vector2(x,y);

				
			}
		}

		//Walls
		Vector2 wallTemp = new Vector2(0f,0f);
		for (int i = 0; i < 4; i++)
		{
			walls[i] = Instantiate (wall, wallTemp, Quaternion.identity) as GameObject;
			walls[i].name = ("Wall " + i);
		}

		//Top, bottom, left, right
		walls[0].transform.localScale = new Vector3(width, 1, 1);
		walls[1].transform.localScale = new Vector3(width, 1, 1);
		walls[2].transform.localScale = new Vector3(1, height, 1);
		walls[3].transform.localScale = new Vector3(1, height, 1);

		walls[0].transform.position = new Vector2((width - 1)/ 2,height - 1);			// Top
		walls[1].transform.position = new Vector2((width - 1)/ 2,0);				// Bottom
		walls[2].transform.position = new Vector2(0,(height - 1) / 2);  			// Left
		walls[3].transform.position = new Vector2(width - 10,(height - 1) / 2);			// Right


	}
	
	void Awake () 
	{
		//if control is not set, set it to this one and allow it to persist
		if (gridGenerator == null)
		{
			DontDestroyOnLoad(gameObject);
			gridGenerator = this;
		}
		//else if control exists and it isn't this instance, destroy this instance
		else if(gridGenerator != this)
		{
			Debug.Log ("Game control already exists, deleting this new one");
			Destroy (gameObject);
		}
	}
}

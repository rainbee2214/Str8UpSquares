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
		currentWidth = startingWidth;
		currentHeight = startingHeight;
		startingColour = Random.Range(0,7);
		currentColour = startingColour;
		generateGrid(currentWidth,currentHeight, currentColour);
	}

	void Update () 
	{	
		tempScore++;
		if (tempScore > threshhold) threshholdReached = true;
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

using UnityEngine;
using System.Collections;

public class GridSquareController : MonoBehaviour 
{
	public Color[] colours;
	public int currentColour;
	public int pointsPerFlip = 1;

	void Start () 
	{
		renderer.material.color = colours[currentColour];
	}

	void Update () 
	{
	
	}

	void OnTriggerEnter2D (Collider2D other) 
	{
		if (other.tag != "Background")
		{
			GameController.controller.Score = pointsPerFlip; //Increment score
			GameController.controller.TotalFlips = 1;
			currentColour += 1;
			if (currentColour == 16) currentColour = 0;
			renderer.material.color = colours[currentColour];
		}

	}
}

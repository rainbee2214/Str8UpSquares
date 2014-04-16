using UnityEngine;
using System.Collections;

public class GridSquareController : MonoBehaviour 
{
	public Color[] colours;
	public int currentColour;

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
			currentColour += 1;
			if (currentColour == 8) currentColour = 0;
			renderer.material.color = colours[currentColour];
		}

	}
}

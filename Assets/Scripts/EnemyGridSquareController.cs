using UnityEngine;
using System.Collections;

public class EnemyGridSquareController : MonoBehaviour {

	public Color[] colours;
	public int currentColour;
	public int target;
	void Start () 
	{
		
		currentColour = 3;
		target = 2;
	}
	
	void Update () 
	{
		renderer.material.color = colours[currentColour];
	}
	
	private bool targetReached = false;
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Missile")
		{
			if (currentColour != target)
			{
				currentColour += 1;
				if (currentColour == 8) currentColour = 0;
				renderer.material.color = colours[currentColour];
				
			}
			
			if (currentColour == target) 
			{
				
				if (!targetReached)
				{
					Debug.Log("target");
					other.gameObject.collider2D.enabled = false;
					Destroy(other.gameObject);
					targetReached = true;
				}
				
			}
			
		}
	}
}

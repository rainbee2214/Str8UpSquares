using UnityEngine;
using System.Collections;

public class RainbowController : MonoBehaviour 
{
	public GameObject missile;
	public GameObject[] shots;
	public int coloursAmount = 8;
	public int speed = 12;
	public int delay = 5;

	private bool done = false;
	private Vector2 direction;
	
	void Update () 
	{
		if (Time.time > delay && !done)
		{
			done = true;
			shots = new GameObject[coloursAmount];
			direction = transform.position;
			
			for (int i = 0; i < coloursAmount; i++)
			{
				direction.x -= 1.1f;
				shots[i] = Instantiate(missile, direction, Quaternion.identity) as GameObject;
				shots[i].rigidbody2D.velocity = new Vector2(speed,0f);
			}
		}

	}

}

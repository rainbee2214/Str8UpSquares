using UnityEngine;
using System.Collections;

public class test : MonoBehaviour 
{
	public GameObject missile;
	public GameObject[] shots;

	public int coloursAmount = 8;

	private Vector2 direction;
	public int speed = 12;

	void Start () 
	{
		shots = new GameObject[coloursAmount];
		direction = transform.position;


		for (int i = 0; i < coloursAmount; i++)
		{
			direction.x -= 1.5f;
			shots[i] = Instantiate(missile, direction, Quaternion.identity) as GameObject;
			shots[i].rigidbody2D.velocity = new Vector2(speed,0f);
		}


	}
	
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour 
{
	private Vector2 direction;
	private int count;

	public int speed;
	public int delay;

	void Start () 
	{
		direction = new Vector2(0f,0f);
	}
	

	void Update () 
	{
		if (count % delay == 0)
		{
			// Change enemy direction
			int temp = Random.Range(0,7);
			switch(temp)
			{
			case 0:
				direction.x = speed;
				direction.y = 0f;
				break;
			case 1:
				direction.x = 0f;
				direction.y = speed;
				break;
			case 2:
				direction.x = -speed;
				direction.y = 0f;
				break;
			case 3:
				direction.x = 0f;
				direction.y = -speed;
				break;
			case 4:
				direction.x = speed;
				break;
			case 5:
				direction.x = -speed;
				break;
			case 6:
				direction.y = speed;
				break;
			case 7:
				direction.y = -speed;
				break;

			}
			rigidbody2D.velocity = direction;

		}
		count++;
	}
}

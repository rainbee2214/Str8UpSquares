using UnityEngine;
using System.Collections;

public class PositionScore : MonoBehaviour 
{
	public GameObject player;
	void Start () 
	{
		/*//Vector3 position = player.transform.position;
		*///position.x -= 10;
		//position.y += 10;
		//gameObject.transform.position = position;
	}

	Vector3 lastPosition = new Vector3();
	Vector3 position = new Vector3(1,1,1);
	void Update () 
	{
			position = player.transform.position;
			position.x -= 12;
			position.y += 11;
			gameObject.transform.position = position;

	}
}

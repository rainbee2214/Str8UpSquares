using UnityEngine;
using System.Collections;

public class PositionScore : MonoBehaviour 
{
	public GameObject player;
	void Start () 
	{

	}

	Vector3 lastPosition = new Vector3();
	Vector3 position = new Vector3(1,1,1);
	void Update () 
	{
		position = player.transform.position;
		position.x += Camera.main.orthographicSize - 45;
		position.y += Camera.main.orthographicSize - 2;
		gameObject.transform.position = position;
	}
}

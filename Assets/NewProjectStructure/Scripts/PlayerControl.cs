using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour 
{
	public Vector2 position;
	private float speed;

	void Start()
	{
		speed = GameControl.controller.Speed;
	}
	
	void Update()
	{
		if (speed != GameControl.controller.Speed) speed = GameControl.controller.Speed;
		speed /= 100f;
		if (Input.GetButton("w"))
		{
			position.y += speed;
		}
		if (Input.GetButton("s"))
		{
			position.y -= speed;
		}
		if (Input.GetButton("a"))
		{
			position.x -= speed;
		}
		if (Input.GetButton("d"))
		{
			position.x += speed;
		}

		transform.position = position;
		speed *= 100f;
	}
}

using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// This script will be used to control a PlayerUI Component. A PlayerUI Component
// has the same abilities as the player in terms of manipulating colours, however, 
// when it shoots Missiles nothing is added to the score, so it doesn't affect the 
// game in that way :)
public class PlayerUIController : MonoBehaviour 
{
	//Time Variables
	private float nextFireTime; 		//Stores time player can shoot next
	//Weapons
	public GameObject missile; 			//Prefab of missile to shoot
	public List<GameObject> missiles; 	//List of shot missiles
	public int currentMissileAmount; 	//Amount of missiles currently loaded
	public int missileReloads; 			//Clips remaining
	public int missileClipSize = 25; 	//Clip size
	public float fireRate = 0.12f; 		//Fire speed
	public float missileSpeed = 12; 	//Missile speed

	public Vector3 desiredPosition;
	public string direction;
	int currentDirection; // 0 = up, 1 = right, 2 = down, 3 = left
	void Start () 
	{
		switch (direction)
		{
		case "up":
		case "u":
		case "U":
			currentDirection = 0;
			break;
		case "right":
		case "r":
		case "R":
			currentDirection = 1;
			break;
		case "down":
		case "d":
		case "D":
			currentDirection = 2;
			break;
		case "left":
		case "l":
		case "L":
			currentDirection = 3;
			break;
		default:
			break;
		}
	}

	void Update () 
	{
		if (Time.time > nextFireTime)
		{
			Shoot();
			nextFireTime = Time.time + fireRate;
		}
	}

	// Add object pooling. Easy to do, but I don't feel like doing it right now
	void Shoot()
	{
		//Instantiate the missile, save the reference, name it
		GameObject temp = Instantiate (missile, transform.position, Quaternion.identity) as GameObject;
		temp.name = ("Missile:"+missileReloads+currentMissileAmount);
		
		//Movement Variables
		Vector2 missileDirection = new Vector2(0f,0f);
		
		//Missile launching
		//Ouya	

		switch(currentDirection)
		{
		case 0:
			missileDirection.y = 1;
			break;
		case 1:
			missileDirection.x = 1;
			break;
		case 2:
			missileDirection.y = -1;
			break;
		case 3:
			missileDirection.x = -1;
			break;
		}

		
		
		//Apply direction * speed, save reference, update ammo count
		temp.rigidbody2D.velocity = missileDirection * missileSpeed;
		missiles.Add(temp);
		currentMissileAmount--;
	}


}

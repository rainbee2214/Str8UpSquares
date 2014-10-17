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

	//Missile Pool
	int poolSize = 10;
	List<GameObject> missilePool;
	List<GameObject> currentMissiles;

	public Vector3 desiredPosition;
	public string direction;
	int currentDirection; // 0 = up, 1 = right, 2 = down, 3 = left
	void Start () 
	{
		missilePool = new List<GameObject>();
		currentMissiles = new List<GameObject>();
		//Create missilePool;
		for(int i = 0; i < poolSize; i++)
		{
			GameObject temp = Instantiate (missile) as GameObject;
			missilePool.Add(temp);
		}

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
			if (missilePool.Count != 0)
			{
				Shoot();
				nextFireTime = Time.time + fireRate;
			}
		}

		// When a missile is past the grid, it should be removed from current and added back into the pool
		// Check is missile is touching grid, if not, add it to pool
		for (int i = 0; i < currentMissiles.Count; i++)
		{
			if(currentMissiles[i])
			{
				
			}
		}
	}

	// Add object pooling. Easy to do, but I don't feel like doing it right now
	void Shoot()
	{		
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

		// Get a missile from the pool
		GameObject missile = missilePool[0];
		missilePool.RemoveAt(0);
		currentMissiles.Add(missile);
		// Change position of a missile in the pool

		//Apply direction * speed, save reference, update ammo count
		missile.rigidbody2D.velocity = missileDirection * missileSpeed;;
	}

	void OnTriggerEnter2D (Collider2D other) 
	{
		Debug.Log("Trigger!" + this.name);
		
	}
}

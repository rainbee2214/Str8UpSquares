using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissileAttackController : MonoBehaviour 
{
	public GameObject missile;
	public OuyaPlayer playerOuya;

	public List<GameObject> missiles;

	public int currentMissileAmount = 10;
	public int missileReloads = 3;

	void Start () 
	{
		missiles = new List<GameObject>();
	}

	void Update () 
	{
		if ( Input.GetButtonDown("R") && ( Input.GetButton("I") || Input.GetButton("K") || Input.GetButton("J") || Input.GetButton("L") ) )
		{
			if (currentMissileAmount == 0 && missileReloads == 0)
			{
				Debug.Log("No ammo");
			}
			else
			{
				if (currentMissileAmount == 0) 
				{
					Debug.Log("Clip emptied.");
					reload();
				}
				shoot();
			}
		}
	}

	void reload()
	{
//		for (int i = 0; i < missiles.Count; i++)
//		{
//			Destroy(missiles[i]);
//		}
		missiles.Clear();
		missileReloads--;
		currentMissileAmount = 10;
	}

	void shoot()
	{

		GameObject temp = Instantiate (missile, transform.position, Quaternion.identity) as GameObject;
		temp.name = ("Missile:"+missileReloads+currentMissileAmount);

		Vector2 missileDirection = new Vector2(0f,0f);
		int missileSpeed = 6;

		if (Input.GetButton("I"))
		{
			missileDirection.y = missileSpeed;
			temp.rigidbody2D.velocity = missileDirection;
		}
		if (Input.GetButton("K"))
		{
			missileDirection.y = -missileSpeed;
			temp.rigidbody2D.velocity = missileDirection;
		}
		if (Input.GetButton("J"))
		{
			missileDirection.x = -missileSpeed;
			temp.rigidbody2D.velocity = missileDirection;
		}
		if (Input.GetButton("L"))
		{
			missileDirection.x = missileSpeed;
			temp.rigidbody2D.velocity = missileDirection;
		}

		missiles.Add(temp);
		currentMissileAmount--;
	}
}

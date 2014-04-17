using UnityEngine;
using System.Collections;

public class WallDestroyer : MonoBehaviour 
{
	void OnCollisionEnter2D(Collision2D other)
	{

		if (other.gameObject.tag == "Missile") 
			Destroy(other.gameObject);
	}
}

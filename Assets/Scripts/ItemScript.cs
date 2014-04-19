using UnityEngine;
using System.Collections;

public class ItemScript : MonoBehaviour 
{
	public int itemType;

	void Start () 
	{
		itemType = Random.Range(0,6);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			other.GetComponent<PlayerController>().ApplyPowerUp(itemType);
			Destroy(gameObject);
		}
	}
}

using UnityEngine;
using System.Collections;

public class WarpControl : MonoBehaviour 
{
	public bool oneWay = true;
	public GameObject warpPartner;

	public Vector2 currentPosition;
	public GameObject player;
	bool warpActive;

	void Start()
	{
		transform.position = currentPosition;
		if (warpPartner == null)
		{
			oneWay = true;
			warpPartner = gameObject;
		}
	}

	void Update()
	{
		if (transform.position.x != currentPosition.x || transform.position.y != currentPosition.y) 
			transform.position = currentPosition;
		if ((Input.GetButtonDown("Warp")) && warpActive)
		{
			Debug.Log("Warp");
			player.gameObject.GetComponent<PlayerControl>().position  = warpPartner.transform.position;
			//warpActive = false;
			//player.transform.position = warpPartner.transform.position;
		}
	}

//	void OnTriggerStay(Collider other)
//	{
//		if (other.tag == "Player") 
//		{
//			warpActive = true;
//
//		}
//	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			
			Debug.Log("Yes.");
			warpActive = true;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			warpActive = false;
		}
	}

}

using UnityEngine;
using System.Collections;

public class WarpControl : MonoBehaviour 
{
	public bool oneWay = true;
	public GameObject warpPartner;

	public Vector2 currentPosition;


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
	}

	void OnTriggerStay(Collider other)
	{
		Debug.Log(other.tag);
		if (other.tag == "Player") Debug.Log("Yes.");
		if (Input.GetButtonDown("Warp"))// && other.tag == "Player")
		{
			Debug.Log("Warp");
			other.transform.position = warpPartner.transform.position;
		}
	}

}

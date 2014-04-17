using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour 
{

	public GameObject player;

	void Start () 
	{
	
	}

	void Update () 
	{
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Wall") 
		{
			Debug.Log("Wall");
		}
		else
			other.renderer.enabled = true;

	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Wall") 
		{
			Debug.Log("Wall2");
		}
		else
			other.renderer.enabled = false;
		if (other.tag == "Missile") Destroy(other.gameObject);
	}

	void OnTriggerStay2D(Collider2D other)
	{

	}

	void OnCollisionEnter2D(Collision2D other)
	{

	}
	
	void OnCollisionExit2D(Collision2D other)
	{

	}
}

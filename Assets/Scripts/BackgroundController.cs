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
		other.renderer.enabled = true;
		if(other.tag != "Player")Debug.Log("Collision1.");
	}

	void OnTriggerExit2D(Collider2D other)
	{
		Debug.Log("Collision2.");
		other.renderer.enabled = false;
		if (other.tag == "Missile") Destroy(other.gameObject);
	}

	void OnTriggerStay2D(Collider2D other)
	{
		//Debug.Log("Inside background.");
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log("Collision3.");
	}
	
	void OnCollisionExit2D(Collision2D other)
	{
		Debug.Log("Collision4.");
	}
}

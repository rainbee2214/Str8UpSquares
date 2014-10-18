using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour 
{
	Vector3 position;
	public GameObject player;
	void Start () 
	{
	
	}

	void Update () 
	{
		position = player.transform.position;
		gameObject.transform.position = position;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag != "Wall" && other.tag != "GridEnemy" && other.tag != "MissileUI"  && other.tag != "PlayerUI") 
			other.renderer.enabled = true;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag != "Wall" && other.tag != "GridEnemy" && other.tag != "MissileUI" && other.tag != "PlayerUI") 
			other.renderer.enabled = false;
		if (other.tag == "Missile" || other.tag == "MissileUI") Destroy(other.gameObject);
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

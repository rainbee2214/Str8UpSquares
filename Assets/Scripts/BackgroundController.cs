using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour 
{

	void Start () 
	{
	
	}

	void Update () 
	{
		//int size = 0;
		//size = gridGenerator.gameObject.GetComponent<GridGenerator>().currentWidth;
		//gameObject.transform.localScale = new Vector3(size,size,1);
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

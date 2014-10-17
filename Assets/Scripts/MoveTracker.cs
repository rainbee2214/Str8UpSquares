using UnityEngine;
using System.Collections;

public class MoveTracker : MonoBehaviour 
{

	bool move = true;
	void Start () 
	{
	
	}

	void Update () 
	{
		if(move)
		{
			move = false;
			Vector3 position = new Vector3(15f, 16.75f, 1f);
			gameObject.transform.position = position;
		}
	}
}

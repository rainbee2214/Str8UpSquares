using UnityEngine;
using System.Collections;

public class PointLightScript : MonoBehaviour 
{
	public float size;
	public int delay = 3;
	
	private bool done = false;
	void Start()
	{
		gameObject.light.range = size;
	}
	void Update() 
	{
		if (Time.time > delay && !done)
		{
			size--;
			if (size < 0) size = 0;
			gameObject.light.range = size;
		}

	}

}

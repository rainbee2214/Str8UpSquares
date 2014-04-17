using UnityEngine;
using System.Collections;

public class TitleLightController : MonoBehaviour {

	public static Transform from;
	public Transform fromLeft, fromRight, fromTop, fromBottom;
	public Transform to;
	public float speed;
	// Use this for initialization
	void Start () 
	{
		int random = Random.Range(1,4);
		switch(random)
		{
		case 1: from = fromLeft; break;
		case 2: from = fromRight; break;
		case 3: from = fromTop; break;
		case 4: from = fromBottom; break;
		default: break;
		}


	}

	// Update is called once per frame
	void Update () 
	{
		transform.rotation =
			Quaternion.Lerp (from.rotation, to.rotation, Time.time * speed);

		//Debug.Log (transform.rotation == to.rotation);

		if (transform.rotation == to.rotation)
		{
			light.spotAngle += Time.deltaTime * 300;
			if (light.spotAngle >= 200) light.spotAngle = 0;
		}
	}
}


// Interpolates rotation between the rotations
// of from and to.
// (Choose from and to not to be the same as 
// the object you attach this script to)

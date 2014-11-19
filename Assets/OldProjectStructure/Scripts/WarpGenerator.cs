using UnityEngine;
using System.Collections;

public class WarpGenerator : MonoBehaviour {

	public GameObject[] warps;
	public GameObject warp;
	// Use this for initialization
	void Start () 
	{
		warps = new GameObject[Random.Range(4,10)];
		for (int i = 0; i < warps.Length; i++)
		{
			warps[i] = Instantiate (warp, new Vector2((Random.Range(-10,10)),(Random.Range(-10,10))), Quaternion.identity) as GameObject;
			warps[i].name = ("Warp" + i); 
		}
		for (int i = 0; i < warps.Length; i++)
		{
			if (i == warps.Length - 1) 
				warps[i].gameObject.GetComponent<WarpController>().warpPartner = warps[0];
			else	
				warps[i].gameObject.GetComponent<WarpController>().warpPartner = warps[i+1];
		}

		warp.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

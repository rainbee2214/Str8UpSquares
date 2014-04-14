﻿using UnityEngine;
using System.Collections;

public class TitleScreenDemo : MonoBehaviour {
	public GUIText helloWorld, downText,upText;//, rightText, leftText;

	void Start () 
	{
	
	}

	void Update () 
	{
		if (Input.GetButton("S"))
		{
			helloWorld.gameObject.transform.position = new Vector2(0.5f,0.8f);
			downText.active = true;
			downText.gameObject.transform.position = new Vector2(0.5f,0.5f);
		}
		else if (Input.GetButtonUp("S"))
		{
			helloWorld.gameObject.transform.position = new Vector2(0.5f,0.5f);
			downText.active = false;
		}
		else if (Input.GetButton("W"))
		{
			helloWorld.gameObject.transform.position = new Vector2(0.5f,0.1f);
			upText.active = true;
			upText.gameObject.transform.position = new Vector2(0.5f,0.5f);
		}
		else
		{
			helloWorld.gameObject.transform.position = new Vector2(0.5f,0.5f);
			upText.active = false;
		}
	
	}
}

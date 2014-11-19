using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameControl : MonoBehaviour 
{
	public static GameControl controller;
	public List<Color> AllGameColors; //All the game colors used in the entire game // 16 at most for now

	private int winning_score = 100;

	#region Properties
	private int score;
	public int Score
	{
		get{return score;}
		set{score += value;}
	}

	private float speed = 1f;
	public float Speed
	{
		get{return speed;}
		set{speed += value;}
	}

	private bool playerDead = false;
	public bool PlayerDead
	{
		get{return playerDead;}
		set{playerDead = value;}
	}

	#endregion
	
	void Awake () 
	{
		//if control is not set, set it to this one and allow it to persist
		if (controller == null)
		{
			DontDestroyOnLoad(gameObject);
			controller = this;
		}
		//else if control exists and it isn't this instance, destroy this instance
		else if(controller != this)
		{
			Debug.Log ("Game control already exists, deleting this new one");
			Destroy (gameObject);
		}
	}
	
	void Start()
	{
		playerDead = false;
	}
	
	void Update()
	{
		if (playerDead == true)
		{
			Debug.Log("Player died.");
		}

		if (score > winning_score)
		{
			Debug.Log("Player wins.");
		}
	}

	
	public List<Color> SetColors()
	{
		//Different levels will have different subsets of colours used
		return AllGameColors;
	}

}

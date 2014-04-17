using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour 
{
	//Static reference to the GameController class
	public static GameController controller;
	
	//GUI References
	public GUIText scoreText;
	public GUIText targetColorText;
	
	//Game Stats		
	public bool typeMouseClick;
	public bool typeMouseOver;

	//Controller reference
	public OuyaSDK.OuyaPlayer player = OuyaSDK.OuyaPlayer.player1;
	
	#region Properties
	private int score;
	public int Score
	{
		get{return score;}
		set{score += value;}
	}
	
	private int targetColor = 0;
	public int TargetColor
	{
		get{return targetColor;}
		set{targetColor = value;}
	}
	
	private int totalFlips = 0;
	public int TotalFlips
	{
		get{return totalFlips;}
		set{totalFlips += value;}
	}
	
	private bool playerDead = false;
	public bool PlayerDead
	{
		get{return playerDead;}
		set{playerDead = value;}
	}
	
	private bool invincibilityOn = false;
	public bool InvincibilityOn
	{
		get{return invincibilityOn;}
		set{invincibilityOn = value;}
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
		targetColor = UnityEngine.Random.Range(0, 6);
	}
	
	void Update()
	{
		scoreText.text = "Score: " + score;

	}
	
	void OnGUI()
	{
		if (playerDead)
		{
			//Set the Size of the centered popoup here
			int groupWidth = 250;
			int groupHeight = 100;
			int groupX = (Screen.width - groupWidth) / 2;
			int groupY = (Screen.height - groupWidth) /2;
			// Make a group on the center of the screen
			GUI.BeginGroup (new Rect (groupX, groupY, groupWidth, groupHeight));
			GUI.Box (new Rect (0, 0, groupWidth, groupHeight), "You ran into a wall... \nPress O to restart");
			if (OuyaExampleCommon.GetButtonDown(OuyaSDK.KeyEnum.BUTTON_O, player))
			{
				Application.LoadLevel(Application.loadedLevel);
			}
			GUI.EndGroup ();
		}
	}
	
	#region SaveLoad
	public void Save()
	{
		//Create binary formatter
		BinaryFormatter bf = new BinaryFormatter();
		
		//Create a file
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
		
		//Create a PlayerData class and set values
		PlayerData data = new PlayerData();
		//data.totalClicks = totalClicks;
		data.totalFlips = totalFlips;
		
		//Take the seriazable PlayerData class that data is, and write it to file
		bf.Serialize(file, data);
		file.Close ();
	}
	
	public void Load()
	{
		//If the file exists
		if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
		{
			//Create a binary formatter
			BinaryFormatter bf = new BinaryFormatter();
			//Open the file
			FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			//Create a PlayerData file by typecasting what is deserialized from the binary formatter reading from the file
			PlayerData data = (PlayerData)bf.Deserialize(file);
			//Close the file
			file.Close();
			//Set the loaded data
			//totalClicks = data.totalClicks;
			totalFlips = data.totalFlips;
		}
	}
	#endregion
}

[Serializable] //This tells unity that this is a data container that can be written to a file
class PlayerData
{
	//Use private and get/set, this is quick
	//public int totalClicks;
	public int totalFlips;
}

using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerController : MonoBehaviour, 
OuyaSDK.IMenuButtonUpListener,
OuyaSDK.IMenuAppearingListener,
OuyaSDK.IPauseListener,
OuyaSDK.IResumeListener 
{
	//Player Stats
	public int startSpeed = 3;   	// Player Move Speed
	public int boostedSpeed = 6;	//Turbo speed
	private int speed;
	
	//Ouya Input
	public OuyaSDK.OuyaPlayer player = OuyaSDK.OuyaPlayer.player1;  // Player controller
	public float joystickDeadzone = 0.25f;                          // the size of the joystick deadzone
	public float triggerThreshold = 0.1f;                           // the threshold before the trigger is pressed
	private Vector2 moveDirection; 									// Used for movement
	
	//Weapons
	public GameObject missile; 			//Prefab of missile to shoot
	public List<GameObject> missiles; 	//List of shot missiles
	public int currentMissileAmount; 	//Amount of missiles currently loaded
	public int missileReloads; 			//Clips remaining
	public int missileClipSize = 25; 	//Clip size
	public float fireRate = 0.12f; 		//Fire speed
	public float missileSpeed = 12; 	//Missile speed

	//Time Variables
	private float nextFireTime; 		//Stores time player can shoot next
	private float scaleTime; 			//Stores time player is scaled up until
	private bool scaledUp; 				//is the player scaled up?
	private float speedTime; 			//Stores time player is sped up until
	private bool spedUp; 				//is the player sped up?
	public float scaleUpTimeLength; 	//How long the player will be scaled up for
	public float speedUpTimeLength; 	//How long the player will be sped up for
	
	//GUI Ammo Reference
	public GUIText ammoText;

	#region Enter/Exit Script
	void Awake()
	{
		//Start Ouya Input
		OuyaSDK.registerMenuButtonUpListener(this);
		OuyaSDK.registerMenuAppearingListener(this);
		OuyaSDK.registerPauseListener(this);
		OuyaSDK.registerResumeListener(this);
		UnityEngine.Input.ResetInputAxes();
	}
	void OnDestroy()
	{
		//Stop Ouya Input
		OuyaSDK.unregisterMenuButtonUpListener(this);
		OuyaSDK.unregisterMenuAppearingListener(this);
		OuyaSDK.unregisterPauseListener(this);
		OuyaSDK.unregisterResumeListener(this);
		UnityEngine.Input.ResetInputAxes();
	}
	#endregion
	
	#region Ouya Input
	public void OuyaMenuButtonUp()
	{
	}
	
	public void OuyaMenuAppearing()
	{
	}
	
	public void OuyaOnPause()
	{
	}
	
	public void OuyaOnResume()
	{
	}
	#endregion

	void Update()
	{
		#region Powerups
		if (spedUp && Time.time > speedTime)
		{
			speed = startSpeed;
			spedUp = false;
		}
		
		if (scaledUp && Time.time > scaleTime)
		{
			transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
			rigidbody2D.mass = 1;
			scaledUp = false;
		}
		#endregion

		#region Button Presses
		if (OuyaExampleCommon.GetButtonDown(OuyaSDK.KeyEnum.BUTTON_O, player))
		{

		}
		if (OuyaExampleCommon.GetButtonDown(OuyaSDK.KeyEnum.BUTTON_U, player))
		{
			
		}
		if (OuyaExampleCommon.GetButtonDown(OuyaSDK.KeyEnum.BUTTON_Y, player))
		{

		}
		if (OuyaExampleCommon.GetButtonDown(OuyaSDK.KeyEnum.BUTTON_A, player))
		{
			
		}
		#endregion

		#region Movement
		moveDirection = new Vector2(0f, 0f);

		//Ouya
		if (Mathf.Abs(OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_LSTICK_X, player)) > joystickDeadzone) //If joystick is past deadzone, apply movement
		{
			moveDirection.x = OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_LSTICK_X, player);
		}
		if (Mathf.Abs(OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_LSTICK_Y, player)) > joystickDeadzone)
		{
			moveDirection.y = -OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_LSTICK_Y, player); // - to make it not inverted
		}


		//PC
		if (Input.GetButton("W"))
		{
			moveDirection.y = 1;
		}
		if (Input.GetButtonUp("W"))
		{
			moveDirection.y = 0;
		}
		if (Input.GetButton("S"))
		{
			moveDirection.y = -1;
		}
		if (Input.GetButtonUp("S"))
		{
			moveDirection.y = 0;
		}
		if (Input.GetButton("A"))
		{
			moveDirection.x = -1;
		}
		if (Input.GetButtonUp("A"))
		{
			moveDirection.x = 0;
		}
		if (Input.GetButton("D"))
		{
			moveDirection.x = 1;
		}
		if (Input.GetButtonUp("D"))
		{
			moveDirection.x = 0;
		}

		//Apply movement vector
		rigidbody2D.velocity = direction * speed;
		#endregion

		#region Weapons
		if ((((Mathf.Abs(OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_RSTICK_X, player)) > joystickDeadzone) ||
		    (Mathf.Abs(OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_RSTICK_Y, player)) > joystickDeadzone) ||
		    Input.GetButtonDown("I") || Input.GetButtonDown("K") || Input.GetButtonDown("J") || Input.GetButtonDown("L"))) &&
		    Time.time > nextFireTime)
		{
			if (currentMissileAmount == 0 && missileReloads == 0)
			{
				Debug.Log("No ammo");
			}
			else
			{
				if (currentMissileAmount == 0) 
				{
					Debug.Log("Clip emptied.");
					reload();
				}
				Shoot();
				nextFireTime = Time.time + fireRate;
			}
		}

		//Update GUI Ammo count
		ammoText.text = "Ammo: " + currentMissileAmount + " / " + missileReloads;
		#endregion
	}

	#region Weapons
	void Shoot()
	{
		//Instantiate the missile, save the reference, name it
		GameObject temp = Instantiate (missile, transform.position, Quaternion.identity) as GameObject;
		temp.name = ("Missile:"+missileReloads+currentMissileAmount);

		//Movement Variables
		Vector2 missileDirection = new Vector2(0f,0f);

		//Missile launching
		//Ouya
		if (Mathf.Abs(OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_RSTICK_X, player)) > joystickDeadzone)
		{
			missileDirection.x = (OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_RSTICK_X, player));
			//if (OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_RSTICK_X, player) > 0) missileDirection.x = 1;
			//if (OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_RSTICK_X, player) < 0) missileDirection.x = -1;
		}
		if (Mathf.Abs(OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_RSTICK_Y, player)) > joystickDeadzone)
		{
			missileDirection.y = -(OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_RSTICK_Y, player));
			//if (OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_RSTICK_Y, player) < 0) missileDirection.y = 1; 
			//if (OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_RSTICK_Y, player) > 0) missileDirection.y = -1;
		}


		//PC
		if (Input.GetButtonDown("I"))
		{
			missileDirection.y = 1;
		}
		if (Input.GetButtonDown("K"))
		{
			missileDirection.y = -1;
		}
		if (Input.GetButtonDown("J"))
		{
			missileDirection.x = -1;
		}
		if (Input.GetButtonDown("L"))
		{
			missileDirection.x = 1;
		}


		//Apply direction * speed, save reference, update ammo count
		temp.rigidbody2D.velocity = missileDirection * missileSpeed;
		missiles.Add(temp);
		currentMissileAmount--;
	}

	void reload()
	{
		missiles.Clear(); 	//Destroy all current bullets
		missileReloads--; 
		currentMissileAmount = missileClipSize;
	}
	#endregion

	#region Powerups
	public void ApplyPowerUp(int PUType)
	{
		switch(PUType)
		{
		case 0: //SizeUp
			if (scaledUp)
			{
				scaleTime += scaleUpTimeLength;
			}
			else
			{
				ScaleUp();
			}
			break;
		case 1: //SpeedUp
			if (spedUp)
			{
				speedTime += speedUpTimeLength;
			}
			else
			{
				SpeedUp();
			}
			break;
		case 2: //AmmoUp
			currentMissileAmount += 50;
			break;
		case 3: //Score
			GameController.controller.Score = 100;
			break;
		case 4: //TBD
			break;
		case 5: //TBD
			break;
		case 6: //TBD
			break;
		}
	}

	void ScaleUp()
	{
		transform.localScale = new Vector3(2f, 2f, 2f);
		rigidbody2D.mass = 5;
		scaledUp = true;
		scaleTime = Time.time + scaleUpTimeLength;
	}

	void SpeedUp()
	{
		speed = boostedSpeed;
		spedUp = true;
		speedTime = Time.time + speedUpTimeLength;
	}
	#endregion
}
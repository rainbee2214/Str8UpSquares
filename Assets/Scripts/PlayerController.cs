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
	public int speed = 3;   // Player Move Speed
	
	//Ouya Input
	public OuyaSDK.OuyaPlayer player = OuyaSDK.OuyaPlayer.player1;  // Player controller
	public float joystickDeadzone = 0.25f;                                  // the size of the deadzone
	public float triggerThreshold = 0.1f;                           // the threshold before the trigger is pressed
	public Vector2 direction;
	
	//Weapons
	public GameObject missile;
	public List<GameObject> missiles;
	public int currentMissileAmount;
	public int missileReloads;
	public int missileClipSize = 25;
	public float fireRate = 0.12f;
	public float missileSpeed = 12;
	
	private float nextFireTime;
	
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
		//Update GUI Ammo count
		ammoText.text = "Ammo: " + ((missileReloads * missileClipSize)+currentMissileAmount);


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
		direction = new Vector2(0f, 0f);

		//Ouya
		if (Mathf.Abs(OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_LSTICK_X, player)) > joystickDeadzone) //If joystick is past deadzone, apply movement
		{
			direction.x = OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_LSTICK_X, player);
		}
		if (Mathf.Abs(OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_LSTICK_Y, player)) > joystickDeadzone)
		{
			direction.y = -OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_LSTICK_Y, player); // - to make it not inverted
		}
		
		//PC
		if (Input.GetButton("W"))
		{
			direction.y = 1;
		}
		if (Input.GetButtonUp("W"))
		{
			direction.y = 0;
		}
		if (Input.GetButton("S"))
		{
			direction.y = -1;
		}
		if (Input.GetButtonUp("S"))
		{
			direction.y = 0;
		}
		if (Input.GetButton("A"))
		{
			direction.x = -1;
		}
		if (Input.GetButtonUp("A"))
		{
			direction.x = 0;
		}
		if (Input.GetButton("D"))
		{
			direction.x = 1;
		}
		if (Input.GetButtonUp("D"))
		{
			direction.x = 0;
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
		#endregion
	}
	
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
}
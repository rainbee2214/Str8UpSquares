using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerController : MonoBehaviour, 
OuyaSDK.IMenuButtonUpListener,
OuyaSDK.IMenuAppearingListener,
OuyaSDK.IPauseListener,
OuyaSDK.IResumeListener 
{
	public int speed = 3; 											// Player Move Speed
	public OuyaSDK.OuyaPlayer player = OuyaSDK.OuyaPlayer.player1;	// Player controller
	public float joystickDeadzone = 0.25f; 									// the size of the deadzone
	public float triggerThreshold = 0.1f; 							// the threshold before the trigger is pressed

	public Vector2 direction;


	public GameObject missile;
	public List<GameObject> missiles;
	public int currentMissileAmount;
	public int missileReloads;
	public int missileClipSize = 25;

	public GUIText ammoText;

	public float fireRate = 0.12f;
	private float nextFireTime;

	void Awake()
	{
		OuyaSDK.registerMenuButtonUpListener(this);
		OuyaSDK.registerMenuAppearingListener(this);
		OuyaSDK.registerPauseListener(this);
		OuyaSDK.registerResumeListener(this);
		Input.ResetInputAxes();
	}
	void OnDestroy()
	{
		OuyaSDK.unregisterMenuButtonUpListener(this);
		OuyaSDK.unregisterMenuAppearingListener(this);
		OuyaSDK.unregisterPauseListener(this);
		OuyaSDK.unregisterResumeListener(this);
		Input.ResetInputAxes();
	}
	
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

	void Update()
	{
		ammoText.text = "Ammo: " + ((missileReloads * missileClipSize)+currentMissileAmount);

		if (OuyaExampleCommon.GetButtonDown(OuyaSDK.KeyEnum.BUTTON_O, player))
		{
			Application.LoadLevel("LevelFour");
		}
		if (OuyaExampleCommon.GetButtonDown(OuyaSDK.KeyEnum.BUTTON_U, player))
		{
			
		}
		if (OuyaExampleCommon.GetButtonDown(OuyaSDK.KeyEnum.BUTTON_Y, player))
		{
			Application.LoadLevel("Menu");
		}
		if (OuyaExampleCommon.GetButtonDown(OuyaSDK.KeyEnum.BUTTON_A, player))
		{
			
		}


		direction = new Vector2(0f, 0f);

		//Ouya
		//If joystick is past deadzone, apply movement
		if (Mathf.Abs(OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_LSTICK_X, player)) > joystickDeadzone)
		{
			direction.x = OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_LSTICK_X, player);
		}
		if (Mathf.Abs(OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_LSTICK_Y, player)) > joystickDeadzone)
		{
			direction.y = -OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_LSTICK_Y, player); // - to make it not inverted
		}
		
		//Movement on a computer

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

		rigidbody2D.velocity = direction * speed;

		//Missiles
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
				shoot();
				nextFireTime = Time.time + fireRate;
			}
		}
	}
	
	void shoot()
	{
		GameObject temp = Instantiate (missile, transform.position, Quaternion.identity) as GameObject;
		temp.name = ("Missile:"+missileReloads+currentMissileAmount);
		
		Vector2 missileDirection = new Vector2(0f,0f);
		int missileSpeed = 12;

		//Missile launching
		if (Mathf.Abs(OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_RSTICK_X, player)) > joystickDeadzone)
		{
			if (OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_RSTICK_X, player) > 0) missileDirection.x = 1;
			if (OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_RSTICK_X, player) < 0) missileDirection.x = -1;
		}
		if (Mathf.Abs(OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_RSTICK_Y, player)) > joystickDeadzone)
		{
			if (OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_RSTICK_Y, player) < 0) missileDirection.y = 1; 
			if (OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_RSTICK_Y, player) > 0) missileDirection.y = -1;
		}

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

		temp.rigidbody2D.velocity = missileDirection * missileSpeed;
		missiles.Add(temp);
		currentMissileAmount--;
	}

	void reload()
	{
		missiles.Clear();
		missileReloads--;
		currentMissileAmount = 10;
	}
}
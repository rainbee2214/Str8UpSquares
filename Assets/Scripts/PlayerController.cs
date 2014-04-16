using UnityEngine;
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
	}
}
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{


	public int speed = 3; 											// Player Move Speed
	public bool continuousScan = true; 								// do we want to scan for trigger and d-pad button events ?
	//public OuyaPlayer player = OuyaPlayer.P01; 						// the player/controller we want to observe
	//public DeadzoneType deadzoneType = DeadzoneType.CircularMap; 	// the type of deadzone we want to use for convenience access
	public float deadzone = 0.25f; 									// the size of the deadzone
	public float triggerThreshold = 0.1f; 							// the threshold before the trigger is pressed

	public Vector2 direction;

	void Start () 
	{

		//OuyaInput.SetContinuousScanning(continuousScan);			// set button state scanning to receive input state events for trigger and d-pads
		//OuyaInput.SetDeadzone(deadzoneType, deadzone);				// define the deadzone if you want to use advanced joystick and trigger access
		//OuyaInput.SetTriggerThreshold(triggerThreshold); 			// do one controller update here to get everything started as soon as possible
		//OuyaInput.UpdateControllers();
	}

	void Update()
	{
		//OuyaInput.UpdateControllers();

		//Only one type of movement can work at a time
		//Ouya controller movement
//		Vector2 movement = OuyaInput.GetJoystick(OuyaJoystick.LeftStick, player);
//		rigidbody2D.velocity = movement * speed;
//		if (OuyaInput.GetButton(OuyaButton.O,player))
//		{
//			rigidbody2D.velocity = new Vector2(3,3);
//		}
		//Movement on a computer
		direction = new Vector2(0f, 0f);
		if (Input.GetButton("W"))
		{
			direction.y = speed;
			rigidbody2D.velocity = direction;
		}
		if (Input.GetButtonUp("W"))
		{
			direction.y = 0;
			rigidbody2D.velocity = direction;
		}
		if (Input.GetButton("S"))
		{
			direction.y = -speed;
			rigidbody2D.velocity = direction;;
		}
		if (Input.GetButtonUp("S"))
		{
			direction.y = 0;
			rigidbody2D.velocity = direction;
		}
		if (Input.GetButton("A"))
		{
			direction.x = -speed;
			rigidbody2D.velocity = direction;
		}
		if (Input.GetButtonUp("A"))
		{
			direction.x = 0;
			rigidbody2D.velocity = direction;
		}
		if (Input.GetButton("D"))
		{
			direction.x = speed;
			rigidbody2D.velocity = direction;
		}
		if (Input.GetButtonUp("D"))
		{
			direction.x = 0;
			rigidbody2D.velocity = direction;
		}
	}
}

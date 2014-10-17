using UnityEngine;
using System.Collections;

public class FlipFlop : MonoBehaviour {
	const int _ZERO = 0;
	const int _ONE = 1;

	GameObject zero, one;
	SpriteRenderer spriteRenderer0;
	SpriteRenderer spriteRenderer1;

	int state = 0;
	int whichToDisplay;
	int currentlyDisplayed;

	bool chooseZero = false;
	bool chooseOne = false;
	public bool flipNow = false;
	
	void Start () {
		currentlyDisplayed = _ZERO;
		whichToDisplay = _ZERO;
		one = (transform.GetChild (1)).gameObject;
		zero = (transform.GetChild (0)).gameObject;
		spriteRenderer0 = one.GetComponent<SpriteRenderer>();
		spriteRenderer1 = zero.GetComponent<SpriteRenderer>();
		spriteRenderer1.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (flipNow) 
		{
			flip ();
		}
		if (chooseZero)
		{
			state = 0;
			spriteRenderer0.enabled = true;
			spriteRenderer1.enabled = false;
			chooseZero = false;
		}
		else if (chooseOne)
		{
			state = 1;
			spriteRenderer0.enabled = false;
			spriteRenderer1.enabled = true;
			chooseOne = false;
		}
	}

	public int getState()
	{
		return state;
	}

	public void flip()
	{
		flipNow = false;
		if (state == 0)
		{
			chooseZero = false;
			chooseOne = true;
			state = 1;
		}
		else if (state == 1)
		{
			chooseZero = true;
			chooseOne = false;
			state = 0;
		}
	}
}

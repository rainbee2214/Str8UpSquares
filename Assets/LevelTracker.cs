using UnityEngine;
using System.Collections;

public class LevelTracker : MonoBehaviour {

	public GameObject flipFlop;
	public int n = 16;
	GameObject[] flipFlops;
	GameObject f;
	int state;
	public bool flip;
	// Use this for initialization
	void Start () {
		flipFlops = new GameObject[n];
		for(int i = 0; i < n; i++)
		{
			flipFlops[i] = Instantiate (flipFlop) as GameObject;

			flipFlops[i].name = "FlipFlop:" + i;
			flipFlops[i].transform.parent = this.transform;
			Vector3 position = new Vector3(19-i, 19, 1);
			flipFlops[i].transform.position = position;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		int index = 0;
		int afterFlip = 0;
		if(flip)
		{

			flipABit ();
		}
	
	}
	// This function will always flip bits starting with the right most bit. If adds one.
	void flipABit()
	{
		flip = false;
		int index = 0;
		int afterFlip = 0;
		while (afterFlip != 1 && index < n)
		{

			flipFlops[index].gameObject.GetComponent<FlipFlop> ().flip ();
			afterFlip = flipFlops[index].gameObject.GetComponent<FlipFlop>().getState();
			index++;
			//if the flip becomes a 0
		}
		//Debug.Log (getDecimal());
		float decimalValue = getDecimal ();
		Debug.Log (decimalValue);
	}

	float getDecimal()
	{
		float decimalValue = 0;
		for (int i = 0; i < n; i++)
		{
			if (flipFlops[i].gameObject.GetComponent<FlipFlop>().getState() == 1)
			{
				decimalValue += Mathf.Pow(2, i);
			}
		}

		return decimalValue;
	}
}

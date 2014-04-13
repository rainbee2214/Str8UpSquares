using UnityEngine;
using System.Collections;

public class WarpController : MonoBehaviour {
	public bool warpEnabled = false;
	public Color enabledColor;
	public Color disabledColor;

	public ParticleSystem particle;

	public OuyaPlayer playerOuya;
	public GameObject player;
	public GameObject warpPartner;

	void Start () 
	{
		renderer.material.color = disabledColor;
		particle.startColor = enabledColor;
		warpEnabled = false;
	}

	void Update () 
	{

		if (warpEnabled)
		{
			renderer.material.color = enabledColor;
			particle.enableEmission = true;

			if (OuyaInput.GetButtonDown(OuyaButton.O,playerOuya))
			{
				player.rigidbody2D.transform.position = warpPartner.rigidbody2D.transform.position;
				//Debug.Log("Warped.");
			}


//			if (Input.GetButtonDown("Q"))
//			{
//
//				player.rigidbody2D.transform.position = warpPartner.rigidbody2D.transform.position;
//				Debug.Log("Warped.");
//			}
		}
		else
		{
			renderer.material.color = disabledColor;
			particle.enableEmission = false;
		}

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{

			warpEnabled = true;
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			warpEnabled = false;
		}
	}
}

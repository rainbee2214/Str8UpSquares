using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridSpace : MonoBehaviour 
{
	// A Gridspace consists of one "Register" Which is 8 bits long: 8 squares
	GameObject register;
	GameObject[] registers;
	public int numberOfRegisters = 1;

	GameObject[] gridSpace = new GameObject[8];
	List<Color> colors;
	int currentColorIndex;

	public bool changeColor;

	Material material;
	
	void Start () 
	{
		registers = new GameObject[numberOfRegisters];
		colors = GameControl.controller.SetColors();
		Vector3 registerPosition = new Vector3(0f, 0f, 1f);
		for (int i = 0; i < registers.Length; i++)
		{
			registers[i] = CreateRegister();
			registers[i].gameObject.name = "Register " + i;
			registers[i].transform.position = registerPosition;
			if ((i+1)%4 == 0) registerPosition.y += 1f;
			registerPosition.y += 1f;
		}
	}

	public GameObject CreateRegister()
	{
		Vector3 gridSpacePosition = new Vector3(-4f, 0f, 1f);
		GameObject reg = new GameObject();
		for (int i = 0; i < gridSpace.Length; i++)
		{
			gridSpace[i] = GameObject.CreatePrimitive(PrimitiveType.Quad);
			gridSpace[i].transform.position = gridSpacePosition;
			gridSpacePosition.x += 1f;
			gridSpace[i].gameObject.renderer.material = Resources.Load("GridSpaceMaterial", typeof(Material)) as Material;
			gridSpace[i].gameObject.renderer.material.color = colors[currentColorIndex];
			gridSpace[i].gameObject.name = i+"";
			gridSpace[i].transform.parent = reg.transform;
		}
		return reg;
	}

	void Update () 
	{	
		if (changeColor) ChangeColor();
	}

	void ChangeColor()
	{
		changeColor = false;
		currentColorIndex++;
		if (currentColorIndex >= colors.Count) currentColorIndex = 0;
		for (int i = 0; i < gridSpace.Length; i++)
			gridSpace[i].renderer.material.color = colors[currentColorIndex];
	}
}


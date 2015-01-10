using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridGenerator : MonoBehaviour
{
    GameObject gridSquare;
    List<GameObject> squares;

    void Awake()
    {
        gridSquare = Resources.Load("Prefabs/GridSquare", typeof(GameObject)) as GameObject;
        BuildGrid(GameController.controller.gridWidth, GameController.controller.gridHeight);
    }

    void BuildGrid(int width, int height)
    {
        squares = new List<GameObject>();
        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                squares.Add(Instantiate(gridSquare, new Vector2(x,y) ,Quaternion.identity) as GameObject);
            }
        }

        for (int i = 0; i < squares.Count; i++)
        {
            squares[i].name = "Square" + i;
            squares[i].transform.parent = transform;
            squares[i].renderer.material = Instantiate(Resources.Load("Materials/GridSquare", typeof(Material))) as Material;
        }
    }
}

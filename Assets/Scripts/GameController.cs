using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public static GameController controller;
    public int gridHeight;
    public int gridWidth;

    public Vector3 centerPosition;

    void Awake ()
    {
        if (controller == null)
        {
            DontDestroyOnLoad(gameObject);
            controller = this;
        }
        else if (controller != this)
        {
            Destroy(gameObject);
        }

        centerPosition.Set(gridWidth / 2f, gridHeight / 2f, -1f);
    }

    void Update()
    {

    }
}

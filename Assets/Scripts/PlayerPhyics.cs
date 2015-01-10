using UnityEngine;
using System.Collections;

public class PlayerPhyics : MonoBehaviour
{
    Vector2 position;

    void Awake()
    {
        position = transform.position;
    }

    public void Move(float x, float y)
    {
        position.Set(x+transform.position.x, y+transform.position.y);
        rigidbody2D.MovePosition(position);
    }

    public void Shoot()
    {

    }
}

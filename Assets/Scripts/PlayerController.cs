using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhyics))]
public class PlayerController : MonoBehaviour
{
    PlayerPhyics playerPhysics;
    public float speed = 1f;

    float scale;

    void Awake()
    {
        playerPhysics = GetComponent<PlayerPhyics>();
    }

    void Start()
    {
        Vector3 p = GameController.controller.centerPosition;
        scale = transform.localScale.x;
        p.Set(p.x - (scale/2f), p.y - (scale/2f), p.z);
        transform.position = p;

    }

    void FixedUpdate()
    {
        if (HasAxisInput()) playerPhysics.Move(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime);
    }

    bool HasAxisInput()
    {
        return (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 ||
            Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0);
    }
}

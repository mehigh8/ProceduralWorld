using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    public float movementSpeed;

    private Vector3 direction = Vector3.zero;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            direction = Vector3.up;
        else if (Input.GetKey(KeyCode.S))
            direction = Vector3.down;
        else if (Input.GetKey(KeyCode.A))
            direction = Vector3.left;
        else if (Input.GetKey(KeyCode.D))
            direction = Vector3.right;
        else
            direction = Vector3.zero;
    }

    private void FixedUpdate()
    {
        transform.position += direction * movementSpeed;
    }
}

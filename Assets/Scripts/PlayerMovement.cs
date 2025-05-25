using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float xSensitivity;
    public float ySensitivity;
    public GameObject playerCamera;

    private Vector3 direction = Vector3.zero;
    private float xRotation;
    private float yRotation;

    void Start()
    {
        
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ySensitivity;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        playerCamera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        if (Input.GetKey(KeyCode.W))
            direction = transform.forward;
        else if (Input.GetKey(KeyCode.S))
            direction = -transform.forward;
        else if (Input.GetKey(KeyCode.A))
            direction = -transform.right;
        else if (Input.GetKey(KeyCode.D))
            direction = transform.right;
        else
            direction = Vector3.zero;
    }

    private void FixedUpdate()
    {
        transform.position += direction * movementSpeed;
        if (Physics.Raycast(new Vector3(transform.position.x, 200, transform.position.z), Vector3.down, out RaycastHit hit, float.MaxValue))
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
    }
}

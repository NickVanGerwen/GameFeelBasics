using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float sensX = 1;
    [SerializeField] float sensY = 1;

    Transform cam;
    [SerializeField] Transform orientation;

    float mouseX;
    float mouseY;

    float xRotation;
    float yRotation;

    [SerializeField] float maxXRotation = 90;

    void Start()
    {
        // gets camera rig transform
        cam = Camera.main.transform.parent.transform;

        // locks cursor to center of screen and hides it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        GetInput();

        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    void GetInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensX;
        xRotation -= mouseY * sensY;

        // clamps xRotation to prevent camera from flipping
        xRotation = Mathf.Clamp(xRotation, -maxXRotation, maxXRotation);
    }
}

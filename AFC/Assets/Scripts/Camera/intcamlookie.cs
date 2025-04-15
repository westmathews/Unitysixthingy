using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class intcamlookie : NetworkBehaviour
{
    public float mouseSensitivity; // Mouse sensitivity
    public float xRotation; // Vertical rotation
    public GameObject mncam;
    public GameObject stickycam;
    public float bsmssen;
    void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        bsmssen = mouseSensitivity;
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            mouseSensitivity = 0;
        }
        else
        {
            mouseSensitivity = bsmssen;
        }
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Apply mouse movement to the camera's rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp the vertical rotation
        if (mncam.GetComponent<Lookie>().recoilX == 0)
        {
            xRotation = mncam.GetComponent<Lookie>().xRotation;
        }
    }
}

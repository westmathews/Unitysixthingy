using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunShmove : MonoBehaviour
{
    public float mouseSensitivity; // Mouse sensitivity
    public Transform playerBody; // Reference to the player's body for rotation
    public float xRotation = 0f; // Vertical rotation
    public bool thirdpers = false;
    public float basemousesensitivity;
    public GameObject mncam;

    void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {   /*
        if (Input.GetKey(KeyCode.LeftShift))
        {

            thirdpers = true;
        }
        else
        {
            thirdpers = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            xRotation = mncam.GetComponent<Lookie>().xRotation;
        }
        xRotation = mncam.GetComponent<Lookie>().xRotation;
        if (mncam.GetComponent<Lookie>().scoping)
        {
            mouseSensitivity = basemousesensitivity / 2;
        }
        else
        {
            mouseSensitivity = basemousesensitivity;
        }
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Apply mouse movement to the camera's rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp the vertical rotation
        xRotation -= mncam.GetComponent<Lookie>().recoilX;
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        if(!thirdpers)
        {            
            playerBody.Rotate(Vector3.up * mouseX);
        }
        */
        transform.localRotation = mncam.transform.localRotation;
        
    }
}

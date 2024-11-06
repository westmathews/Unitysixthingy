using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShmove : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Mouse sensitivity
    public Transform playerBody; // Reference to the player's body for rotation
    private float xRotation = 0f; // Vertical rotation
    public bool thirdpers = false;
    //public GameObject sticam;
    void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {

            thirdpers = true;
        }
        else
        {
            thirdpers = false;
        }
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Apply mouse movement to the camera's rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp the vertical rotation

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        if(!thirdpers)
        {

            
            playerBody.Rotate(Vector3.up * mouseX);
        }
        //if (sticam.GetComponent<PewPew>().maingun)
        //{
            //xRotation -= mouseY + 20;
            //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //}

    }
}

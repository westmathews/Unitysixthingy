using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookie : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Mouse sensitivity
    public Transform playerBody; // Reference to the player's body for rotation
    public float xRotation = 0f; // Vertical rotation
    public GameObject Sticker;
    public Transform player;
    public Vector3 starting;
    public float recoilX;
    public float recoilStrength; // How strong the recoil is
    public float recoilResetSpeed;// Speed at which recoil resets
    public float finalcoil;
    public bool coilin = false;
    public float initialcoil;
    public GameObject intcam;
    void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Apply mouse movement to the camera's rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp the vertical rotation
        xRotation -= recoilX;
        recoilX = Mathf.Lerp(recoilX, 0f, recoilResetSpeed * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
        if (Sticker.GetComponent<PewPew>().maingun)
        {
            recoil();
        }
        if (xRotation <= finalcoil && coilin)
        {
            recoilX -= recoilStrength;
            coilin = false;
        }
        if (xRotation > initialcoil)
        {
            recoilX = 0;
            initialcoil = 10000;
        }
    }
    public void recoil()
    {
        initialcoil = intcam.GetComponent<intcamlookie>().xRotation;
        finalcoil = intcam.GetComponent<intcamlookie>().xRotation - recoilStrength;
        recoilX += recoilStrength;
        coilin = true;
    }
}

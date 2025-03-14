using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookie : MonoBehaviour
{
    
    
    public float xRotation = 0f; // Vertical rotation
    [Header("References")]
    public Transform playerBody; // Reference to the player's body for rotation
    public GameObject Sticker;
    public Transform player;
    public Vector3 starting;
    public Camera maincam;
    [Header("Recoil")]
    public bool secondaryfired = false;
    public float recoilX;
    public float recoilStrength; // How strong the recoil is
    public float secondaryrecoilstrength;
    public float recoilResetSpeed;// Speed at which recoil resets
    public float finalcoil;
    public bool coilin = false;
    public float initialcoil;
    public GameObject intcam;
    [Header("sensitivity")]
    public float bsmssen;
    public float mouseSensitivity = 100f; // Mouse sensitivity
    [Header("Scoping")]
    public float defFOV;
    public float zoomFOV;
    public float zoomspeed;
    public bool scopestart = false;
    public bool blockscope = true;
    public bool scoping = false;
    [Header("ScopedRecoil")]
    public float scopedrecoilstrength;
    public float scopedsecondaryrecoilstrength;
    public float scopedrecoilspeed;
    void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        bsmssen = mouseSensitivity;
    }

    void Update()
    {
        if (maincam.GetComponent<CameraSwitchWithRotation>().isThirdPerson == false)
        {
            blockscope = false;
        }
        else
        {
            blockscope = true;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            mouseSensitivity = bsmssen * 2;
        }
        if (!scoping && !(Input.GetKey(KeyCode.LeftShift)))
        {
            mouseSensitivity = bsmssen;
        }
        //toggle scope
        if (Input.GetKey(KeyCode.Mouse1))
        {
            scopestart = true;
        }
        else
        {
            scopestart = false;
        }
        if (scopestart && !blockscope)
        {
            scoping = true;
        }
        else
        {
            scoping = false;
        }
        if (scoping)
        {
            mouseSensitivity = bsmssen / 2;
        }
        float targetFOV = scoping ? zoomFOV : defFOV;
        maincam.fieldOfView = Mathf.Lerp(maincam.fieldOfView, targetFOV, Time.deltaTime * zoomspeed);
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Apply mouse movement to the camera's rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp the vertical rotation
        xRotation -= recoilX;

        if (scoping)
        {
            recoilX = Mathf.Lerp(recoilX, 0f, scopedrecoilspeed * Time.deltaTime);
        }
        else
        {
            recoilX = Mathf.Lerp(recoilX, 0f, recoilResetSpeed * Time.deltaTime);
        }
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
        if (secondaryfired)
        {
            if (xRotation <= finalcoil && coilin)
            {
                if (scoping)
                {
                    recoilX -= scopedsecondaryrecoilstrength;

                }
                else
                {
                    recoilX -= secondaryrecoilstrength;

                }
                coilin = false;
            }
        }
        else
        {
            if (xRotation <= finalcoil && coilin)
            {
                if (scoping)
                {
                    recoilX -= scopedrecoilstrength;

                }
                else
                {
                    recoilX -= recoilStrength;

                }
                coilin = false;
            }
        }
        if (xRotation > initialcoil)
        {
            recoilX = 0;
            initialcoil = 10000;
        }
    }
    public void recoil()
    {
        recoilX = 0;
        initialcoil = intcam.GetComponent<intcamlookie>().xRotation;
        if (scoping)
        {
            recoilX += scopedrecoilstrength;
            finalcoil = intcam.GetComponent<intcamlookie>().xRotation - scopedrecoilstrength;
        }
        else
        {
            recoilX += recoilStrength;
            finalcoil = intcam.GetComponent<intcamlookie>().xRotation - recoilStrength;
        }
        secondaryfired = false;
        coilin = true;
        Debug.Log("int" + intcam.GetComponent<intcamlookie>().xRotation);
        Debug.Log("main" + xRotation);
    }
    public void secondaryrecoil()
    {
        recoilX = 0;
        initialcoil = intcam.GetComponent<intcamlookie>().xRotation;

        if (scoping)
        {
            recoilX += scopedsecondaryrecoilstrength;
            finalcoil = intcam.GetComponent<intcamlookie>().xRotation - scopedsecondaryrecoilstrength;

        }
        else
        {
            recoilX += secondaryrecoilstrength;
            finalcoil = intcam.GetComponent<intcamlookie>().xRotation - secondaryrecoilstrength;
        }
        secondaryfired = true;
        
        coilin = true;
    }
}

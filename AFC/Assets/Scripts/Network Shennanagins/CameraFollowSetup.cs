using UnityEngine;
using Mirror;

public class CameraFollowSetup : NetworkBehaviour
{
    public Camera playerCamera;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        // Optional: disable any menu/UI camera
        Camera menuCam = Camera.main;
        if (menuCam != null && menuCam != playerCamera)
        {
            menuCam.gameObject.SetActive(false);
        }

        // Enable and position the camera
        if (playerCamera != null)
        {
            playerCamera.gameObject.SetActive(true);
        }
    }
}

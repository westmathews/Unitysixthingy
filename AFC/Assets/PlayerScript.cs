using UnityEngine;
using Mirror;

public class MyPlayer : NetworkBehaviour
{
    public Camera playerCamera; // Assign in the Inspector

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        // Activate camera only for the local player
        if (playerCamera != null)
        {
            playerCamera.gameObject.SetActive(true);
        }
    }

    public override void OnStopLocalPlayer()
    {
        if (playerCamera != null)
        {
            playerCamera.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        if (!isLocalPlayer && playerCamera != null)
        {
            // Disable the camera for non-local players
            playerCamera.gameObject.SetActive(false);
        }
    }
}

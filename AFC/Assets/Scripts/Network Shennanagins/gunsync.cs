using UnityEngine;
using Mirror;

public class GunSync : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnRotChanged))]
    private Quaternion syncedRotation;

    public Transform gunTransform;

    void Update()
    {
        if (isLocalPlayer)
        {
            CmdUpdateGunRotation(gunTransform.rotation);
        }
    }

    [Command]
    void CmdUpdateGunRotation(Quaternion rot)
    {
        syncedRotation = rot;
    }

    void OnRotChanged(Quaternion oldRot, Quaternion newRot)
    {
        if (!isLocalPlayer)
        {
            gunTransform.rotation = newRot;
        }
    }
}

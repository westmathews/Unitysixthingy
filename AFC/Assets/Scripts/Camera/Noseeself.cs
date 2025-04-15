using UnityEngine;
using Mirror;
using UnityEngine.Video;
public class Noseeself : NetworkBehaviour
{
    public GameObject Gun;
    public GameObject PlayerModel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<CameraSwitchWithRotation>().isThirdPerson)
        {
            RpcRevealObject(connectionToClient);
            //GetComponent<Camera>().cullingMask = -1;
        }
        else
        {
            RpcHideObject(connectionToClient);
            //GetComponent<Camera>().cullingMask = 63;
        }



    }
    void RpcHideObject(NetworkConnectionToClient conn)
    {
        if (isLocalPlayer)
        {
            HideObject();
        }
    }
    void HideObject()
    {
        if (PlayerModel.activeSelf)
        {
            PlayerModel.SetActive(false);
            Gun.SetActive(true);
        }
    }
    void RpcRevealObject(NetworkConnectionToClient conn)
    {
        if (isLocalPlayer)
        {
            RevealObject();
        }
    }
    void RevealObject()
    {
        if (!PlayerModel.activeSelf)
        {
            PlayerModel.SetActive(true);
            Gun.SetActive(false);
        }
    }
}

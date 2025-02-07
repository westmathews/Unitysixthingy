using UnityEngine;
using UnityEngine.Video;
public class Noseeself : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<CameraSwitchWithRotation>().isThirdPerson)
        {
            GetComponent<Camera>().cullingMask = -1;
        }
        else
        {
            GetComponent<Camera>().cullingMask = 63;
        }


    }
}

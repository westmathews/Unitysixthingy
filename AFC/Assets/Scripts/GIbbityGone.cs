using UnityEngine;

public class GIbbityGone : MonoBehaviour
{
    public MeshRenderer Gunexist;  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Gunexist.enabled = false;

        }
        else
        {
            Gunexist.enabled = true;
        }
    }
}

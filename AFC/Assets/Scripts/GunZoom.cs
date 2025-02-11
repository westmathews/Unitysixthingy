using UnityEngine;

public class GunZoom : MonoBehaviour
{
    public GameObject mainCam;
    public float GunSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (mainCam.GetComponent<Lookie>().scoping)
        {

            
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, -.2f, .48f), Time.deltaTime * GunSpeed);

        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(.24f, -.423f, .48f), Time.deltaTime * GunSpeed);
        }

    }
}

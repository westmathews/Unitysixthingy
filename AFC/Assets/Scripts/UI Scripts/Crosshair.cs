using UnityEngine;
using UnityEngine.UI;
public class Crosshair : MonoBehaviour
{
    public RawImage CS;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            CS.enabled = false;

        }
        else
        {
            CS.enabled = true;
        }
    }
}

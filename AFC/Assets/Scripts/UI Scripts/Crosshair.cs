using UnityEngine;
using UnityEngine.UI;
public class Crosshair : MonoBehaviour
{

    public GameObject CS1;
    public GameObject CS2;
    public Camera maincam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
       
        if (Input.GetKey(KeyCode.LeftShift))
        {
            CS1.SetActive(false);
            CS2.SetActive(false);
        }
        else
        {
            if (maincam.GetComponent<Lookie>().seeplayer)
            {
                CS1.SetActive(false);
                CS2.SetActive(true);
            }
            else
            {
                CS1.SetActive(true);
                CS2.SetActive(false);
            }

        }
    }
}

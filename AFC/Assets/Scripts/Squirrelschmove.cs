using UnityEngine;

public class Squirrelschmove : MonoBehaviour
{
    public bool tail = true;
    public float Glidetime;
    public float mvabtime;
    public bool mvcool;
    public GameObject UI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mvabtime = .2f;
        mvcool = true;
    }

    // Update is called once per frame
    void Update()
    {
        UI.GetComponent<UI>().mvcool = (mvabtime - 3) * -1;
        if (UI.GetComponent<UI>().mvcool < 0)
        {
            UI.GetComponent<UI>().mvready = true;
            mvcool = false;
        }
        else
        {
            UI.GetComponent<UI>().mvready = false;
            mvcool = false;
        }
        mvabtime += Time.deltaTime;
        Glidetime += Time.deltaTime;
        if (Input.GetKey(KeyCode.E)&& !mvcool && !GetComponentInChildren<CameraSwitchWithRotation>().isThirdPerson && !GetComponent<Player_Movement>().isGrounded)
        {
            GetComponent<Player_Movement>().ySpeed = -1.5f;
            GetComponent<Player_Movement>().gliding = true;
            GetComponent<Player_Movement>().wspeed = 12.5f;           
        }
        else
        {
            GetComponent<Player_Movement>().gliding = false;
            GetComponent<Player_Movement>().wspeed = 7.5f;

        }
        if (Input.GetKey(KeyCode.E) && GetComponent<Player_Movement>().isGrounded)
        {
            mvabtime = 0;
        }
        if (Input.GetKeyUp(KeyCode.E) && !GetComponentInChildren<CameraSwitchWithRotation>().isThirdPerson)
        {
            mvabtime = 0;
        }
        
    }
}
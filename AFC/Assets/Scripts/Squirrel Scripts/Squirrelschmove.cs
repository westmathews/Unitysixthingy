using UnityEngine;

public class Squirrelschmove : MonoBehaviour
{
    public bool tail = true;
    public float Glidetime;
    public float mvabtime;
    public bool mvcool;
    public GameObject UI;
    public float wspeed;
    public float airTime;
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
            
        }
        mvabtime += Time.deltaTime;
        Glidetime += Time.deltaTime;
        if (Input.GetKey(KeyCode.E)&& !mvcool && !GetComponentInChildren<CameraSwitchWithRotation>().isThirdPerson && !GetComponent<Player_Movement>().isGrounded)
        {
            airTime = +1;
            //wspeed = (Mathf.Sqrt(airTime) * 25);
            GetComponent<Player_Movement>().ySpeed = -1.5f;
            GetComponent<Player_Movement>().gliding = true;
            GetComponent<Player_Movement>().wspeed = 15;           
        }
        else
        {
            GetComponent<Player_Movement>().gliding = false;
            GetComponent<Player_Movement>().wspeed = 7.5f;

        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            mvcool = true;
        }
        if (Input.GetKey(KeyCode.E) && GetComponent<Player_Movement>().isGrounded && UI.GetComponent<UI>().mvcool < 0)
        {
            mvabtime = 0;
        }
        if (Input.GetKeyUp(KeyCode.E) && !GetComponentInChildren<CameraSwitchWithRotation>().isThirdPerson && UI.GetComponent<UI>().mvcool < 0)
        {
            mvabtime = 0;
        }
        
    }
}
using UnityEngine;
using Mirror;
public class Lizardschmove : NetworkBehaviour
{
    public bool tail = true;
    public float Speebtime;
    public float mvabtime;
    public bool mvcool;
    public MeshRenderer self;
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
        if (isLocalPlayer)
        {


            UI.GetComponent<UI>().mvcool = (mvabtime - 3) * -1;
            if (!mvcool)
            {
                UI.GetComponent<UI>().mvready = true;
            }
            else
            {
                UI.GetComponent<UI>().mvready = false;

            }
            mvabtime += Time.deltaTime;
            Speebtime += Time.deltaTime;

            if (GetComponentInChildren<Health>().hepo <= 50)
            {
                if (Speebtime >= 3)
                {
                    Speebtime = 0;
                    if (!tail)
                    {
                        GetComponentInParent<Player_Movement>().sprintspd = 11;

                    }
                }
                if (tail)
                {
                    GetComponentInParent<Player_Movement>().sprintspd = 22;
                    tail = false;
                }



            }
            if (GetComponentInChildren<Health>().hepo == 100)
            {
                tail = true;
            }
            if (mvabtime > 3)
            {
                mvcool = false;
            }
            if (Input.GetKeyDown(KeyCode.E) && !mvcool)
            {
                Dissapear(mvabtime);
                mvabtime = 0;
                mvcool = true;
            }
            if (mvabtime < .1)
            {
                GetComponentInParent<Player_Movement>().wspeed = 100;
                GetComponentInParent<Player_Movement>().sprintspd = 100;
                self.enabled = false;
            }
            else
            {
                GetComponentInParent<Player_Movement>().wspeed = 5;
                GetComponentInParent<Player_Movement>().sprintspd = 11;
            }
            if (mvabtime > .4)
            {
                self.enabled = true;
            }
        }
        [Command]
        void Dissapear( float mvabtime)
        {
            Goaway(mvabtime);
        }
        [ClientRpc]
        void Goaway( float mvabtime)
        {
            if (mvabtime < .1)
            {
                self.enabled = false;
            }
            if (mvabtime > .4)
            {
                self.enabled = true;
            }
        }
    }
}

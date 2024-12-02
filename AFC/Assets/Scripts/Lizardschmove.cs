using UnityEngine;

public class Lizardschmove : MonoBehaviour
{
    public bool tail = true;
    public float Speebtime;
    public float mvabtime;
    public bool mvcool;
    public MeshRenderer self;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mvabtime = .2f;
        mvcool = true;
    }

    // Update is called once per frame
    void Update()
    {
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
            mvabtime = 0;
            mvcool = true;
        }
        if (mvabtime < .1)
        {
            GetComponentInParent<Player_Movement>().wspeed = 100;
            self.enabled = false;
        }
        else
        {
            GetComponentInParent<Player_Movement>().wspeed = 5;
            self.enabled = true;
        }

    }
}

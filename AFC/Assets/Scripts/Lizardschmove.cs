using UnityEngine;

public class Lizardschmove : MonoBehaviour
{
    public bool tail = true;
    public float Speebtime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}

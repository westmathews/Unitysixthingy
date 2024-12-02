using UnityEngine;

public class PewPew : MonoBehaviour
{
    //public Camera playerCamera; // Assign your camera in the inspector
    public float shootingRange; // How far the ray can go
    public LayerMask targetLayer; // Specify which layer to detect hits
    public float ammo;
    //public Vector3 target;
    private float rtime;
    public bool reloading;
    private bool shotcooldown;
    private float nospam;
    //public string thing_hit = "nothingyet";
    public float maxammo;
    public float rspd;
    public float shtspd;
    public float dmg;
    public bool maingun;
    public float snddmg;
    public bool abcool;
    public float abtime;
    public float abcooldown;
    public bool secondary;
    private void Start()
    {

        ammo = maxammo;

    }
    void Update()
    {
        if (abtime < abcooldown)
        {
            abcool = true;
        }
        else
        {
            abcool = false;
        }
        if (nospam < shtspd)
        {
            shotcooldown = true;
        }
        else
        {
            shotcooldown = false;
        }
        nospam = nospam + Time.deltaTime;
        abtime = abtime + Time.deltaTime;
        rtime = rtime + Time.deltaTime;
        if (Input.GetKey(KeyCode.R))
        {
            reload();
        }


        if (Input.GetKey(KeyCode.LeftShift))
        {

        }
        else
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
            Debug.DrawRay(transform.position, forward, Color.red);
            if (Input.GetMouseButtonDown(0) && ammo > 0 && !shotcooldown) //Are you allowed to shooting
            {
                ammo = ammo - 1;
                nospam = 0;
                maingun = true;
            }
            else
            {
                maingun = false;
               if (ammo < 1)
               {
                    reload();
               }
            }
            if (Input.GetMouseButtonDown(1)&& !abcool)
            {
                abtime = 0;
                secondary = true;

            }
            else
            {
                secondary = false;
            }
        }

    }
    void reload()
    {
        if (!reloading)
        {
            rtime = 0;
            ammo = 0;
            reloading = true;
        }
        if (rtime >= rspd)
        {
            ammo = maxammo;
            reloading = false;
        }

       
        
        
        
    }
    
    

}
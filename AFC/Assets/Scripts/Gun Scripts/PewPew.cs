using UnityEngine;
using Mirror;
public class PewPew : NetworkBehaviour
{
    //public Camera playerCamera; // Assign your camera in the inspector
    // How far the ray can go
    public LayerMask targetLayer;
    [Header("Ammo")]
    public float ammo;
    public float maxammo;
    [Header("Reloading")]
    public float rspd;
    public float rtime;
    public bool reloading;
    [Header("Main")]
    public bool shotcooldown;
    public float nospam;
    public float shtspd;
    public float shootingRange;
    public bool maingun;
    public float dmg;
    [Header("Secondary")]
    public bool abcool;
    public float abtime;
    public float abcooldown;
    public bool secondary;
    public float snddmg;
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
            if (Input.GetMouseButton(0) && ammo > 0 && !shotcooldown) //Are you allowed to shooting
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
            if (Input.GetKey(KeyCode.F)&& !abcool)
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
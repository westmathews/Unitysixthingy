using UnityEngine;

public class PewPew : MonoBehaviour
{
    public Camera playerCamera; // Assign your camera in the inspector
    public float shootingRange = 100f; // How far the ray can go
    public LayerMask targetLayer; // Specify which layer to detect hits
    public float ammo;
    public Vector3 target;
    private float rtime;
    public static bool reloading;
    public bool shotcooldown;
    private float nospam;
    public string thing_hit = "nothingyet";
    private void Start()
    {
        ammo = 10;

    }
    void Update()
    {

        if (nospam > 0)
        {
            shotcooldown = true;
        }
        else
        {
            shotcooldown = false;
        }
        nospam = nospam - Time.deltaTime;

        rtime = rtime - Time.deltaTime;
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
                This(transform.position, new Vector3(1, -1, 0));
                nospam = 0.3f;
            }
            else
            {
               if (ammo < 1)
               {
                    reload();
               }
            }
        }

    }
    void reload()
    {
        if (!reloading)
        {
            rtime = 3;
            ammo = 0;
            reloading = true;
        }
        if (rtime <= 0)
        {
            ammo = 10f;
            reloading = false;
        }

       
        
        
        
    }
    void Shoot()
    {
        
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // Shoot from the center of the screen
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, shootingRange, targetLayer))
        {
            Debug.Log("Hit: " + hit.collider.name);
            //Did ya hit?
        }
    }

    void This(Vector3 playerPos, Vector3 offset)
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, shootingRange))
        {
            target = hit.transform.position;
            Debug.Log("Hit object tag: " + hit.collider.tag);
            thing_hit = (hit.collider.tag);
            // Check if the hit object has the "Player" tag
            if (hit.collider.CompareTag("Player"))
            {
                //gets health script owner
                hit.collider.gameObject.GetComponent<Health>().hepo -= GetComponentInChildren<Rifle>().dmg;
               
                // Logic for hitting a player
                Debug.Log("Hit a player!");
                // You can add additional actions here, like applying damage or triggering an effect
            }
        }
        //Ray raytwo = new Vector3(target)(new Vector3(offset));
    }

}
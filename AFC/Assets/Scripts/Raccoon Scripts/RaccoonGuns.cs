using UnityEngine;
using TMPro;
using Mirror;
public class RaccoonGuns : NetworkBehaviour
{
    public GameObject Gun;
    public GameObject actvfire;
    public GameObject fire;
    public GameObject revolver;
    public GameObject rifle;
    public GameObject intcam;
    public Camera playerCamera;
    public Vector3 target;
    public string thing_hit = "nothingyet";
    public float range;
    public float sndshots = 0;
    public float sndtime;
    public GameObject hitind;
    public GameObject hitfab;
    public float flametimer;
    public NetworkConnectionToServer conn;
    public GameObject dartPrefab;
    public Rigidbody darbbody;
    public Transform shootPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        range = GetComponentInParent<PewPew>().shootingRange;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isLocalPlayer)
        {


            flametimer += Time.deltaTime;

            sndtime += Time.deltaTime;

            if (sndtime >= .1 && sndshots > 0)
            {
                Secondary(transform.position, new Vector3(1, -1, 0));
            }
            if (GetComponentInParent<PewPew>().maingun)
            {
                flamie(flametimer);
            }
            if (GetComponentInParent<PewPew>().secondary)
            {
                Secondary(transform.position, new Vector3(1, -1, 0));
            }
        }
    }
    [Command]
    void flamie(float flametimer)
    {
        This(flametimer);
    }

    [ClientRpc]
    void This(float flametimer)
    {
        if (flametimer > .05)
        {
            actvfire = Instantiate(fire, transform.position + transform.forward * 1, transform.rotation);
            actvfire.GetComponent<FlameThrowerParticle>().intcam = intcam;
            actvfire.GetComponent<FlameThrowerParticle>().playerCamera = playerCamera;
            flametimer = 0;
        }
        //Ray raytwo = new Vector3(target)(new Vector3(offset));
    }

    void Secondary(Vector3 playerPos, Vector3 offset)
    {
        CmdShootDart();
        /*rifle.SetActive(false);
        revolver.SetActive(true);
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        sndshots += 1;
        sndtime = 0;
        if (sndshots == 6)
        {
            sndshots = 0;
            revolver.SetActive(false);
            rifle.SetActive(true);
        }
        GetComponentInParent<PewPew>().nospam = 0;
        playerCamera.GetComponent<Lookie>().secondaryrecoil();
        if (Physics.Raycast(ray, out hit, range))
        {
            target = hit.transform.position;
            Debug.Log("Hit object tag: " + hit.collider.tag);
            thing_hit = (hit.collider.tag);
            // Check if the hit object has the "Player" tag
            if (hit.collider.CompareTag("Player"))
            {
                //gets health script owner
                hit.collider.gameObject.GetComponent<Health>().hepo -= GetComponentInParent<PewPew>().snddmg;
                hitind = Instantiate(hitfab, hit.point, Quaternion.identity); //Quaternion.RotateTowards(hitind.transform.rotation, hit.collider.transform.rotation., 360));
                hitind.transform.rotation = intcam.transform.rotation;
                hitind.GetComponent<TextMeshPro>().text = "10";
                // Logic for hitting a player
                // You can add additional actions here, like applying damage or triggering an effect
            }
        }
        //Ray raytwo = new Vector3(target)(new Vector3(offset));*/
    }
    [Command]
    void CmdShootDart()
    {
        GameObject dart = Instantiate(dartPrefab, shootPoint.position, shootPoint.rotation);
        NetworkServer.Spawn(dart);
        darbbody = dart.GetComponent<Rigidbody>();
        darbbody.AddForce(transform.forward * 50, ForceMode.Impulse);
    }
}

using TMPro;
using UnityEngine;
using Mirror;
public class SquirrelGuns : NetworkBehaviour
{
    public GameObject grenadeprefab;
    public GameObject Grenaben;
    public GameObject intcam;
    public Camera playerCamera;
    public Vector3 target;
    public string thing_hit = "nothingyet";
    public float range;
    public float sndshots = 5;
    public bool shat = false;
    public float spreadangle = 5;
    public MeshRenderer nade;
    public Rigidbody grenade;
    public Transform grenada;
    public float FrontDistance;
    public GameObject hitind;
    public GameObject hitfab;
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


            if (Input.GetKey(KeyCode.Mouse1))
            {
                spreadangle = 5;
            }
            else
            {
                spreadangle = 7.5f;
            }
            if (sndshots < 5 && shat)
            {
                intcam.GetComponent<intcamlookie>().xRotation = playerCamera.GetComponent<Lookie>().xRotation;
                This(transform.position, new Vector3(1, -1, 0));
                sndshots += 1;
            }
            if (sndshots >= 5)
            {
                shat = false;
                sndshots = 0;
                Debug.Log("hi alex :)");
                playerCamera.GetComponent<Lookie>().recoil();
            }
            if (GetComponentInParent<PewPew>().maingun)
            {
                This(transform.position, new Vector3(1, -1, 0));
                shat = true;
            }
            if (GetComponentInParent<PewPew>().secondary)
            {
                Secondary(transform.position, new Vector3(1, -1, 0));
            }
        }
    }
    void This(Vector3 playerPos, Vector3 offset)
    {
        Vector3 shootdirection = spreaddirection();
        RaycastHit hit;

        if (Physics.Raycast(playerCamera.transform.position, shootdirection, out hit, range))
        {
            target = hit.transform.position;
            Debug.Log("Hit object tag: " + hit.collider.tag);
            thing_hit = (hit.collider.tag);

            // Check if the hit object has the "Player" tag
            if (hit.collider.CompareTag("Player"))
            {
                //gets health script owner
                hit.collider.gameObject.GetComponent<Health>().hepo -= GetComponentInParent<PewPew>().dmg;
                hit.collider.gameObject.GetComponent<Health>().intcam = intcam;
                //hitind = Instantiate(hitfab, hit.point, Quaternion.identity); //Quaternion.RotateTowards(hitind.transform.rotation, hit.collider.transform.rotation., 360));
                //hitind.transform.rotation = intcam.transform.rotation;
                //hitind.GetComponent<TextMeshPro>().text = "15";
                // Logic for hitting a player
                // You can add additional actions here, like applying damage or triggering an effect
            }
        }
        Debug.DrawRay(playerCamera.transform.position, shootdirection * range, Color.red, 1f);        //Ray raytwo = new Vector3(target)(new Vector3(offset));
    }
    void Secondary(Vector3 playerPos, Vector3 offset)
    {
        //Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        //RaycastHit hit;
        //sndshots += 1;
        Vector3 GunPosition = transform.position;
        Vector3 Gunforward = transform.forward;
        Vector3 Spawnpos = GunPosition + Gunforward * FrontDistance;
        Grenaben = Instantiate(grenadeprefab, Spawnpos, Quaternion.identity);
        grenade = Grenaben.GetComponent<Rigidbody>();
        grenada = Grenaben.GetComponent<Transform>();
        nade = Grenaben.GetComponentInChildren<MeshRenderer>();
        nade.enabled = true;
        grenade.useGravity = true;
        grenade.constraints = RigidbodyConstraints.None;
        grenade.AddForce(transform.forward * 35,ForceMode.Impulse);
        grenada.parent = null;
        grenada.GetComponent<CapsuleCollider>().enabled = true;
        
    }
    Vector3 spreaddirection()
    {
        Vector3 direction = playerCamera.transform.forward;

        float randomhor = Random.Range(-spreadangle, spreadangle);
        float randomvert = Random.Range(-spreadangle, spreadangle);

        Quaternion rotation = Quaternion.Euler(randomvert, randomhor, 0);
        return rotation * direction;
    }
}

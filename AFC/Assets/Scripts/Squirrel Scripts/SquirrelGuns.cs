using TMPro;
using UnityEngine;
using Mirror;
public class SquirrelGuns : NetworkBehaviour
{
    public LayerMask layermask;
    public GameObject self;
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
        if (isLocalPlayer)
        {
            self.layer = 6;
        }
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
    { if (isLocalPlayer)
        {


            Vector3 shootdirection = spreaddirection();
            RaycastHit hit;

            if (Physics.Raycast(playerCamera.transform.position, shootdirection, out hit, range, ~layermask))
            {
                target = hit.transform.position;
                Debug.Log("Hit object tag: " + hit.collider.tag);
                thing_hit = (hit.collider.tag);

                if (hit.collider.CompareTag("Player"))
                {
                    if (!hit.collider.gameObject.GetComponentInChildren<Health>().isLocalPlayer)
                    {


                        NetworkIdentity enemyId = hit.collider.GetComponent<NetworkIdentity>();
                        if (enemyId != null)
                        {

                            cmdchangehealth(enemyId.netId, 15);
                        }
                    }
                }
            }
        }
    }
    void Secondary(Vector3 playerPos, Vector3 offset)
    {
        Grenadetrigger();
    }
    [Command]
    void Grenadetrigger()
    {
        {
            Vector3 GunPosition = transform.position;
            Vector3 Gunforward = transform.forward;
            Vector3 Spawnpos = GunPosition + Gunforward * FrontDistance;
            Grenaben = Instantiate(grenadeprefab, Spawnpos, transform.rotation);
            Grenaben.GetComponent<Squirelgrenade>().ownplayer = gameObject;
            Grenaben.GetComponent<Squirelgrenade>().intcam = intcam;
            NetworkServer.Spawn(Grenaben, connectionToClient);
        }
    }
    
    Vector3 spreaddirection()
    {
        Vector3 direction = playerCamera.transform.forward;

        float randomhor = Random.Range(-spreadangle, spreadangle);
        float randomvert = Random.Range(-spreadangle, spreadangle);
        Quaternion rotation = Quaternion.Euler(randomvert, randomhor, 0);
        return rotation * direction;
    }
    [Command]
    private void cmdchangehealth(uint enemyNetId, float dmgdealt)
    {
        Debug.Log("triggered");
        if (NetworkServer.spawned.TryGetValue(enemyNetId, out NetworkIdentity enemyIdentity))
        {
            Health enemyHealth = enemyIdentity.GetComponentInChildren<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.intcam = intcam;
                enemyHealth.TakeDamage(15, connectionToClient);
            }
            else
            {
                Debug.LogError("Enemy has no Health component.");
            }
        }
        else
        {
            Debug.LogError("Could not find enemy by netId.");
        }
    }
}

using UnityEngine;
using Mirror;
using TMPro;
public class LizardGuns : NetworkBehaviour
{
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
    public GameObject enemy;
    public float dmgdealt;
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
            sndtime += Time.deltaTime;
            if (sndtime >= .1 && sndshots > 0)
            {
                Secondary(transform.position, new Vector3(1, -1, 0));
            }
            if (GetComponentInParent<PewPew>().maingun)
            {
                This(transform.position, new Vector3(1, -1, 0));
            }
            if (GetComponentInParent<PewPew>().secondary)
            {
                Secondary(transform.position, new Vector3(1, -1, 0));
            }
        }
    }

    void This(Vector3 playerPos, Vector3 offset)
    {
        if (isLocalPlayer)
        {
            intcam.GetComponent<intcamlookie>().xRotation = playerCamera.GetComponent<Lookie>().xRotation;
            Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            playerCamera.GetComponent<Lookie>().recoil();


            if (Physics.Raycast(ray, out hit, range))
            {
                target = hit.transform.position;
                Debug.Log("Hit object tag: " + hit.collider.tag);
                thing_hit = (hit.collider.tag);


                // Check if the hit object has the "Player" tag
                if (hit.collider.CompareTag("Player"))
                {

                    NetworkIdentity enemyId = hit.collider.GetComponent<NetworkIdentity>();
                    if (enemyId != null)
                    {
                        dmgdealt = 40;
                        cmdchangehealth(enemyId.netId, dmgdealt);
                    }

                }
            }
        }

       
    }

    [Command]
    private void cmdchangehealth(uint enemyNetId,float dmgdealt)
    {
        if (NetworkServer.spawned.TryGetValue(enemyNetId, out NetworkIdentity enemyIdentity))
        {
            Health enemyHealth = enemyIdentity.GetComponentInChildren<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.intcam = intcam;
                enemyHealth.TakeDamage(dmgdealt);
                
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
    /*[ClientRpc]
    private void RpcDamageSync(GameObject enemy)
    {
        enemy.GetComponent<Health>().hepo -= 40;
    }*/
    void Secondary(Vector3 playerPos, Vector3 offset)
    {
        if (isLocalPlayer)
        {
            rifle.SetActive(false);
            revolver.SetActive(true);
            sndshots += 1;
            sndtime = 0;
            if (sndshots == 6)
            {
                sndshots = 0;
                revolver.SetActive(false);
                rifle.SetActive(true);
            }

 
            intcam.GetComponent<intcamlookie>().xRotation = playerCamera.GetComponent<Lookie>().xRotation;
            Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            playerCamera.GetComponent<Lookie>().recoil();
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

                    NetworkIdentity enemyId = hit.collider.GetComponent<NetworkIdentity>();
                    if (enemyId != null)
                    {
                        dmgdealt = 10;
                        cmdchangehealth(enemyId.netId, dmgdealt);
                    }

                }
            }
        }

    }
}

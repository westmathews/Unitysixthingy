using UnityEngine;
using TMPro;
using Mirror;
public class RaccoonGuns : NetworkBehaviour
{
    public GameObject player;
    public GameObject Gun;
    public GameObject fakefire;
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
    public GameObject shootpointobj;
    public GameObject hookprefab;
    public float hooktimer = 0;
    public GameObject UI;
    public GameObject hookPrefab;
    public GameObject chainPrefab;
    private GameObject currentHook;
    private GameObject currentChain;
    public Vector3 shooterStartPoint;
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

            UI.GetComponent<UI>().mvcool = (hooktimer - 5) * -1;
            if (hooktimer > 5)
            {
                UI.GetComponent<UI>().mvready = true;
            }
            else
            {
                UI.GetComponent<UI>().mvready = false;
            }
            flametimer += Time.deltaTime;
            hooktimer += Time.deltaTime;
            sndtime += Time.deltaTime;

            if (sndtime >= .1 && sndshots > 0)
            {
                Secondary(transform.position, new Vector3(1, -1, 0));
            }
            if (GetComponentInParent<PewPew>().maingun)
            {
                flamie(flametimer, shootPoint.position);
            }
            if (GetComponentInParent<PewPew>().secondary)
            {
                Secondary(transform.position, new Vector3(1, -1, 0));
            }
            if (hooktimer > 5 && Input.GetKeyDown(KeyCode.E))
            {
                ShootHook();
                hooktimer = 0;
                Debug.Log("triggered shot");
            }

        }
    }
    [Command]
    void flamie(float flametimer, Vector3 shtpont)
    {
        if (flametimer > .05)
        {

            actvfire = Instantiate(fire, shootPoint.position, shootPoint.rotation);
            NetworkServer.Spawn(actvfire, connectionToClient);
            //actvfire.GetComponent<FlameThrowerParticle>().ownplayer = shootpointobj;
            actvfire.transform.parent = netIdentity.connectionToClient.identity.gameObject.transform;
            //actvfire.transform.position = shtpont;

            actvfire.GetComponent<FlameThrowerParticle>().intcam = intcam;
            actvfire.GetComponent<FlameThrowerParticle>().playerCamera = playerCamera;
            flametimer = 0;
            RpcSetupFlame(actvfire, netIdentity.connectionToClient.identity.gameObject, shtpont);

        }
    }
    [ClientRpc]
    void RpcSetupFlame(GameObject flame, GameObject playerOwner, Vector3 shtpont)
    {
        flame.transform.parent = playerOwner.transform;
        flame.transform.position = shtpont;
    }
    void Secondary(Vector3 playerPos, Vector3 offset)
    {
        CmdShootDart();
    }
    [Command]
    void CmdShootDart()
    {
        GameObject dart = Instantiate(dartPrefab, shootPoint.position + shootPoint.forward, shootPoint.rotation);
        NetworkServer.Spawn(dart, connectionToClient);
        dart.GetComponent<Dart>().intcam = intcam;
        darbbody = dart.GetComponent<Rigidbody>();
        //darbbody.AddForce(transform.forward * 50, ForceMode.Impulse);
    }
    [Command]
    void ShootHook()
    {
        // 1. Spawn the hook
        currentHook = Instantiate(hookprefab, shootPoint.position + shootPoint.forward, shootPoint.rotation);
        HookScript hookScript = currentHook.GetComponent<HookScript>();
        hookScript.hookStart = shootPoint;
        hookScript.shooter = netIdentity.netId;

        NetworkServer.Spawn(currentHook, connectionToClient);

        // 2. Spawn the chain
        currentChain = Instantiate(chainPrefab);
        NetworkServer.Spawn(currentChain, connectionToClient);

        // 3. Send setup data
        NetworkIdentity hookId = currentHook.GetComponent<NetworkIdentity>();
        NetworkIdentity chainId = currentChain.GetComponent<NetworkIdentity>();
        RpcSetChainEndpoints(chainId.netId, hookId.netId);

        // 4. OPTIONAL: If you want the chain to start building now on the server too:
        Batman chainScript = currentChain.GetComponent<Batman>();
        chainScript.startPoint = shootPoint;
        chainScript.endPoint = currentHook.transform;
    }


    [ClientRpc]
    void RpcSetChainEndpoints(uint chainNetId, uint hookNetId)
    {
        if (NetworkClient.spawned.TryGetValue(chainNetId, out NetworkIdentity chainIdentity) &&
            NetworkClient.spawned.TryGetValue(hookNetId, out NetworkIdentity hookIdentity))
        {
            Batman chainScript = chainIdentity.GetComponent<Batman>();

            // 🧠 Assign start and end
            chainScript.startPoint = shootPoint;
            chainScript.endPoint = hookIdentity.transform;

            // 🔗 Assign chain link prefab (make sure it's set on the player script)
            chainScript.chainLinkPrefab = chainPrefab;
        }
        else
        {
            Debug.LogWarning("Could not find chain or hook in spawned objects");
        }


    }
}
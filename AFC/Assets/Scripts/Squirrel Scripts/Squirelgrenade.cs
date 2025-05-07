using Mirror;
using UnityEngine;

public class Squirelgrenade : NetworkBehaviour
{
    public GameObject ownplayer;
    public GameObject Thineself;
    public Rigidbody me;
    public SphereCollider Explosion;
    public GameObject prefab;
    public bool active = true;
    public CharacterController controller;
    public float kama = 0;
    public GameObject intcam;
    public bool hitself = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.transform.parent = null;
        me.AddForce(transform.forward * 35, ForceMode.Impulse);
    }
    
    
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hitsmth");
        Explosion.enabled = true;
    } 
    void OnTriggerEnter(Collider other)
    {
        hitself = false;
        
        if (other.GetComponent<CharacterController>())
        {
            Debug.Log("got it");
        }
        
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponentInChildren<Health>().isLocalPlayer)
            {
                hitself = true;
            }
            else
            {
                Debug.Log("That aint me");
            }
            Debug.Log("HitPlayer");
            if (other.gameObject.GetComponent<Player_Movement>())
            {
                other.gameObject.GetComponent<Player_Movement>().grenadehit = true;
                Debug.Log("PlayerBounce");
            }
            
            NetworkIdentity enemyId = other.GetComponent<NetworkIdentity>();
            if (enemyId != null)
            {
                cmdchangehealth(enemyId.netId, 40, hitself);
            }
            
        }
        
        
        

       
    }
    [Command]
    private void cmdchangehealth(uint enemyNetId, float dmgdealt, bool hitmeself)
    {
        Debug.Log("triggered");
        if (NetworkServer.spawned.TryGetValue(enemyNetId, out NetworkIdentity enemyIdentity))
        {
            Health enemyHealth = enemyIdentity.GetComponentInChildren<Health>();
            Player_Movement movement = enemyIdentity.GetComponentInChildren<Player_Movement>(); 
            if (enemyHealth != null)
            {
                if (!hitmeself)
                {
                    enemyHealth.intcam = intcam;
                    enemyHealth.TakeDamage(30, connectionToClient);
                }
                else
                {
                    Debug.Log("hitself");
                }
                if (movement != null)
                {
                    Debug.Log("Hit you Hit you now we FUCK HARD");
                    movement.grenadehityou(gameObject, connectionToClient);
                }
                else
                {
                    Debug.Log("Fucks You");
                }
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
        hitself = false;
    }
    void Update()
    {

        if (Explosion.enabled && kama == 100)
        {
            Debug.Log("Destroying self");
            kama = 0;
            Destroy(gameObject);
        }

        if (Explosion.enabled)
        {
            kama += 1;
        }
    }
    //Jaydon's brackets DO NOT DELETE
    //[[[[[[[[[[[[[]]]]]]]]]]]]]
}


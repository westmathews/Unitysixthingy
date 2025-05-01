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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.transform.parent = null;
        me.AddForce(transform.forward * 35, ForceMode.Impulse);
    }

    // Update is called once per frame
    
    
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hitsmth");
        //me.AddExplosionForce(200000, transform.position, 150000, 3000.0f,ForceMode.Impulse);
        Explosion.enabled = true;

    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Explodered");
        //other.GetComponent<Rigidbody>().AddExplosionForce(20, transform.position, 15, 3.0f, ForceMode.Impulse);
        if (other.GetComponent<CharacterController>())
        {
            Debug.Log("got it");
        }
        // Check if the hit object has the "Player" tag
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("HitPlayer");
            if (other.gameObject.GetComponent<Player_Movement>())
            {
                other.gameObject.GetComponent<Player_Movement>().grenadehit = true;
                Debug.Log("PlayerBounce");
            }
            //Vector3 hitdirection =
            NetworkIdentity enemyId = other.gameObject.GetComponent<NetworkIdentity>();
            if (enemyId != null)
            {

                cmdchangehealth(enemyId.netId, 40);
            }
            
            //Debug.Log("Hit");


            // Logic for hitting a player
            // You can add additional actions here, like applying damage or triggering an effect
        }
        
        
        

       
    }
    [Command]
    private void cmdchangehealth(uint enemyNetId, float dmgdealt)
    {
        Debug.Log("triggered");
        if (NetworkServer.spawned.TryGetValue(enemyNetId, out NetworkIdentity enemyIdentity))
        {
            Health enemyHealth = enemyIdentity.GetComponentInChildren<Health>();
            Player_Movement movement = enemyIdentity.GetComponentInChildren<Player_Movement>(); 
            if (enemyHealth != null)
            {
                if (enemyIdentity != ownplayer.GetComponent<NetworkIdentity>())
                {
                    enemyHealth.intcam = intcam;
                    enemyHealth.TakeDamage(30, connectionToClient);
                }
                if (movement != null)
                {

                    Debug.Log("Hit you Hit you now we FUCK HARD");
                    movement.grenadehityou(Explosion);
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
    }
    void Update()
    {

        if (Explosion.enabled && kama == 10000)
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
}

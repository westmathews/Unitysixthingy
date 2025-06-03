using UnityEngine;
using Mirror;

public class Dart : NetworkBehaviour
{
    public float speed = 10f;
    public float lifeTime = 5f;
    public float damage = 25f;
    public GameObject intcam;
    private Rigidbody rb;
    [SyncVar]
    public NetworkIdentity enemy;
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.linearVelocity = transform.forward * speed;
        rb.AddForce(transform.forward * 50, ForceMode.Impulse);
        //Invoke(nameof(DestroySelf), lifeTime);
    }


    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("collided");
        gameObject.transform.parent = other.gameObject.transform;
        rb.isKinematic = true;
        Invoke(nameof(DestroySelf), lifeTime);
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInParent<Player_Movement>().darted = true;
            Debug.Log("collided player");
            NetworkIdentity enemyId = other.gameObject.GetComponent<NetworkIdentity>();
            enemy = enemyId;
            if (enemyId != null)
            {

                cmdchangehealth(enemyId.netId, 25);
            }
            gameObject.transform.parent = other.gameObject.transform;
            rb.isKinematic = true;
            Invoke(nameof(DestroySelf), lifeTime);
        }
        else
        {
            Invoke(nameof(DestroySelf), lifeTime);
        }
        
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
                enemyHealth.TakeDamage(dmgdealt, connectionToClient);
                slow(enemyIdentity);
                
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
        //DestroySelf();
    }
    [Command]
    void slow(NetworkIdentity enemyIdentity)
    {
        Debug.Log("I so smar");
        enemyIdentity.GetComponentInParent<Player_Movement>().darted = true;
        enemyIdentity.GetComponentInParent<Player_Movement>().dartTimer = 3;
        enemy = enemyIdentity;
    }
    [Command]
    public void DestroySelf()
    {
        if (enemy != null)
        {
            var playerMovement = enemy.GetComponentInParent<Player_Movement>();
            if (playerMovement != null)
            {
                playerMovement.darted = false; // Revert darted state
                playerMovement.dartTimer = 0; // Reset timer, if used
                Debug.Log("Undarted");
            }
        }
        NetworkServer.Destroy(gameObject); // Destroy the dart
    }

    /*[Command]
    void fast(uint enemyid)
    {
        if (NetworkServer.spawned.TryGetValue(enemyid, out NetworkIdentity enemy))
        {
            Debug.Log("Fasted");
            enemy.GetComponentInParent<Player_Movement>().darted = false;
            NetworkServer.Destroy(gameObject);
        }
    }
    [Client]
    void fast2(uint enemyid)
    {
        if (NetworkServer.spawned.TryGetValue(enemyid, out NetworkIdentity enemy))
        {
            Debug.Log("Fasted");
            enemy.GetComponentInParent<Player_Movement>().darted = false;
            NetworkServer.Destroy(gameObject);
        }
    }
    */
}
    

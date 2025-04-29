using UnityEngine;
using Mirror;

public class Dart : NetworkBehaviour
{
    public float speed = 10f;
    public float lifeTime = 5f;
    public float damage = 25f;
    public GameObject intcam;
    private Rigidbody rb;
    public NetworkIdentity enemy;
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.linearVelocity = transform.forward * speed;
        rb.AddForce(transform.forward * 50, ForceMode.Impulse);
        Invoke(nameof(DestroySelf), lifeTime);
    }


    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("collided");
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInParent<Player_Movement>().darted = true;
            Debug.Log("collided player");
            NetworkIdentity enemyId = other.gameObject.GetComponent<NetworkIdentity>();
            if (enemyId != null)
            {

                cmdchangehealth(enemyId.netId, 25);
            }
            gameObject.transform.parent = other.gameObject.transform;
            rb.isKinematic = true;
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
                enemy = enemyIdentity;
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
    [Client]
    void slow(NetworkIdentity enemyIdentity)
    {
        Debug.Log("I so smar");
        enemyIdentity.GetComponentInParent<Player_Movement>().darted = true;
        enemyIdentity.GetComponentInParent<Player_Movement>().dartTimer = 3;
    }
    [Server]
    private void DestroySelf(NetworkIdentity enemyIdentity)
    {
        fast(enemy);
        NetworkServer.Destroy(gameObject);
    }
    [Client]
    void fast(NetworkIdentity enemy)
    {
        enemy.GetComponentInParent<Player_Movement>().darted = false;
    }
}
    

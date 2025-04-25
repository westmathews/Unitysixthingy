using UnityEngine;
using Mirror;

public class Dart : NetworkBehaviour
{
    public float speed = 10f;
    public float lifeTime = 5f;
    public float damage = 25f;
    public GameObject intcam;
    private Rigidbody rb;

    public override void OnStartServer()
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
            Debug.Log("collided player");
            NetworkIdentity enemyId = other.gameObject.GetComponent<NetworkIdentity>();
            if (enemyId != null)
            {

                cmdchangehealth(enemyId.netId, 25);
            }
        }

        DestroySelf();
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
    [Server]
    private void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }
}
    

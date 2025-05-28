using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class HookScript : NetworkBehaviour
{
    public Vector3 movingvelocity;
    public Rigidbody self;
    [SyncVar]
    public uint shooter;
    public Transform hookStart; // where the chain starts (e.g., hand or gun)
    public Transform hookEnd;
    private List<GameObject> currentChainLinks = new List<GameObject>();
    private bool built = false;
    private float lifeTime = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(shooter);
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 100, ForceMode.Impulse);
        Invoke(nameof(DestroySelf), lifeTime);
    }
    void Update()
    {
        movingvelocity = self.linearVelocity;
        

    }
    void OnCollisionEnter(Collision other)
    {
        
        gameObject.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 0, 0);
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Hit Ground");
            HitGround(shooter,movingvelocity);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            NetworkIdentity enemyId = other.gameObject.GetComponent<NetworkIdentity>();
            if (enemyId != null)
            {
                Debug.Log("hoooked player");
                HitPlayer(enemyId.netId, movingvelocity);
            }
        }
    }
    [Command]
    void HitGround(uint ShooterId, Vector3 Selfvelocity)
    {
        Invoke(nameof(DestroySelf), lifeTime);
        Debug.Log("Triggered targeting: " + shooter);
        NetworkServer.spawned.TryGetValue(ShooterId, out NetworkIdentity Shooter);
        Player_Movement movescript = Shooter.GetComponentInChildren<Player_Movement>();
        movescript.HookMove(Shooter.connectionToClient,Selfvelocity);
        Debug.Log(Selfvelocity);
        
    }
    [Command]
    void HitPlayer(uint TargetId, Vector3 Selfvelocity)
    {
        Invoke(nameof(DestroySelf), lifeTime);
        Debug.Log("Triggered targeting: " + shooter);
        NetworkServer.spawned.TryGetValue(TargetId, out NetworkIdentity Target);
        Player_Movement movescript = Target.GetComponentInChildren<Player_Movement>();
        movescript.HookMove(Target.connectionToClient, new Vector3 (Selfvelocity.x * -1,Selfvelocity.y *-1,Selfvelocity.z *-1));
        Debug.Log(Selfvelocity);
        
    }
    [Server]
    private void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }


}

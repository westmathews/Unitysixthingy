using Mirror;
using UnityEngine;

public class HookScript : NetworkBehaviour
{
    public Vector3 movingvelocity;
    public Rigidbody self;
    public NetworkConnection shooter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(shooter);
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 100, ForceMode.Impulse);
        dostuff();
    }
    [Server]
    void dostuff()
    {
        if (shooter == null)
        {
            Debug.LogError("Shooter is null. Make sure it is assigned before calling dostuff.");
            return;
        }
        NetworkConnection shttr = shooter;
        SyncShooter(shttr.identity.netId);
    }
    [ClientRpc]
    void SyncShooter(uint ShttrId)
    {
        if (!NetworkServer.spawned.TryGetValue(ShttrId, out NetworkIdentity Shoter))
        {
            Debug.LogError($"Failed to find NetworkIdentity with NetId {ShttrId}");
            return;
        }

        Debug.Log("Triggered" + ShttrId);;

        shooter = Shoter.connectionToClient;

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
            NetworkIdentity ShotId = shooter.identity;
            HitGround(ShotId.netId,movingvelocity);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            NetworkIdentity enemyId = other.gameObject.GetComponent<NetworkIdentity>();
            if (enemyId != null)
            {

            }
        }
    }
    [Command]
    void HitGround(uint ShooterId, Vector3 Selfvelocity)
    {
        Debug.Log("Triggered targeting: " + shooter);
        NetworkServer.spawned.TryGetValue(ShooterId, out NetworkIdentity Shooter);
        Player_Movement movescript = Shooter.GetComponentInChildren<Player_Movement>();
        movescript.HookMove(shooter,Selfvelocity);
        Debug.Log(Selfvelocity);
    }
}

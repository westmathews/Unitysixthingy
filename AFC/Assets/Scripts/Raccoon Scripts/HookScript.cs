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
        SyncShooter(shooter.identity.netId);
    }
    [ClientRpc]
    void SyncShooter(uint shootmanid)
    {
        NetworkServer.spawned.TryGetValue(shootmanid, out NetworkIdentity shootman);
        shooter = shootman.connectionToClient;
    }
    private void Update()
    {
        movingvelocity = self.linearVelocity;
    }
    private void OnCollisionEnter(Collision other)
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

            }
        }
    }
    [TargetRpc]
    private void HitGround(NetworkConnection Shooter, Vector3 Selfvelocity)
    {
        Debug.Log("Triggered targeting: " + shooter);
        Player_Movement movescript = shooter.identity.GetComponent<Player_Movement>();
        movescript.HookMove(shooter,Selfvelocity);
        Debug.Log(Selfvelocity);
    }
}

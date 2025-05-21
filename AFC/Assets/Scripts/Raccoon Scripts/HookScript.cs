using Mirror;
using UnityEngine;

public class HookScript : NetworkBehaviour
{
    public Vector3 movingvelocity;
    public Rigidbody self;
    [SyncVar]
    public uint shooter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(shooter);
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 100, ForceMode.Impulse);
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

            }
        }
    }
    [Command]
    void HitGround(uint ShooterId, Vector3 Selfvelocity)
    {
        Debug.Log("Triggered targeting: " + shooter);
        NetworkServer.spawned.TryGetValue(ShooterId, out NetworkIdentity Shooter);
        Player_Movement movescript = Shooter.GetComponentInChildren<Player_Movement>();
        movescript.HookMove(Shooter.connectionToClient,Selfvelocity);
        Debug.Log(Selfvelocity);
    }
}

using UnityEngine;
using Mirror;

public class Dart : NetworkBehaviour
{
    public float speed = 10f;
    public float lifeTime = 5f;
    public float damage = 25f;

    private Rigidbody rb;

    public override void OnStartServer()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * speed;
        Invoke(nameof(DestroySelf), lifeTime);
    }

    [ServerCallback]
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health hp = other.GetComponent<Health>();
            if (hp != null)
            {
                hp.TakeDamage(damage, null); // You can update this to pass shooterConn if needed
            }
        }

        DestroySelf();
    }

    [Server]
    private void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }
}

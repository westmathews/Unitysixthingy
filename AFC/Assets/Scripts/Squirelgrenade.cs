using UnityEngine;

public class Squirelgrenade : MonoBehaviour
{
    public GameObject ownplayer;
    public GameObject Thineself;
    public Rigidbody me;
    public SphereCollider Explosion;
    public GameObject prefab;
    public bool active = true;
    public CharacterController controller;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    
    
    void OnCollisionEnter(Collision collision)
    {
        
        //me.AddExplosionForce(200000, transform.position, 150000, 3000.0f,ForceMode.Impulse);
        Explosion.enabled = true;
    }
    void OnTriggerEnter(Collider other)
    {
        //other.GetComponent<Rigidbody>().AddExplosionForce(20, transform.position, 15, 3.0f, ForceMode.Impulse);
        if (other.GetComponent<CharacterController>())
        {
            Debug.Log("got it");
        }
        // Check if the hit object has the "Player" tag
        if (other.gameObject.CompareTag("Player"))
        {
            //Vector3 hitdirection = 
            if (other.gameObject.GetComponentInChildren<SquirrelGuns>())
            {

            }
            else
            {
                //gets health script owner
                other.gameObject.GetComponentInChildren<Health>().hepo -= 50;
            }
            Debug.Log("Hit");
            other.gameObject.GetComponent<Player_Movement>().grenadehit = true;
            // Logic for hitting a player
            // You can add additional actions here, like applying damage or triggering an effect
        }
        
        Destroy(Thineself);        
    }
}

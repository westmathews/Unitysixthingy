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
    public float kama = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
            if (other.gameObject.GetComponentInChildren<SquirrelGuns>())
            {

            }
            else
            {
                //gets health script owner
                other.gameObject.GetComponentInChildren<Health>().hepo -= 50;
                Debug.Log("Boom");
                
            }
            Destroy(Thineself);
            //Debug.Log("Hit");


            // Logic for hitting a player
            // You can add additional actions here, like applying damage or triggering an effect
        }
        
        
        

       
    }
    void Update()
    {

        if (Explosion.enabled && kama == 20)
        {
            Debug.Log("Destroying self");
            Destroy(Thineself);
        }

        if (Explosion.enabled)
        {


            kama += 1;
        }
    }
}

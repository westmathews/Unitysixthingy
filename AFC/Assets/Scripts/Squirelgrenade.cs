using UnityEngine;

public class Squirelgrenade : MonoBehaviour
{
    public GameObject Thineself;
    public SphereCollider Explosion;
    public GameObject prefab;
    public bool active = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnCollisionEnter(Collision collision)
    {
        Explosion.enabled = true;
    }
    void OnTriggerEnter(Collider other)
    {
        // Check if the hit object has the "Player" tag
        if (other.gameObject.CompareTag("Player"))
        {
            //gets health script owner
            other.gameObject.GetComponent<Health>().hepo -= 50;

            // Logic for hitting a player
            // You can add additional actions here, like applying damage or triggering an effect
        }
        //GetComponent<SquirrelGuns>().Grenaben = prefab;
        Destroy(Thineself);
        
    }
    /*void OnCollisionStay(Collision collision)
    {
        self.useGravity = false;
        meself.GetComponent<CapsuleCollider>().enabled = false;
        Explosion.enabled = true;
        meself.SetParent(sticky);
        self.transform.position = intobj.transform.position;
        self.constraints = RigidbodyConstraints.FreezeAll;
    }
    */
}

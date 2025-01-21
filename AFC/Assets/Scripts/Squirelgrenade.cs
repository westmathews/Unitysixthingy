using UnityEngine;

public class Squirelgrenade : MonoBehaviour
{
    public GameObject intobj;
    public Rigidbody self;
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
        self.useGravity = false;
    }
}

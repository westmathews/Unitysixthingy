using UnityEngine;
using UnityEngine.ParticleSystemJobs;
using System.Collections;
using System.Collections.Generic;
public class FlameThrowerParticle : MonoBehaviour
{
    
    public ParticleSystem self;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
        
        if (other.gameObject.CompareTag("Player")&&!other.gameObject.GetComponentInChildren<RaccoonGuns>())
        {
            
            Debug.Log("HitPlayer");
            other.gameObject.GetComponentInChildren<Health>().hepo -= 1;
            var collision = self.collision;
            collision.enabled = false;
        }
        
    }
}

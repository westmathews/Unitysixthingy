using UnityEngine;
using UnityEngine.ParticleSystemJobs;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Mirror.Examples.Common;
using UnityEngine.UIElements;

public class FlameThrowerParticle : MonoBehaviour
{
    public GameObject intcam;
    public GameObject hitind;
    public GameObject hitfab;
    public ParticleSystem self;
    public Camera playerCamera;

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
            hitind = Instantiate(hitfab, other.GetComponent<Collider>().ClosestPointOnBounds(self.transform.position), Quaternion.identity); //Quaternion.RotateTowards(hitind.transform.rotation, hit.collider.transform.rotation., 360));
            hitind.transform.rotation = intcam.transform.rotation;
            hitind.GetComponent<TextMeshPro>().text = "1";
        }
        
    }
}

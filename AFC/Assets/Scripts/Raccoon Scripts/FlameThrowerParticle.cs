using UnityEngine;
using UnityEngine.ParticleSystemJobs;
using System.Collections;
using System.Collections.Generic;
public class FlameThrowerParticle : MonoBehaviour
{
    public ParticleSystem self;
    public bool bullseye;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleTrigger()
    {
        Debug.Log("trigeer");
    }
}

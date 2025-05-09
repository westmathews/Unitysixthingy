using UnityEngine;
using UnityEngine.ParticleSystemJobs;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Mirror.Examples.Common;
using UnityEngine.UIElements;
using Mirror;
public class FlameThrowerParticle : NetworkBehaviour
{
    public GameObject intcam;
    public GameObject hitind;
    public GameObject hitfab;
    public ParticleSystem self;
    public Camera playerCamera;
    public bool fake = false;
    public float existtimer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Debug.Log("World scale" + gameObject.transform.lossyScale);
        Debug.Log("local scale" + gameObject.transform.lossyScale);
    }
    private void Update()
    {
        existtimer += Time.deltaTime;
        if (existtimer > .05)
        {
            gameObject.transform.parent = null;
            //Debug.Log("orphan");
        }
    }
    void OnParticleCollision(GameObject other)
    {

        if (!fake)
        {


            if (other.gameObject.CompareTag("Player"))
            {
                if (!other.GetComponentInChildren<Health>().isLocalPlayer)
                {

                    NetworkIdentity enemyId = other.GetComponentInParent<NetworkIdentity>();
                    cmdchangehealth(enemyId.netId, 1);
                    /*other.gameObject.GetComponentInChildren<Health>().hepo -= 1;
                    other.gameObject.GetComponentInChildren<Health>().burn = 5;
                    other.gameObject.GetComponentInChildren<Health>().burnTimer = 1;
                    other.gameObject.GetComponentInChildren<Health>().intcam = intcam;*/
                    var collision = self.collision;
                }
                else
                {
                    Debug.Log("That Me:)");
                }

            }
        }
    }
    [Command]
    private void cmdchangehealth(uint enemyNetId, float dmgdealt)
    {
        Debug.Log("triggered");
        if (NetworkServer.spawned.TryGetValue(enemyNetId, out NetworkIdentity enemyIdentity))
        {
            Health enemyHealth = enemyIdentity.GetComponentInChildren<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.intcam = intcam;
                enemyHealth.TakeDamage(dmgdealt, connectionToClient);
                enemyHealth.burn = 5;
                enemyHealth.burnTimer = 1;
            }
            else
            {
                Debug.LogError("Enemy has no Health component.");
            }
        }
        else
        {
            Debug.LogError("Could not find enemy by netId.");
        }
    }
}

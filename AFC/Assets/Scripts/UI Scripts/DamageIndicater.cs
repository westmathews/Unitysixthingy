using UnityEngine;

public class DamageIndicater : MonoBehaviour
{
    public float deathtimer;
    public GameObject self;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        deathtimer += Time.deltaTime;
        transform.Translate(transform.up * Time.deltaTime);
        if (deathtimer > .5)
        {
            Destroy(self);
        }
    }
}

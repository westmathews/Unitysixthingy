using UnityEngine;

public class Health : MonoBehaviour
{
    
    public float hepo;
    public MeshRenderer player;
    public bool ouch;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player.enabled = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hepo < 1)
        {
            Debug.Log("OH SHIT OH FUCK IVE BEEN SHOT");
            player.enabled = false;
            hepo = 9999999;
            
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            hepo = hepo - 10;
        }
        

        
    }
}

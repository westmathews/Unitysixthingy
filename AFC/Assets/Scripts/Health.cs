using UnityEngine;

public class Health : MonoBehaviour
{
    public static float hp;
    public float hepo;
    public MeshRenderer player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hepo = 100f;
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
        
    }
}

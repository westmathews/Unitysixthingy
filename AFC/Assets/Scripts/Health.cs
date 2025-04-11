using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private float regentimer = 0;
    public float hitcheck;
    public float hepo;
    public float maxhp;
    public MeshRenderer player;
    public float regencool = 0;
    public Image healthbar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hepo = maxhp;
        player.enabled = true;
        hitcheck = hepo;
    }

    // Update is called once per frame
    void Update()
    {
        if (hepo >= maxhp)
        {
            hepo = maxhp;
            hitcheck = hepo;
        }
        regentimer += Time.deltaTime;
        regencool += Time.deltaTime;
        if(hepo < 1)
        {
            Debug.Log("OH SHIT OH FUCK IVE BEEN SHOT");
            player.enabled = false;
            hepo = maxhp;
            
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            hepo -= 10;
        }
        if(hepo < hitcheck)
        {
            hitcheck = hepo;
            regencool = 0;
        }
        if (regencool > 3 && regentimer >= .1 && hepo < maxhp)
        {
            hepo += 1;
            regentimer = 0;
        }
        //healthbar.fillAmount = hepo/maxhp;
    }
}

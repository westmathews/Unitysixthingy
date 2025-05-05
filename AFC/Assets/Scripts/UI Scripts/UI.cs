using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI : MonoBehaviour

{
    public GameObject Bulletuno;
    public GameObject Bulletdos;
    public GameObject Bullettres;
    public GameObject Bulletquatro;
    public TextMeshProUGUI ammer;
    public TextMeshProUGUI hbar;
    public TextMeshProUGUI abiby;
    public TextMeshProUGUI mvabiby;
    private float equalizer;
    public float mvcool;
    public bool mvready = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        equalizer = (GetComponentInParent<PewPew>().abtime -5) * -1;
        hbar.text = "Health: " + GetComponentInParent<Health>().hepo.ToString("F0");
        if (!GetComponentInParent<PewPew>().reloading)
        {
            ammer.text = "Ammo: " + GetComponentInParent<PewPew>().ammo.ToString();
        }
        else
        {
            ammer.text = "Reloading";
        }
        if (!GetComponentInParent<PewPew>().abcool)
        {
            abiby.text = "Reby"; 
        }
        else
        {
            abiby.text = "" + equalizer.ToString("F0");
        }
        if (mvready)
        {
            mvabiby.text = "Redy";
        }
        else
        {
            mvabiby.text = "" + mvcool.ToString("F0");
        }
        /*
        if (GetComponentInParent<PewPew>().ammo >= 1)
        {
            Bulletuno.SetActive(true);
        }
        else
        {
            Bulletuno.SetActive(false);
        }
        if (GetComponentInParent<PewPew>().ammo >= 2)
        {
            Bulletdos.SetActive(true);
        }
        else
        {
            Bulletdos.SetActive(false);
        }
        if (GetComponentInParent<PewPew>().ammo >= 3)
        {
            Bullettres.SetActive(true);
        }
        else
        {
            Bullettres.SetActive(false);
        }
        if (GetComponentInParent<PewPew>().ammo >= 4)
        {
            Bulletquatro.SetActive(true);
        }
        else
        {
            Bulletquatro.SetActive(false);
        }
        */
    }
}

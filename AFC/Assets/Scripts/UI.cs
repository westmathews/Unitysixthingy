using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI : MonoBehaviour

{
    public TextMeshProUGUI ammer;
    public TextMeshProUGUI hbar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        hbar.text = "Health: " + GetComponentInParent<Health>().hepo.ToString();
        if (!GetComponentInParent<PewPew>().reloading)
        {
            ammer.text = "Ammo: " + GetComponentInParent<PewPew>().ammo.ToString();
        }
        else
        {
            ammer.text = "Reloading";
        }
    }
}

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public GameObject SquardButton;
    public GameObject LardButton;
    public GameObject Squirl;
    public GameObject Lard;
    public GameObject Menu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        if (SquardButton.GetComponent<Click>().click)
        {
            Squirl.SetActive(true);
            Menu.SetActive(false);
        }
        if (LardButton.GetComponent<Click>().click)
        {
            Lard.SetActive(true);
            Menu.SetActive(false);
        }
    }
    public void clicke()
    {

    }
}

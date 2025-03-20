using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{

    public GameObject RacButton;
    public GameObject SquardButton;
    public GameObject LardButton;
    public GameObject Squirl;
    public GameObject Lard;
    public GameObject Rac;
    public GameObject Menu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    void Update()
    {

        if (SquardButton.GetComponent<Click>().click)
        {
            Squirl.SetActive(true);
            SquardButton.GetComponent<Click>().click = false;
            Cursor.lockState = CursorLockMode.Locked;
            Menu.SetActive(false);

        }
        if (LardButton.GetComponent<Click>().click)
        {
            Lard.SetActive(true);
            LardButton.GetComponent<Click>().click = false;
            Cursor.lockState = CursorLockMode.Locked;
            Menu.SetActive(false);
        }
        if (RacButton.GetComponent<Click>().click)
        {
            Rac.SetActive(true);
            RacButton.GetComponent<Click>().click = false;
            Cursor.lockState = CursorLockMode.Locked;
            Menu.SetActive(false);
        }
    }
}

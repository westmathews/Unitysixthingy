using UnityEngine;

public class CharacterSelectUI : MonoBehaviour
{
    public bool playerpicked = false;
   
    public CharacterSelectSender sender;

    public void SelectSquirrel()
    {
        Debug.Log("Button Clicked: SQUIRREL");
        sender.SelectCharacter(0);
         Camera cam = GameObject.FindGameObjectWithTag("UICamera")?.GetComponent<Camera>();
        if (cam != null)
        {
            cam.gameObject.SetActive(false);
        }

        Debug.Log("🧍 Local player started, UI disabled.");
    }

    public void SelectLizard()
    {
        Debug.Log("Button Clicked: LIZARD");
        sender.SelectCharacter(1);
         Camera cam = GameObject.FindGameObjectWithTag("UICamera")?.GetComponent<Camera>();
        if (cam != null)
        {
            cam.gameObject.SetActive(false);
        }

        Debug.Log("🧍 Local player started, UI disabled.");
    }

    public void SelectRacoon()
    {
        Debug.Log("Button Clicked: RACOON");
        sender.SelectCharacter(2);
         Camera cam = GameObject.FindGameObjectWithTag("UICamera")?.GetComponent<Camera>();
        if (cam != null)
        {
            cam.gameObject.SetActive(false);
        }

        Debug.Log("🧍 Local player started, UI disabled.");
    }
    public void Update()
    {
        if (!playerpicked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }
    }
}

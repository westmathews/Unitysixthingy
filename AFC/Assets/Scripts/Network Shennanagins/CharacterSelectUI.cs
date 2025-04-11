using UnityEngine;

public class CharacterSelectUI : MonoBehaviour
{
    public CharacterSelectSender sender;

    public void SelectSquirrel()
    {
        Debug.Log("Button Clicked: SQUIRREL");
        sender.ConnectAsCharacter(0); // index for Squirrel prefab
    }

    public void SelectLizard()
    {
        sender.ConnectAsCharacter(1); // index for Lizard prefab
    }

    public void SelectRacoon()
    {
        sender.ConnectAsCharacter(2); // index for Racoon prefab
    }
}

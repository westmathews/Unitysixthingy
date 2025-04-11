using UnityEngine;
using Mirror;

public class CharacterHostButton : MonoBehaviour
{
    public int selectedIndex;

    public void HostAsCharacter()
    {
        Debug.Log("🌐 Hosting as character: " + selectedIndex);

        NetworkClient.OnConnectedEvent += SendCharacterSelection;

        NetworkManager.singleton.StartHost(); // This will trigger OnConnectedEvent
    }

    void SendCharacterSelection()
    {
        Debug.Log("📨 Host sending character index: " + selectedIndex);

        NetworkClient.Send(new CharacterSelectMessage
        {
            selectedCharacterIndex = selectedIndex
        });

        NetworkClient.OnConnectedEvent -= SendCharacterSelection;
    }
}

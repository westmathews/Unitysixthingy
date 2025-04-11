using Mirror;
using UnityEngine;

public class CharacterSelectSender : MonoBehaviour
{
    private int selectedIndex;

    public void ConnectAsCharacter(int index)
    {
        selectedIndex = index;

        // Start the connection
        NetworkManager.singleton.networkAddress = "localhost";
        NetworkManager.singleton.StartClient();

        // Subscribe to the OnConnected event
        NetworkClient.OnConnectedEvent += SendCharacterSelection;
    }

    private void SendCharacterSelection()
    {
        Debug.Log("✅ Connected! Sending character index: " + selectedIndex);
        Debug.Log("Connected to server: " + NetworkClient.isConnected); // Should be true

        NetworkClient.Send(new CharacterSelectMessage
        {
            selectedCharacterIndex = selectedIndex
        });

        // Unsubscribe so it only fires once
        NetworkClient.OnConnectedEvent -= SendCharacterSelection;
    }
}

public struct CharacterSelectMessage : NetworkMessage
{
    public int selectedCharacterIndex;
}

public struct ConnectMessage : NetworkMessage { }

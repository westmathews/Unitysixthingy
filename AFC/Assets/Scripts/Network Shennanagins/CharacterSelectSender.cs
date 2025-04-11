using Mirror;
using UnityEngine;

public class CharacterSelectSender : MonoBehaviour
{
    private int selectedIndex;

    public void ConnectAsCharacter(int index)
    {
        selectedIndex = index;
        Debug.Log("🟡 Button pressed. Selected index: " + selectedIndex);

        NetworkClient.OnConnectedEvent += SendCharacterSelection;

        Debug.Log("🟡 Starting client...");
        NetworkManager.singleton.networkAddress = "localhost";
        NetworkManager.singleton.StartClient();
    }

    private void SendCharacterSelection()
    {
        Debug.Log("🟢 Connected! Sending character index: " + selectedIndex);

        NetworkClient.Send(new CharacterSelectMessage
        {
            selectedCharacterIndex = selectedIndex
        });

        NetworkClient.OnConnectedEvent -= SendCharacterSelection;
    }
}
public struct CharacterSelectMessage : NetworkMessage
{
    public int selectedCharacterIndex;
}

public struct ConnectMessage : NetworkMessage { }

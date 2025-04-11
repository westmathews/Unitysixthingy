using UnityEngine;
using Mirror;

public class CharacterSelectSender : MonoBehaviour
{
    public int selectedPrefabIndex = 0;

    public void ConnectAsCharacter(int index)
    {
        selectedPrefabIndex = index;
        NetworkManager.singleton.StartClient();

        NetworkClient.RegisterHandler<ConnectMessage>(OnClientConnected);
    }

    void OnClientConnected(ConnectMessage msg)
    {
        var message = new SelectCharacterMessage { prefabIndex = selectedPrefabIndex };
        NetworkClient.Send(message);
    }
}
public struct ConnectMessage : NetworkMessage { }

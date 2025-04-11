using UnityEngine;
using Mirror;
using System.Collections.Generic;

public class MyNetworkManager : NetworkManager
{
    public Camera uiCamera;
    public GameObject[] playerPrefabs;
    private Dictionary<NetworkConnectionToClient, int> selectedPrefabIndex = new();

    public override void OnStartServer()
    {
        base.OnStartServer();
        NetworkServer.RegisterHandler<SelectCharacterMessage>(OnReceiveCharacterChoice);
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        int index = 0;
        if (selectedPrefabIndex.TryGetValue(conn, out int chosenIndex))
            index = Mathf.Clamp(chosenIndex, 0, playerPrefabs.Length - 1);

        GameObject prefab = playerPrefabs[index];
        Transform start = GetStartPosition();
        GameObject player = Instantiate(prefab, start.position, start.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);
        base.OnServerAddPlayer(conn);

        if (uiCamera != null)
            uiCamera.gameObject.SetActive(false);
    }

    void OnReceiveCharacterChoice(NetworkConnectionToClient conn, SelectCharacterMessage msg)
    {
        Debug.Log($"[Server] Received prefab index: {msg.prefabIndex} from client {conn.connectionId}");
        selectedPrefabIndex[conn] = msg.prefabIndex;
    }
    
    
     

        

}


public struct SelectCharacterMessage : NetworkMessage
{
    public int prefabIndex;
}

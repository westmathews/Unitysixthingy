using UnityEngine;
using Mirror;
using System.Collections.Generic;

public class MyNetworkManager : NetworkManager
{
    public Camera uiCamera;
    public GameObject[] characterPrefabs;
    private Dictionary<NetworkConnectionToClient, int> connectionToIndex = new();

    public override void OnStartServer()
    {
        base.OnStartServer();
        NetworkServer.RegisterHandler<CharacterSelectMessage>(OnCharacterSelectReceived);
    }
    void OnCharacterSelectMessage(NetworkConnectionToClient conn, CharacterSelectMessage msg)
    {
        Debug.Log("💥 Server received character index: " + msg.selectedCharacterIndex);

        GameObject player = Instantiate(characterPrefabs[msg.selectedCharacterIndex]);
        NetworkServer.AddPlayerForConnection(conn, player);
    }

    void OnCharacterSelectReceived(NetworkConnectionToClient conn, CharacterSelectMessage msg)
    {
        Debug.Log("Server received character index: " + msg.selectedCharacterIndex);
        connectionToIndex[conn] = msg.selectedCharacterIndex;

        // Now spawn player manually
        SpawnPlayer(conn);
    }

    void SpawnPlayer(NetworkConnectionToClient conn)
    {
        if (conn.identity != null)
        {
            Debug.LogWarning("Player already exists.");
            return;
        }

        int index = connectionToIndex.ContainsKey(conn) ? connectionToIndex[conn] : 0;

        GameObject prefab = characterPrefabs[index];
        GameObject player = Instantiate(prefab);
        NetworkServer.AddPlayerForConnection(conn, player);
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        // Override to do nothing; we’re handling it ourselves now
    }





}


public struct SelectCharacterMessage : NetworkMessage
{
    public int prefabIndex;
}

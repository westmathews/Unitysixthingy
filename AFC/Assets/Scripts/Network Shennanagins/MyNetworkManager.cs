using Mirror;
using UnityEngine;

public class MyNetworkManager : NetworkManager
{
    public GameObject squirrelPrefab;
    public GameObject lizardPrefab;
    public GameObject racoonPrefab;
    public override void OnStartServer()
    {
        base.OnStartServer();

        CharacterSelectionData.Initialize(new GameObject[] {
            squirrelPrefab, lizardPrefab, racoonPrefab
        });
    }

    
}

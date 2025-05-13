using Mirror;
using UnityEngine;

public class PlaceholderPlayer : NetworkBehaviour
{
    [Command]
    public void CmdChooseCharacter(int index)
    {
        Debug.Log("Server received character selection: " + index);
        GameObject prefab = CharacterSelectionData.GetCharacterPrefab(index);
        if (prefab != null)
        {
            GameObject newCharacter = Instantiate(prefab, transform.position, transform.rotation);
            NetworkServer.ReplacePlayerForConnection(connectionToClient, newCharacter, true);
            Debug.Log("Server replaced player with new character.");
        }
        else
        {
            Debug.LogError("Invalid character index! FATHER HELP.");
        }
    }
}

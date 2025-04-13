using UnityEngine;
using Mirror;

public class CharacterSelectSender : MonoBehaviour
{
   public void SelectCharacter(int index)
{
    if (NetworkClient.connection != null && NetworkClient.connection.identity != null)
    {
        var player = NetworkClient.connection.identity.GetComponent<PlaceholderPlayer>();
        player.CmdChooseCharacter(index);
    }
    else
    {
        Debug.LogWarning("🚨 Cannot select character! Not connected or no identity yet.");
    }
}
}

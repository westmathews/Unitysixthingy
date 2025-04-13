using UnityEngine;
using Mirror;
using System.Collections;

public class CharacterHostButton : MonoBehaviour
{
    public int selectedIndex;

    public void HostAsCharacter()
    {
        // Save the selected index for the host
        CharacterSelectionData.selectedIndex = selectedIndex;
        Debug.Log("🌐 Hosting as character: " + selectedIndex);

        // Wait one frame to ensure NetworkManager.singleton is ready
        StartCoroutine(DelayedHostStart());
    }

    IEnumerator DelayedHostStart()
    {
        yield return null; // wait for the next frame

        if (NetworkManager.singleton != null)
        {
            Debug.Log("✅ NetworkManager.singleton is ready. Hosting now!");
            NetworkManager.singleton.StartHost();
        }
        else
        {
            Debug.LogError("❌ NetworkManager.singleton is STILL null! FATHER HELP.");
        }
    }
}


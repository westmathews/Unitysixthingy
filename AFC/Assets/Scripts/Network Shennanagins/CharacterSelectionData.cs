using UnityEngine;

public static class CharacterSelectionData
{
    public static GameObject[] characterPrefabs;
     public static int selectedIndex = 0;

    public static void Initialize(GameObject[] prefabs)
    {
        characterPrefabs = prefabs;
    }

    public static GameObject GetCharacterPrefab(int index)
    {
        if (characterPrefabs != null && index >= 0 && index < characterPrefabs.Length)
        {
            return characterPrefabs[index];
        }

        return null;
    }
}

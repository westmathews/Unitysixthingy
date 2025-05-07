using Mirror;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;

    private Dictionary<string, List<NetworkConnectionToClient>> rooms = new();

    void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        instance = this;
    }

    public string CreateRoom(NetworkConnectionToClient hostConn)
    {
        string code = GenerateRoomCode();
        rooms[code] = new List<NetworkConnectionToClient> { hostConn };
        return code;
    }

    public bool JoinRoom(string code, NetworkConnectionToClient conn)
    {
        if (rooms.TryGetValue(code, out var roomPlayers))
        {
            roomPlayers.Add(conn);
            return true;
        }
        return false;
    }

    string GenerateRoomCode()
    {
        const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ123456789";
        string code;
        do
        {
            code = new string(System.Linq.Enumerable.Range(0, 5)
                .Select(x => chars[Random.Range(0, chars.Length)]).ToArray());
        } while (rooms.ContainsKey(code));
        return code;
    }
}


using UnityEngine;
using System.Collections.Generic;
public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    [SerializeField] private GameObject keyPrefab;
    [SerializeField] private GameObject TableLegDrop;
    private bool keySpawned = false;
    public static LevelManager Instance { get { return _instance; } }
    private HashSet<GameObject> visitedRooms = new HashSet<GameObject>();
    public float chancePerRoom = 0.1f;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    public void CheckIfRoomIsNew(GameObject room)
    {
        // Check if visited before
        if (!visitedRooms.Contains(room))
        {
            SpawnWeapon(room);
            visitedRooms.Add(room);
            Debug.Log("New room visited. Total unique rooms: " + visitedRooms.Count);
            if (!keySpawned)
            {
                SpawnKey(room);
            }
        }
        else
        {
            Debug.Log("Room already visited.");
        }
    }

    private void SpawnKey(GameObject room)
    {
        Vector3 spawnPos;
        int roomsVisited = visitedRooms.Count;
        float chance = Mathf.Clamp01(roomsVisited * chancePerRoom);
        int level = LevelGeneration.Instance.level;

        if (roomsVisited == LevelGeneration.Instance.roomAmountPerLevel[level])
        {
            chance = 1f; // Guarantee key spawn in last room
        }
        float seedRoll = (float)GameSeed.KeyRandom.NextDouble();
        if (seedRoll < chance && roomsVisited > 2)
        {
            if (room.transform.Find("KeySpawn") != null)
            {
                spawnPos = room.transform.Find("KeySpawn").position;

            }
            else
            {
                spawnPos = new Vector3(room.transform.position.x, room.transform.position.y + 2, room.transform.position.z);
            }

            Instantiate(keyPrefab, spawnPos, Quaternion.identity);
            keySpawned = true;

        }
    }

    public void SpawnWeapon(GameObject room)
    {
        Vector3 spawnPos;
        float weaponChance = 0.2f;
        float seedroll = (float)GameSeed.WeaponRandom.NextDouble();
        if (seedroll < weaponChance)
        {
            if (room.transform.Find("WeaponSpawn") != null)
            {
                spawnPos = room.transform.Find("WeaponSpawn").position;
                Debug.Log($"weapon spawn point found in {room.name}");
            }
            else
            {
                spawnPos = new Vector3(room.transform.position.x, room.transform.position.y + 2, room.transform.position.z);
            }

            Instantiate(TableLegDrop, spawnPos, Quaternion.identity);
            Debug.Log($"weapon spawned in {room.name}");
        }
    }

    public int RoomsVisitedCount()
    {
        return visitedRooms.Count;
    }

    public void ResetLevelProgress()
    {
        visitedRooms.Clear();
        keySpawned = false;
    }
    public void AddStartRoom()
    {
        GameObject startRoom = GameObject.FindWithTag("StartRoom");
        if (startRoom != null)
        {
            visitedRooms.Add(startRoom);
        }
        else
        {
            Debug.LogWarning("Start room not found");
        }
    }
}

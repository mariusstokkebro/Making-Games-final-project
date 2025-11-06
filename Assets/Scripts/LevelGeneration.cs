using UnityEngine;
using System.Collections.Generic;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] private int roomAmount = 5;

    [SerializeField] private GameObject startRoom;
    [SerializeField] private List<GameObject> level1RoomPrefabs;

    private List<Transform> availableDoors = new List<Transform>();

    void Start()
    {
        Instantiate(startRoom, Vector3.zero, Quaternion.identity);
        AddRoomDoors(startRoom);
        for (int i = 0; i < roomAmount; i++)
        {
            if (availableDoors.Count == 0)
            {
                Debug.LogWarning("No more doors to attach new rooms!");
                break;
            }
            //choose door and remove it from available doors
            Transform attachDoor = availableDoors[0];
            availableDoors.RemoveAt(0);
            //choose random room from list
            GameObject newRoomPrefab = level1RoomPrefabs[Random.Range(0, level1RoomPrefabs.Count)];
            GameObject newRoom = Instantiate(newRoomPrefab);

            Vector3 position = new Vector3(0, 0, 0);
            int roomIndex = Random.Range(0, level1RoomPrefabs.Count);
            Instantiate(level1RoomPrefabs[roomIndex], position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void AddRoomDoors(GameObject room)
    {
        int doorAmount = 0;
        Transform roomLayout = room.transform.GetChild(1);
        foreach (Transform child in roomLayout)
        {
            if (child.CompareTag("Door"))
            {
                availableDoors.Add(child);
                doorAmount++;
            }


        }
        Debug.Log(doorAmount);
    }

    private Transform FindDoorForSecondRoom(GameObject room, Transform firstRoomDoor)
    {
        Transform RoomLayout = room.transform.GetChild(1);
        List<Transform> doors = new List<Transform>();
        foreach (Transform child in roomLayout)
        {
            if (child.CompareTag("Door"))
                doors.Add(child);
        }

        if (doors.Count == 0) return null;
        return doors.;
    }
}

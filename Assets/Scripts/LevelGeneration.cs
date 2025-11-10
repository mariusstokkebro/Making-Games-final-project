using UnityEngine;
using System.Collections.Generic;
public class LevelGeneration : MonoBehaviour
{

    [SerializeField] private int roomAmount = 5;
    // if all doors in one room needs to be used before moving to next room
    [SerializeField] private bool uniformRoomGeneration = true;
    [SerializeField] private GameObject startRoom;
    [SerializeField] private List<RoomsInLevel> roomPrefabsByLevel = new List<RoomsInLevel>();
    private int level = 0;
    private Transform attachDoor;
    private List<Transform> availableDoors = new List<Transform>();
    private List<Transform> doorsToDestroy = new List<Transform>();
    private List<GameObject> roomsToDestroy = new List<GameObject>();

    void Start()
    {
        generateLevel(roomPrefabsByLevel, roomAmount, level);
    }
    void generateLevel(List<RoomsInLevel> roomPrefabs, int roomAmount, int level)
    {
        if (roomsToDestroy.Count > 0)
        {
            DestroyRooms(roomsToDestroy);
            roomsToDestroy.Clear();
        }
        Instantiate(startRoom, Vector3.zero, Quaternion.identity);
        //move camera to start room
        Transform cameraPoint = startRoom.transform.Find("cameraPoint");
        Camera.main.transform.position = cameraPoint.position;
        Camera.main.transform.rotation = cameraPoint.rotation;
        AddRoomDoors(startRoom);
        roomsToDestroy.Add(startRoom);
        for (int i = 0; i < roomAmount; i++)
        {
            if (availableDoors.Count == 0)
            {
                Debug.LogWarning("No more doors to attach new rooms!");
                break;
            }
            //choose door and remove it from available doors
            if (uniformRoomGeneration == false)
            {
                int randomIndex = Random.Range(0, availableDoors.Count);
                attachDoor = availableDoors[randomIndex];
                availableDoors.RemoveAt(randomIndex);
            }
            else
            {
                attachDoor = availableDoors[0];
                availableDoors.RemoveAt(0);
            }
            addRooms();

        }
        foreach (Transform door in availableDoors)
        {
            doorsToDestroy.Add(door);
        }
        DestroyDoors(doorsToDestroy);

    }
    // Update is called once per frame
    void Update()
    {

    }

    private void AddRoomDoors(GameObject room, Transform usedDoor = null)
    {
        int doorAmount = 0;
        Transform roomLayout = room.transform.GetChild(1);
        foreach (Transform child in roomLayout)
        {
            if (child.CompareTag("Door") && child != usedDoor)
            {
                availableDoors.Add(child);
                doorAmount++;
            }


        }
        Debug.Log(doorAmount);
    }

    private Transform FindDoorForSecondRoom(GameObject room, Transform firstRoomDoor)
    {
        Transform roomLayout = room.transform.GetChild(1);
        List<Transform> doors = new List<Transform>();
        foreach (Transform child in roomLayout)
        {
            if (child.CompareTag("Door"))
            {
                float difference = (child.rotation.eulerAngles.y - firstRoomDoor.rotation.eulerAngles.y);
                if (difference < 0)
                    difference = difference * -1;
                if (difference > 130 && difference < 230)
                {
                    doors.Add(child);
                }
            }
        }

        if (doors.Count == 0) return null;
        return doors[0];
    }
    private void AlignRooms(Transform firstDoor, Transform secondDoor)
    {
        Transform newRoom = secondDoor.root;

        // Calculate how far apart the doors are
        Vector3 offset = firstDoor.position - secondDoor.position + (firstDoor.TransformDirection(Vector3.left) * 10f);

        // Move new room by that offset
        newRoom.position += offset;
    }
    private void DestroyDoors(List<Transform> doors)
    {
        foreach (Transform door in doors)
        {
            Destroy(door.gameObject);
        }
    }
    private void DestroyRooms(List<GameObject> rooms)
    {
        foreach (GameObject room in rooms)
        {
            Destroy(room);
        }
    }
    private void addRooms()
    {
        //choose random room from list
        GameObject newRoomPrefab = roomPrefabsByLevel[level].roomPrefabs[
        Random.Range(0, roomPrefabsByLevel[level].roomPrefabs.Count)
];
        GameObject newRoom = Instantiate(newRoomPrefab);
        Transform secondDoor = FindDoorForSecondRoom(newRoom, attachDoor);
        if (secondDoor)
        {
            AddRoomDoors(newRoom, secondDoor);
            AlignRooms(attachDoor, secondDoor);
            roomsToDestroy.Add(newRoom);
            //newRoom.transform.GetChild(1).gameObject.SetActive(false); //disable room layout
            //newRoom.transform.GetChild(2).gameObject.SetActive(false); //disable enemies
        }
        else
        {
            Destroy(newRoom);
            Debug.LogWarning("No suitable door found in new room");
            addRooms();
        }
    }


}

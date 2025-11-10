using UnityEngine;

public class DoorScript : MonoBehaviour
{

    LayerMask roomMask;
    LayerMask doorMask;
    int distanceFromDoorToPlayer = 10;
    Vector3 back;
    void Start()
    {
        roomMask = LayerMask.GetMask("Room");
        doorMask = LayerMask.GetMask("Door");
        back = transform.TransformDirection(Vector3.left);
    }
    void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            //Debug.Log("Door opened");
            Transform room = GetClosestRoom();
            ActivateRoom(room);
            MoveCameraToRoom(room);
            MovePlayerToRoom(other);
            ActivateEnemies(room);
            DisableCurrentRoom();
        }
    }

    Transform GetClosestRoom()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * 1000, Color.white);
        if (Physics.Raycast(transform.position, back, out hit, 100, roomMask))
        {
            return hit.transform;
        }
        Debug.LogWarning("No room found");
        return null;

    }
    void MoveCameraToRoom(Transform room)
    {
        Transform cameraPoint = room.Find("cameraPoint");
        if (cameraPoint != null)
        {
            Camera.main.transform.position = cameraPoint.position;
            Camera.main.transform.rotation = cameraPoint.rotation;
        }
        else
        {
            Debug.LogWarning($"No cameraPoint found in {room.name}");
        }
    }
    void ActivateRoom(Transform room)
    {
        room.Find("roomLayout").gameObject.SetActive(true);
    }
    void ActivateEnemies(Transform room)
    {
        GameObject enemies = room.Find("Enemies")?.gameObject;
        if (enemies == null)
        {
            Debug.LogWarning($"No Enemies found in {room.name}");
        }
        else
        {
            room.Find("Enemies").gameObject.SetActive(true);
        }

    }

    void MovePlayerToRoom(Collider other)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, back, out hit, 100, doorMask))
        {
            other.transform.position = hit.transform.position + (back * distanceFromDoorToPlayer);
        }
        else
        {
            Debug.LogWarning("No door found behind the door");
        }

    }

    void DisableCurrentRoom()
    {
        transform.parent.gameObject.SetActive(false);
        transform.parent.parent.Find("Enemies").gameObject.SetActive(false);
    }
}


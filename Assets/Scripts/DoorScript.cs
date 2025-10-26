



using UnityEngine;

public class DoorScript : MonoBehaviour
{

    LayerMask layerMask;
    int distanceFromDoorToNextRoom = 10;
    Vector3 back;
    void Start()
    {
        layerMask = LayerMask.GetMask("Room");
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
            movePlayerToRoom(other);
            DisableCurrentRoom();
        }
    }

    Transform GetClosestRoom()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * 1000, Color.white);
        if (Physics.Raycast(transform.position, back, out hit, 20, layerMask))
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

    void movePlayerToRoom(Collider other)
    {
        other.transform.position = transform.position + back * distanceFromDoorToNextRoom;
    }

    void DisableCurrentRoom()
    {
        transform.parent.gameObject.SetActive(false);
    }
}


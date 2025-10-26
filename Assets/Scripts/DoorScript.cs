

using UnityEngine;

public class DoorScript : MonoBehaviour
{

    LayerMask layerMask;

    void Start()
    {
        layerMask = LayerMask.GetMask("Room");

    }
    void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            //Debug.Log("Door opened");
            Transform room = GetClosestRoom();
            MoveCameraToRoom(room);

        }
    }

    Transform GetClosestRoom()
    {
        RaycastHit hit;
        Vector3 back = transform.TransformDirection(Vector3.left);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * 1000, Color.white);
        if (Physics.Raycast(transform.position, back, out hit, 20, layerMask))
        {
            return hit.transform;
        }
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
}


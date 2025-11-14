using UnityEngine;
public class DoorScript : MonoBehaviour
{
    private bool smoothCameraTransition = false;
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
            if (smoothCameraTransition)
            {
                Camera.main.GetComponent<CameraScript>().SetTarget(cameraPoint);
            }
            else
            {
                Camera.main.transform.position = cameraPoint.position;
                Camera.main.transform.rotation = cameraPoint.rotation;
            }

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
            return;
        }

        enemies.SetActive(true);

    }

    void MovePlayerToRoom(Collider other)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, back, out hit, 20, doorMask))
        {
            Vector3 targetPos = new Vector3(hit.transform.position.x, 0, hit.transform.position.z)
                             + (back * distanceFromDoorToPlayer);

            CharacterController cc = other.GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false;           // disable before teleporting
                other.transform.position = targetPos;
                cc.enabled = true;            // re-enable after teleporting
            }
            else
            {
                Debug.LogWarning("No CharacterController found on player");
            }
        }
        else
        {
            Debug.LogWarning("No door found behind the door");
        }

    }

    void DisableCurrentRoom()
    {
        transform.parent.gameObject.SetActive(false);
        Transform enemies = transform.parent.parent.Find("Enemies");
        if (enemies)
        {
            enemies.gameObject.SetActive(false);
        }
    }
}


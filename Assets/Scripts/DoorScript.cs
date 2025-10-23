using System.Runtime.CompilerServices;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            Debug.Log("Door opened");

        }
    }

    // Update is called once per frame
    void GetClosestRoom()
    {
        Vector3 back = transform.TransformDirection(Vector3.backwards);
    }
}

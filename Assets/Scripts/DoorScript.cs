using System.Runtime.CompilerServices;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Door opened");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            target = null;
        }

    }
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }


}

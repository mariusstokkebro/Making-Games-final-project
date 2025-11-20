using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    public float moveDuration = 0.2f;

    private Vector3 startPosition;
    private float elapsedTime;
    private bool isMoving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        if (!isMoving)
        {
            // Start a new smooth movement
            startPosition = transform.position;
            elapsedTime = 0f;
            isMoving = true;
        }

        if (isMoving)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration);
            float easedT = EaseInOutCirc(t);

            transform.position = Vector3.Lerp(startPosition, target.position, easedT);

            if (t >= 1f)
            {
                isMoving = false;
                target = null;
            }
        }

    }
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        isMoving = false;
    }

    float EaseInOutCirc(float x)
    {
        return x < 0.5f
            ? (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * x, 2))) / 2f
            : (Mathf.Sqrt(1 - Mathf.Pow(-2 * x + 2, 2)) + 1) / 2f;
    }
}

using UnityEngine;
using System.Collections;
public abstract class BaseEnemy : BaseEntity
{
    [SerializeField] protected float activationDelay = 1f;
    protected bool isActive = false;
    private Coroutine activationCoroutine;

    protected virtual void OnEnable()
    {
        // Stop any previous activation
        if (activationCoroutine != null)
            StopCoroutine(activationCoroutine);

        // Start delayed activation
        activationCoroutine = StartCoroutine(ActivateAfterDelay());
    }

    private IEnumerator ActivateAfterDelay()
    {
        isActive = false;
        yield return new WaitForSeconds(activationDelay);
        isActive = true;
        OnActivated();
    }

    protected virtual void OnActivated() { }
    protected Transform FindPlayer()
    {
        return FindEntity("Player");
    }

    protected void TurnTowardsPlayer()
    {

        if (!isActive) return;

        var player = FindPlayer();
        Vector3 dir = (player.position - transform.position).normalized;

        float targetAngle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;

        float newAngle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, newAngle, 0);
    }

    protected void MoveTowardsTarget(Vector2 target)
    {
        if (!isActive) return;
        var player = FindPlayer();
        Vector3 direction = player.position - transform.position;
        direction.y = 0f;
        direction = direction.normalized;
        transform.position += direction * (movementSpeed * Time.deltaTime);
    }
}

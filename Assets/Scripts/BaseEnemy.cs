using UnityEngine;

public abstract class BaseEnemy : BaseEntity
{
    // Update is called once per frame
    protected Transform FindPlayer()
    {
        return FindEntity("Player");
    }

    protected void TurnTowardsPlayer()
    {
        var player = FindPlayer();
        Vector3 dir = (player.position - transform.position).normalized;

        float targetAngle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;

        float newAngle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, newAngle, 0);
    }

    protected void MoveTowardsTarget(Vector2 target)
    {
        var player = FindPlayer();
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * (movementSpeed * Time.deltaTime);
    }
}

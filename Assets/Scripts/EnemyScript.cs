using UnityEngine;

public abstract class EnemyScript : EntityScript
{
    // Update is called once per frame
    private Transform FindPlayer()
    {
        return FindEntity("Player");
    }

    private void TurnTowardsPlayer()
    {
        Vector2 dir = (FindPlayer().position - transform.position).normalized;
        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        float newAngle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, turnSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, newAngle);
    }

    private void MoveTowardsTarget(Vector2 target)
    {
        Vector2 newPos = Vector2.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);
        transform.position = newPos;
    }
}

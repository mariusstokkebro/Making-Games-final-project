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
        Transform player = FindEntity();
        if (player == null) return;
        Vector2 dir = (player.position - transform.position).normalized;

        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

        float currentAngle = transform.eulerAngles.z;

        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, turnSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0, 0, newAngle);
    }

    private void MoveTowardsTarget(Vector2 target)
    {
        Vector2 currentPos = transform.position;
        Vector2 newPos = Vector2.MoveTowards(currentPos, target, movementSpeed * Time.deltaTime);
        transform.position = newPos;
    }
}

using UnityEngine;
using System.Collections;
public abstract class BaseEnemy : BaseEntity
{
    [SerializeField] protected float activationDelay = 1f;
    protected bool isActive = false;
    private Coroutine activationCoroutine;
    [SerializeField] private GameObject loot;

    protected virtual void OnEnable()
    {
        // Stop any previous activation
        if (activationCoroutine != null)
            StopCoroutine(activationCoroutine);

        // Start delayed activation
        activationCoroutine = StartCoroutine(ActivateAfterDelay());
        if (GameSeed.EnemyRandom.NextDouble() < 1.0) deathEffect = loot;
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

    protected void TurnTowardsTarget(Vector3 targetPosition)
    {
        if (!isActive) return;

        Vector3 dir = targetPosition - transform.position;
        dir.y = 0f;
        if (dir.magnitude < 0.01f) return;

        transform.right = Vector3.Slerp(transform.right, -dir.normalized, turnSpeed * Time.deltaTime);
        
        Debug.DrawRay(transform.position, -transform.right * 2f, Color.red);
        Debug.DrawRay(transform.position, dir.normalized * 2f, Color.green);
    }

    protected void MoveTowardsTarget(Vector3 targetPosition)
    {
        if (!isActive) return;

        Vector3 dir = targetPosition - transform.position;
        dir.y = 0f;
        if (dir.magnitude < 0.01f) return;

        Vector3 moveVec = dir.normalized * (movementSpeed * Time.deltaTime);
        Debug.DrawRay(transform.position, moveVec * 50f, Color.blue);
        
        transform.position += moveVec;
    }
}
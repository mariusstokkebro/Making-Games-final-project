using UnityEngine;
using System.Collections;
using Items_and_Weapons;

public abstract class BaseEnemy : BaseEntity
{
    [SerializeField] protected float activationDelay = 1f;
    protected bool isActive = false;
    private Coroutine activationCoroutine;
    [SerializeField] private GameObject lootPrefab;
    private PassiveItemData _drop;
    public void AssignDrop(PassiveItemData drop) => _drop = drop;

    protected virtual void OnEnable()
    {
        // Chance for enemy to have loot, deathEffect lets us hardcode drops for some enemies
        if (GameSeed.EnemyRandom.NextDouble() < 1.0 && deathEffect == null)
        {
            var drop = LootTable.GetDrop();
            if (drop == null) return;
            AssignDrop(drop);
            deathEffect = lootPrefab;
        }

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
    
    protected override void Die()
    {
        GameObject drop = default;
        if (deathEffect)
        {
            drop = Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        if (drop != null && drop.TryGetComponent(out PassiveItemScript s))
        {
            Debug.Log($"Dropped item {s.itemData}");
            s.SetItem(_drop);
        }
        Destroy(gameObject);
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

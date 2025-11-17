using UnityEngine;
using System.Collections;
using Items_and_Weapons;

public abstract class BaseEnemy : BaseEntity
{
    protected Vector3 velocity;
    public float gravity = -9.81f;
    [SerializeField] protected float activationDelay = 1f;
    protected bool isActive = false;
    private Coroutine activationCoroutine;
    [SerializeField] private GameObject lootPrefab;
    private BaseItem _drop;
    public void AssignDrop(BaseItem drop) => _drop = drop;

    protected CharacterController controller;

    protected virtual void Awake()
    {
        controller = GetComponent<CharacterController>();
        // GameSeed.Initialize(null);
    }
    protected virtual void OnEnable()
    {
        Debug.Log($"Enabled {this.name}");
        // Chance for enemy to have loot, deathEffect lets us hardcode drops for some enemies
        if (GameSeed.EnemyRandom.NextDouble() < 1.0 && deathEffect == null)
        {
            var drop = LootTable.GetPassiveDrop();
            if (drop != null)
            {
                AssignDrop(drop);
                deathEffect = lootPrefab;
            }
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

        if (drop != null && drop.TryGetComponent(out ItemScript s))
        {
            Debug.Log($"Dropped item {s.item}");
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
        velocity.y += gravity * Time.deltaTime;
        Vector3 moveVec = dir.normalized * (movementSpeed * Time.deltaTime);
        Vector3 move = moveVec + velocity * Time.deltaTime;
        Debug.DrawRay(transform.position, moveVec * 50f, Color.blue);

        if (knockbackTimer > 0f)
        {
            move += knockbackVelocity * Time.deltaTime;
            knockbackTimer -= Time.deltaTime;
        }

        controller.Move(move);

        if (controller.isGrounded) velocity.y = 0f;
    }
}

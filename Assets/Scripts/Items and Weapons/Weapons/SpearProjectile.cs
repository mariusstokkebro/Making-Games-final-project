using UnityEngine;

public class SpearProjectile : WeaponProjectile
{
    private Transform target;
    private Vector3 moveDirection;
    private bool initialized = false;
    private bool hasHit = false;

    [SerializeField] private float speed = 20f;
    [SerializeField] private float aimOffset = 0.2f;

    public bool playerWeapon = false;

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;

        Vector3 dir = (target.position - transform.position).normalized;
        Vector3 offset = new Vector3(
            Random.Range(-aimOffset, aimOffset),
            0f,
            Random.Range(-aimOffset, aimOffset)
        );

        moveDirection = (dir + offset).normalized;

        transform.forward = moveDirection;

        initialized = true;
    }

    void Update()
    {
        CountDown(Time.deltaTime);

        if (!initialized) return;

        transform.position += moveDirection * speed * Time.deltaTime;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (hasHit) return;
        if (playerWeapon)
        {
            base.OnTriggerEnter(other);
            return;
        }

        if (other.CompareTag("Player"))
        {
            hasHit = true;

            moveDirection = Vector3.zero;
            initialized = false;
            
            if (other.TryGetComponent<BaseEntity>(out var entity))
            {
                Debug.Log(damage);
                entity.TakeDamage(damage);

                Vector3 dir = other.transform.position - transform.position;
                dir.y = 0f;
                Destroy(gameObject);
            }
        }
    }
}

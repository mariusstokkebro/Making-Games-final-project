using UnityEngine;

public class WeaponProjectile : MonoBehaviour
{
    protected float damage;
    protected float lifetime;
    protected float countdown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void Initialize(float damage, float range, float duration)
    {
        this.damage = damage;
        this.countdown = duration;
        lifetime = duration;
        transform.localScale *= range;
    }

    // Update is called once per frame
    void Update()
    {
        CountDown(Time.deltaTime);
    }

    protected void CountDown(float time)
    {
        countdown -= time;
        if (countdown <= 0)
            Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<BaseEntity>().TakeDamage(damage);
            Vector3 dir = other.transform.position - transform.position;
            dir.y = 0f; // No vertical knockback

            other.gameObject.GetComponent<BaseEntity>().ApplyKnockback(dir);
        }
    }
}

using UnityEngine;

public class WeaponProjectile : MonoBehaviour
{
    private float damage;
    private float angle;
    private float countdown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Initialize(float damage, float range, float angle, float duration)
    {
        this.damage = damage;
        this.angle = angle;
        this.countdown = duration;
        transform.localScale = new Vector3(range, range, range);
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0)
            Destroy(gameObject);
    }
    
    void OnTriggerEnter(Collider other)
    {
        Vector3 hitDirection = (other.gameObject.transform.position - transform.position).normalized;
        if (Vector3.Angle(transform.forward, hitDirection) <= angle && other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy!");
            other.gameObject.GetComponent<BaseEntity>().TakeDamage(damage);
        }
    }
}

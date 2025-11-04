using UnityEngine;

public class SaltAttackScript : MonoBehaviour
{
    [SerializeField] private float destroyTime = 0.5f;
    [SerializeField] private float damageAmount = 50.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<BaseEntity>().TakeDamage(damageAmount);

        }

    }
}

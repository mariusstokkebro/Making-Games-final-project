using UnityEngine;

public abstract class EntityScript : MonoBehaviour
{
    
    [SerializeField] protected float health = 100f;
    [SerializeField] protected float movementSpeed = 3f;
    [SerializeField] protected float damage = 10f;
    [SerializeField] protected float turnSpeed = 180f;
    [SerializeField] protected GameObject deathEffect;

    protected Transform FindEntity(string entityTag)
    {
        GameObject entity = GameObject.FindWithTag(entityTag);
        return entity ? entity.transform : null;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (deathEffect)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}

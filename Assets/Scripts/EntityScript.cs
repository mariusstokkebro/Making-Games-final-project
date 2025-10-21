using UnityEngine;

public abstract class EntityScript : MonoBehaviour
{
    private float health;
    public float movementSpeed;
    public GameObject deathEffect;
    public float damage;

    Transform FindEntity(string entityTag)
    {
        GameObject entity = GameObject.FindWithTag(entityTag);
        return entity.transform;
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
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

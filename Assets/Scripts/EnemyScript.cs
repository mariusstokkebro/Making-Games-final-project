using UnityEngine;

public abstract class EnemyScript : MonoBehaviour
{
    private float health;

    public float damage;

    public string playerTag;

    public GameObject deathEffect;

    public float movementSpeed;

    Transform FindPlayer()
    {
        GameObject player = GameObject.FindWithTag(playerTag);
        return player.transform;
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

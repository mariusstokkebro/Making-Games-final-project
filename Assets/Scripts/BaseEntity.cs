using Items_and_Weapons;
using UnityEngine;
using System;

public abstract class BaseEntity : MonoBehaviour
{
    [SerializeField] protected float health = 100f;
    [SerializeField] protected float movementSpeed = 3f;
    [SerializeField] protected float damage = 10f;
    [SerializeField] protected float turnSpeed = 180f;
    [SerializeField] protected GameObject deathEffect;
    [SerializeField] protected Matrix4x4 _matrix = Matrix4x4.Rotate(Quaternion.Euler(0, -45, 0));
    [SerializeField] protected float knockbackStrength = 10f;
    [SerializeField] protected float knockbackDuration = 0.15f;
    public event Action<EntityStats, float> OnStatChanged;
    protected Vector3 knockbackVelocity;
    protected float knockbackTimer = 0f;

    // Call this when entity gets hit
    public void ApplyKnockback(Vector3 direction)
    {
        knockbackVelocity = direction.normalized * knockbackStrength;
        knockbackTimer = knockbackDuration;
    }
    protected Transform FindEntity(string entityTag)
    {
        GameObject entity = GameObject.FindWithTag(entityTag);
        return entity ? entity.transform : null;
    }

    public virtual void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        if (deathEffect) Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public float GetMovementSpeed() => movementSpeed;
    public void ModifyMovementSpeed(float newMovementSpeed)
    {

        movementSpeed = newMovementSpeed;
        OnStatChanged?.Invoke(EntityStats.Speed, movementSpeed);

    }

    public float GetDamage() => damage;
    public void ModifyDamage(float newDamage)
    {
        damage = newDamage;
        OnStatChanged?.Invoke(EntityStats.Damage, damage);

    }

    public float GetHealth() => health;
    public void ModifyHealth(float newHealth)
    {
        health = newHealth;
    }

    public void Heal(float amount)
    {
        health += amount;
    }
}

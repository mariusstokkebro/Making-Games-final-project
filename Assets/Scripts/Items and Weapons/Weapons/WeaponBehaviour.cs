using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    private WeaponProjectile projectile;
    private float damage;
    private float attackDuration;
    private float range;
    private float angle;
    
    private float cooldownTime;
    private float countdown;
    
    private ParticleEffect particleEffect;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    public void Initialize(float damage, float duration, float cooldown, float range, float angle, WeaponProjectile projectile, ParticleEffect effect)
    {
        cooldownTime = cooldown;
        this.projectile = projectile;
        particleEffect = effect;
        
        this.damage = damage;
        this.attackDuration = duration;
        this.range = range;
        this.angle = angle;
    }

    public void StartAttack()
    {
        if (countdown <= 0)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player == null) return;

            var rotation = player.transform.rotation;
            var position = player.transform.position;
            var fireAngle = rotation * Quaternion.Euler(0, -90, 0);

            Instantiate(projectile, position, fireAngle).Initialize(damage, range, angle, attackDuration);
            Instantiate(particleEffect, position, fireAngle);
            countdown = cooldownTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
    }
}

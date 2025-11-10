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

    private bool canBreak;
    private int usesUntilBreak;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    public void Initialize(Weapon weaponData)
    {
        cooldownTime = weaponData.cooldown;
        projectile = weaponData.weaponProjectile.GetComponent<WeaponProjectile>();
        particleEffect = weaponData.visualEffect.GetComponent<ParticleEffect>();

        damage = weaponData.damage;
        attackDuration = weaponData.duration;
        range = weaponData.range;
        angle = weaponData.angle;

        canBreak = weaponData.canBreak;
        usesUntilBreak = weaponData.usesUntilBreak;
    }

    public void StartAttack()
    {
        if (countdown <= 0)
        {
            Instantiate(projectile, transform.position, transform.rotation).Initialize(damage, range, angle, attackDuration);
            Instantiate(particleEffect, transform.position, transform.rotation);
            countdown = cooldownTime;

            if (canBreak)
                DecreaseDurability();
        }
    }

    public void DecreaseDurability()
    {
        --usesUntilBreak;
        HUD.Instance.ReduceWeaponUses();
        if (usesUntilBreak <= 0)
        {
            Destroy(gameObject);
        }
        // TODO Update UI
    }
    

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
    }
}

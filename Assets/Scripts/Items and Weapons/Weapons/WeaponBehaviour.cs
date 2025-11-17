using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    private WeaponProjectile projectile;
    private float damage;
    private float attackDuration;
    private float range;
    
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

        canBreak = weaponData.canBreak;
        usesUntilBreak = weaponData.usesUntilBreak;
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

            Instantiate(projectile, position, fireAngle).Initialize(damage, range, attackDuration);
            Instantiate(particleEffect, position, fireAngle);
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
            // This is only fine because our primary weapon should never break
            HUD.Instance.RemoveSecondaryWeapon();
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
    }
}

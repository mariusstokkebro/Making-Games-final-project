using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
public class Weapon : BaseItem
{
    [SerializeField] protected GameObject weaponProjectile;
    [SerializeField] protected GameObject visualEffect;

    [SerializeField] protected Vector3 offset;

    [SerializeField] protected float damage = 0;
    /** Length of the timeframe in which the attack can deal damage */
    [SerializeField] protected float duration = 0;
    /** Minimum cooldown time between two attacks*/
    [SerializeField] protected float cooldown = 0;

    [SerializeField] protected float range = 0;
    [SerializeField] protected float angle = 0;
    
    // TODO do we actually need this?
    [SerializeField] protected ActiveItemEffect effect;

    
    public Mesh mesh;
    public Sprite sprite;

    public WeaponBehaviour EquipWeapon(PlayerScript player)
    {
        GameObject obj = new GameObject("Weapon");
        obj.transform.SetParent(player.transform);
        obj.AddComponent<WeaponBehaviour>();
        
        var toReturn = obj.AddComponent<WeaponBehaviour>();
        toReturn.transform.SetLocalPositionAndRotation(GetSpawn(player), GetRotation(player));
        toReturn.Initialize(damage, duration, cooldown, range, angle, weaponProjectile.GetComponent<WeaponProjectile>(), visualEffect.GetComponent<ParticleEffect>());
        
        return toReturn;
    }

    protected Quaternion GetRotation(PlayerScript p)
        => (Quaternion.LookRotation(GetForwardDirection(p))).normalized;

    protected Vector3 GetSpawn(PlayerScript p)
        => p.transform.position + offset;// * (p.transform.right * -1);

    protected Vector3 GetForwardDirection(PlayerScript p)
        => (p.transform.right * -1).normalized;
    
    public override string GetDescription()
    {
        return $"{itemName}\n" +
               $"Damage: {damage}\n" +
               $"Duration: {duration}\n" +
               $"Cooldown: {cooldown}\n" +
               $"Range: {range}\n" +
               $"Effect: {effect}";
    }
}

public enum ActiveItemEffect
{   
    Damaging,
    Healing,
    Befriending,
}

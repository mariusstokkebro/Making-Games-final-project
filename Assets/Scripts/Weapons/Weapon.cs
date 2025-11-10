using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
public class Weapon : ScriptableObject
{
    public GameObject weaponProjectile;
    public GameObject visualEffect;

    public Vector3 offset;
    
    public float damage = 100f;
    /** Length of the timeframe in which the attack can deal damage */
    public float duration = 0.5f;
    /** Minimum cooldown time between two attacks*/
    public float cooldown = 1f;
    
    public float range = 20f;
    public float angle = 25f;
    
    public bool canBreak = false;
    public int usesUntilBreak = 10;
    
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
        toReturn.Initialize(this);
        
        return toReturn;
    }

    protected Quaternion GetRotation(PlayerScript p)
        => (Quaternion.LookRotation(GetForwardDirection(p))).normalized;

    protected Vector3 GetSpawn(PlayerScript p)
        => p.transform.position + offset;// * (p.transform.right * -1);
    
    protected Vector3 GetForwardDirection(PlayerScript p)
        => (p.transform.right * -1).normalized;
}

public enum ActiveItemEffect
{   
    Damaging,
    Healing,
    Befriending,
}

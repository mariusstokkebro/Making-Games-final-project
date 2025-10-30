using UnityEngine;

public abstract class ActiveItem : MonoBehaviour
{
    #nullable enable
    [SerializeField] protected ActiveItemEffect effect;
    
    /// <summary>
    /// Shoot/Stab/Use the primary effect of the item
    /// e.g. Maraca salt-shotgun
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract T? Cast<T>(); // T could be a float for healing items or null for side effects
    
    /// <summary>
    /// Shoot/Stab/Use the secondary effect of the item
    /// e.g. Maraca salt-beam
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract T? AltCast<T>();

    /// <summary>
    /// Drops item
    /// </summary>
    public abstract void Drop(PlayerScript p);

    /// <summary>
    /// A one-time effect when picking up item, e.g. unlocking the dash
    /// </summary>
    public abstract void OnPickup(PlayerScript p);
}

public enum ActiveItemEffect
{
    Healing,
    Damaging,
    Befriending,
}

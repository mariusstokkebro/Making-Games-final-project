using Enums;
using UnityEngine;
public abstract class BaseItem : ScriptableObject
{
    [SerializeField] protected string itemName;
    [SerializeField] protected string description;
    [SerializeField] public Rarity rarity = Rarity.Natural;

    public virtual string GetDescription()
    {
        return description;
    }
}
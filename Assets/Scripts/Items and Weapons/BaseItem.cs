using Enums;
using UnityEngine;
public abstract class BaseItem : ScriptableObject
{
    [SerializeField] protected string itemName;
    [SerializeField] protected string description;
    [SerializeField] public Rarity rarity = Rarity.Natural;

    public string GetDescription()
    {
        return description;
    }
}
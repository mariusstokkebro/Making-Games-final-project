using Enums;
using UnityEngine;
public abstract class BaseItem : ScriptableObject
{
    #nullable enable
    [SerializeField] protected string itemName;
    [SerializeField] public Sprite? sprite;
    [SerializeField] protected string description;
    [SerializeField] public Rarity rarity = Rarity.Natural;

    public virtual string GetDescription()
    {
        return description;
    }
}
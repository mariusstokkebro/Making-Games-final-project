using UnityEngine;
public abstract class BaseItem : ScriptableObject
{
    [SerializeField] protected string itemName;
    [SerializeField] protected string description;

    public virtual string GetDescription()
    {
        return description;
    }
}
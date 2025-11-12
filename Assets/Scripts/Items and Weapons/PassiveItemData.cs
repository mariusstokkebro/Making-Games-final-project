using UnityEngine;

[CreateAssetMenu(fileName = "PassiveItem", menuName = "Scriptable Objects/Passive Item")]
public class PassiveItemData : BaseItem
{
    public Sprite sprite;
    public PassiveEffect[] effects;
    
}

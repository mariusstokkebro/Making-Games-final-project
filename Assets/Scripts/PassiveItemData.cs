using UnityEngine;

[CreateAssetMenu(fileName = "PassiveItem", menuName = "Scriptable Objects/Passive Item")]
public class PassiveItemData : ScriptableObject
{
    public string itemName;
    public Mesh mesh;
    public Sprite sprite;
    public PassiveEffect[] effects;
    
}
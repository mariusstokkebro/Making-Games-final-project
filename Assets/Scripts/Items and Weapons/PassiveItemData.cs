using Items_and_Weapons.Effects;
using UnityEngine;

namespace Items_and_Weapons
{
    [CreateAssetMenu(fileName = "PassiveItem", menuName = "Scriptable Objects/Passive Item")]
    public class PassiveItemData : BaseItem
    {
        public Sprite sprite;
        public PassiveEffect[] effects;
        public override string ToString() => name;
    }


}

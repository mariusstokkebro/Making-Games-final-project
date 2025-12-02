using Items_and_Weapons.Effects;
using UnityEngine;
using System.Text;

namespace Items_and_Weapons
{
    [CreateAssetMenu(fileName = "PassiveItem", menuName = "Scriptable Objects/Passive Item")]
    public class PassiveItemData : BaseItem
    {
        public PassiveEffect[] effects;

        public override string GetDescription()
        {
            StringBuilder sb = new StringBuilder();

            if (effects != null && effects.Length > 0)
            {
                foreach (var effect in effects)
                {
                    if (effect != null)
                        sb.AppendLine($"{effect.passiveEffectType}{effect.GetDescription()}");
                }
            }
            else
            {
                sb.AppendLine("No effects");
            }

            return sb.ToString();
        }
    }
}

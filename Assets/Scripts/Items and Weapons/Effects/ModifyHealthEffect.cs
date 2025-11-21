using UnityEngine;
using Enums;

namespace Items_and_Weapons.Effects
{
    [CreateAssetMenu(fileName = "ModifyHealthEffect", menuName = "Scriptable Objects/Item Effects/Modify Max Health")]
    public class ModifyHealthEffect : PassiveEffect
    {
        public ModifyHealthEffect()
        {
            passiveEffectType = Effect.Health;
        }
        public override void Apply(PlayerScript p)
        {
            p.ModifyHealth(p.GetHealth() * multiplier + increase);
        }

    }
}
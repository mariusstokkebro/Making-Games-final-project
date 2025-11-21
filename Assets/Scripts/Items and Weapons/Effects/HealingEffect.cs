using UnityEngine;
using Enums;

namespace Items_and_Weapons.Effects
{
    [CreateAssetMenu(fileName = "HealingEffect", menuName = "Scriptable Objects/Item Effects/Current Health")]
    public class HealingEffect : PassiveEffect
    {
        public HealingEffect()
        {
            passiveEffectType = Effect.Healing;
        }
        public override void Apply(PlayerScript p)
        {
            p.Heal(p.GetHealth() * multiplier + increase );
        }

    }
}
using UnityEngine;

namespace Items_and_Weapons.Effects
{
    [CreateAssetMenu(fileName = "HealingEffect", menuName = "Scriptable Objects/Item Effects/Current Health")]
    public class HealingEffect : PassiveEffect
    {
        public override void Apply(PlayerScript p)
        {
            p.Heal(p.GetHealth() * multiplier + increase );
        }

    }
}
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
            float potentialHealth = p.GetHealth() * multiplier + increase;
            if (potentialHealth >= 100)
            {
                float healAmount = 100 - p.GetHealth();
                p.ModifyHealth(p.GetHealth() + healAmount);
            }
            else
            {
                p.ModifyHealth(p.GetHealth() * multiplier + increase);
            }
        }

    }
}
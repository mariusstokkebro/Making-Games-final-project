using UnityEngine;

namespace Items_and_Weapons.Effects
{
    [CreateAssetMenu(fileName = "ModifyDamageEffect", menuName = "Scriptable Objects/Item Effects/Modify Damage")]
    public class ModifyDamageEffect : PassiveEffect
    {
        public ModifyDamageEffect()
        {
            base.passiveEffectType = Effect.Damage;
        }
        public override void Apply(PlayerScript p)
        {
            p.ModifyDamage(p.GetDamage() * multiplier + increase);
        }

    }
}
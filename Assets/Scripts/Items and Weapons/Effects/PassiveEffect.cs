using UnityEngine;
using Enums;

namespace Items_and_Weapons.Effects
{
    public abstract class PassiveEffect : ScriptableObject
    {
        public Effect passiveEffectType;
        [SerializeField] protected float increase = 0f;
        [SerializeField] protected float multiplier = 1.0f;
        public abstract void Apply(PlayerScript p);
        public virtual string GetDescription()
        {
            var output = $"";
            if (increase > 0f)
            {
                output += $"increase: {increase} ";
            }
            if (multiplier > 1.0f)
            {
                output += $"multiplier: {multiplier} ";
            }
            return output;
        }
    }
}

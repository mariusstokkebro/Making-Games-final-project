using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "ModifyDamageEffect", menuName = "Scriptable Objects/Item Effects/Modify Damage")]
    public class ModifyDamageEffect : PassiveEffect
    {
        public override void Apply(PlayerScript p)
        {
            p.ModifyDamage(p.GetDamage() * multiplier + increase);
        }

    }
}
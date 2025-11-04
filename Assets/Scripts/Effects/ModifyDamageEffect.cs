using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "ModifyDamageEffect", menuName = "Scriptable Objects/Item Effects/Modify Damage")]
    public class ModifyDamageEffect : PassiveEffect
    {
        public float increase;
        public float multiplier = 1f;

        public override void Apply(PlayerScript p)
        {
            throw new System.NotImplementedException();
        }

    }
}
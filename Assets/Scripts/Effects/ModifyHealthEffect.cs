using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "ModifyHealthEffect", menuName = "Scriptable Objects/Item Effects/Modify Health")]
    public class ModifyHealthEffect : PassiveEffect
    {
        public override void Apply(PlayerScript p)
        {
            p.ModifyHealth(p.GetHealth() * multiplier + increase);
        }

    }
}
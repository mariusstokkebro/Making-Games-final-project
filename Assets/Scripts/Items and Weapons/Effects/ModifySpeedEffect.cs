using UnityEngine;

namespace Items_and_Weapons.Effects
{
    [CreateAssetMenu(fileName = "ModifySpeedEffect", menuName = "Scriptable Objects/Item Effects/Modify Speed")]
    public class ModifySpeedEffect : PassiveEffect
    {   
        public override void Apply(PlayerScript p)
        {
            p.ModifyMovementSpeed(p.GetMovementSpeed() * multiplier + increase);
        }
    }
}
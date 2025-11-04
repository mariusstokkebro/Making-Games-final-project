using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "ModifySpeedEffect", menuName = "Scriptable Objects/Item Effects/Modify Speed")]
    public class ModifySpeedEffect : PassiveEffect
    {
        public float increase;
        public float multiplier = 1f;
        
        public override void Apply(PlayerScript p)
        {
            throw new System.NotImplementedException();
        }
    }
}
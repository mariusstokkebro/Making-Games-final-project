using UnityEngine;

namespace Items_and_Weapons.Effects
{
    public abstract class PassiveEffect : ScriptableObject
    {
        [SerializeField] protected float increase = 0f;
        [SerializeField] protected float multiplier = 1.0f;
        public abstract void Apply(PlayerScript p);
    }
}
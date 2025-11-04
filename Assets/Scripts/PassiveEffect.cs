using UnityEngine;

public abstract class PassiveEffect : ScriptableObject
{
    public abstract void Apply(PlayerScript p);
}
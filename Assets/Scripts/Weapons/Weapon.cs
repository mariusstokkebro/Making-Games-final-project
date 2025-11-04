using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    [SerializeField] protected GameObject visualEffect;
    [SerializeField] protected GameObject collider;
    [SerializeField] protected float forwardOffset = 3.5f;
    [SerializeField] protected ActiveItemEffect effect;
    public Mesh mesh;
    public Sprite sprite;
    
    public abstract void Use(PlayerScript p);
    public abstract void AltUse(PlayerScript p);
    public abstract void Drop(PlayerScript p);

    protected Quaternion GetRotation(PlayerScript p)
        => (Quaternion.LookRotation(p.transform.forward) * Quaternion.Euler(0f, -30f, 0f)).normalized;

    protected Vector3 GetSpawn(PlayerScript p)
        => p.transform.position + (p.transform.right * -1) * forwardOffset;
}

public enum ActiveItemEffect
{
    Healing,
    Damaging,
    Befriending,
}

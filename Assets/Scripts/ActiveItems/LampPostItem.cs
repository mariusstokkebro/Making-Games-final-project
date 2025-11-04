using UnityEngine;

[CreateAssetMenu(fileName = "LampPostItem", menuName = "Scriptable Objects/Active Items/Lamp Post")]
public class LampPostItem : ActiveItem
{

    public float damage = 5;

    public override void Cast(PlayerScript p)
    {
        Instantiate(blast, p.transform.position, GetRotation(p));
        Instantiate(collider, GetSpawn(p), Quaternion.LookRotation(p.transform.forward));
    }

    public override void AltCast(PlayerScript p)
    {
        throw new System.NotImplementedException();
    }

    public override void Drop(PlayerScript p)
    {
        throw new System.NotImplementedException();
    }
}

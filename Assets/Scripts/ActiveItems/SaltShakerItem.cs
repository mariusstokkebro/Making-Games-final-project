using UnityEngine;


[CreateAssetMenu(fileName = "SaltShakerItem", menuName = "Scriptable Objects/Active Items/Salt Shaker")]
public class SaltShakerItem : ActiveItem
{

    public float damage = 5;

    public override void Use(PlayerScript p)
    {
        Instantiate(visualEffect, p.transform.position, GetRotation(p));
        Instantiate(collider, GetSpawn(p), Quaternion.LookRotation(p.transform.forward));
    }

    public override void AltUse(PlayerScript p)
    {
        throw new System.NotImplementedException();
    }

    public override void Drop(PlayerScript p)
    {
        throw new System.NotImplementedException();
    }
}

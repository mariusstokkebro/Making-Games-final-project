using UnityEngine;

public class LampScript : ActiveItem
{

    public float damage = 5;
    
    public override T Cast<T>() where T : default
    {
        throw new System.NotImplementedException();
    }

    public override T AltCast<T>() where T : default
    {
        throw new System.NotImplementedException();
    }

    public override void Drop(PlayerScript p)
    {
        throw new System.NotImplementedException();
    }

    public override void OnPickup(PlayerScript p)
    {
        // Try to add to inventory, destroy if picked up
        throw new System.NotImplementedException();
    }
}

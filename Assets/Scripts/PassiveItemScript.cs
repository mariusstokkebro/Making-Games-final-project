using System;
using UnityEngine;

public class PassiveItemScript : MonoBehaviour
{
    #nullable enable
    [SerializeField] private PassiveItemData itemData;

    private void Start()
    {
        gameObject.GetComponent<MeshFilter>().mesh = itemData.mesh;
    }

    /// <summary>
    /// A one-time effect when picking up item, e.g. unlocking the dash
    /// </summary>
    public void OnPickup(PlayerScript p)
    {
        foreach (var effect in itemData.effects)
        {
            effect.Apply(p);
        }
        // Add itemData to p.inventory
        // Destroy(this)?;
    }

    // Think this is best done by having some unlockables in the player? effect.Apply(p) => p.unlockDash();
    // /// <summary>
    // /// Activate the item's effect, e.g. Dashing
    // /// </summary>
    // /// <typeparam name="T"></typeparam>
    // public T? Activate<T>()
    // {
    //     throw new System.NotImplementedException();
    // }
}

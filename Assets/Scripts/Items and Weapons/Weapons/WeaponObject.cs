using System;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponScript : MonoBehaviour
{
    [FormerlySerializedAs("activeItem")] [SerializeField] internal Weapon weapon;

    public WeaponScript(Weapon item)
    {
        weapon = item;
    }

    private void Start()
    {
        gameObject.GetComponent<MeshFilter>().mesh = weapon.mesh;
    }

    /// <summary>
    /// A one-time effect when picking up item, e.g. unlocking the dash
    /// </summary>
    public void OnPickup(PlayerScript p)
    {
        p.SetSecondaryWeapon(weapon);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            var player = collider.GetComponent<PlayerScript>();
            OnPickup(player);
        }
    }
}
